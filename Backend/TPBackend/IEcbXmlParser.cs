using System;
using System.Collections.Generic;
using System.Xml;

namespace TPBackend
{
    public class CurrencyExchangeRate
    {
        public CurrencyExchangeRate(string currencyCode, decimal rate)
        {
            CurrencyCode = currencyCode;
            Rate = rate;
        }

        public string CurrencyCode { get; }
        public decimal Rate { get; }
    }

    public class ExchangeRateInfo
    {
        public ExchangeRateInfo(DateTime asOfTime, IReadOnlyList<CurrencyExchangeRate> exchangeRates)
        {
            AsOfTime = asOfTime;
            ExchangeRates = exchangeRates;
        }

        public DateTime AsOfTime { get; }
        public IReadOnlyList<CurrencyExchangeRate> ExchangeRates { get; }
    }

    public interface IEcbXmlParser
    {
        ExchangeRateInfo ReadExchangeRatesFromEcbXml(XmlDocument ecbXml);
    }

    public class EcbXmlParserImpl : IEcbXmlParser
    {
        public ExchangeRateInfo ReadExchangeRatesFromEcbXml(XmlDocument ecbXml)
        {
            throw new NotImplementedException();
        }
    }
}
