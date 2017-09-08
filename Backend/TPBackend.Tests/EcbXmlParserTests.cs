using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Xunit;

namespace TPBackend.Tests
{
    public abstract class EcbXmlParserTests<TXmlParser, TXmlLoader>
        where TXmlParser: IEcbXmlParser, new()
        where TXmlLoader: IXmlLoader, new()
    {
        [Fact]
        public void ItCanParseTheFileInTheTestFolder()
        {
            var sut = new TXmlParser();

            var xmlDoc = LoadTestDocument();
            var exchangeRates = sut.ReadExchangeRatesFromEcbXml(xmlDoc);

            Assert.Equal(new DateTime(2017, 9, 6), exchangeRates.AsOfTime);
            Assert.Equal(31, exchangeRates.ExchangeRates.Count);

            var exchangeRateGBP = exchangeRates.ExchangeRates.Where(er => er.CurrencyCode == "GBP").ToArray();
            var zeroExchangeRates = exchangeRates.ExchangeRates.Where(er => er.Rate <= 0).ToArray();

            Assert.Equal(1, exchangeRateGBP.Length);
            Assert.Equal(0.91428M, exchangeRateGBP.Single().Rate);
            Assert.Equal(0, zeroExchangeRates.Length);     // No exchange rates should be non-positive
        }

        private XmlDocument LoadTestDocument()
        {
            var xmlLoader = new TXmlLoader();
            var xmlDocument = xmlLoader.LoadLocalXmlFile("eurofxref-daily.xml");

            return xmlDocument;
        }
    }

    public class EcbXmlParserImplTest : EcbXmlParserTests<EcbXmlParserImpl, XmlLoaderImpl> { }
}
