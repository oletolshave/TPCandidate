using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TPBackend.Tests
{ 
    public class AccountHandlerTests
    {
        [Fact]
        public void ItCanCorrectlySumTheTestData()
        {
            IAccountHandler accountHandler = new AccountHandlerImpl();
            var events = ReadTestEvents();
            var lineItems = ReadTestLineItems();
            var exchangeRates = ReadExchangeRates();
            
            var sumAtFirstDataDKK = accountHandler.SumAmounts(events, lineItems, exchangeRates, new DateTime(2017, 3, 1, 0, 0, 0, DateTimeKind.Utc), "DKK");
            var sumAtFirstDataUSD = accountHandler.SumAmounts(events, lineItems, exchangeRates, new DateTime(2017, 3, 1, 0, 0, 0, DateTimeKind.Utc), "USD");
            
            Assert.Equal(2, sumAtFirstDataDKK.Count);
            Assert.Equal(2, sumAtFirstDataUSD.Count);

            var firstCompanyDKK = sumAtFirstDataDKK.Single(c => c.CompanyId.IdValue == 1);
            var secondCompanyDKK = sumAtFirstDataDKK.Single(c => c.CompanyId.IdValue == 2);

            var secondCompanyUSD = sumAtFirstDataUSD.Single(c => c.CompanyId.IdValue == 2);

            Assert.Equal("ABC Company Inc.", firstCompanyDKK.CompanyName);
            Assert.Equal("GBP", firstCompanyDKK.CompanyCurrencyCode);
            Assert.Equal("EUR", secondCompanyDKK.CompanyCurrencyCode);
            Assert.Equal("EUR", secondCompanyUSD.CompanyCurrencyCode);

            Assert.Equal(0, firstCompanyDKK.Sums.GetAmountInCurrency("EUR"));
            Assert.Equal(100, firstCompanyDKK.Sums.GetAmountInCurrency("DKK"));
            Assert.Equal(5000, firstCompanyDKK.Sums.GetAmountInCurrency("THB"));

            // Converted to GBP - the currency of the company.
            Assert.InRange(firstCompanyDKK.AmountInCompanyCurrency, 127.83M, 127.84M);

            Assert.Equal(12000.33M, secondCompanyDKK.Sums.GetAmountInCurrency("EUR"));

            // Converted amount in USD - the selected base currency for "sumAtFirstDataUSD"
            Assert.InRange(secondCompanyUSD.Sums.AmountInBaseCurrency, 14317.59M, 14317.60M);
        }

        [Fact]
        public void ItAlsoReturnsCompniesWithNoLineItems()
        {
            IAccountHandler accountHandler = new AccountHandlerImpl();

            var extraEvents = new EventBase[] { new EventCreateCompany(new DateTime(2017, 2, 19, 0, 0, 0, DateTimeKind.Utc), 3, "Company AB", "SEK") };

            var events = ReadTestEvents().Concat(extraEvents).ToList();
            var lineItems = ReadTestLineItems();
            var exchangeRates = ReadExchangeRates();

            var sumEUR = accountHandler.SumAmounts(events, lineItems, exchangeRates, new DateTime(2017, 3, 1, 0, 0, 0, DateTimeKind.Utc), "THB");
            var companyAB = sumEUR.Single(c => c.CompanyId.IdValue == 3);
            var companyEUR = sumEUR.Single(c => c.CompanyId.IdValue == 2);

            // Data for all 3 companies should be present - even if one or more companies have no associated line items.
            Assert.Equal(3, sumEUR.Count);
            Assert.Equal("SEK", companyAB.CompanyCurrencyCode);
            Assert.Equal(0, companyAB.AmountInCompanyCurrency);
            Assert.Equal(0, companyAB.Sums.AmountInBaseCurrency);
            Assert.InRange(companyEUR.Sums.AmountInBaseCurrency, 474769.05M, 474769.06M);
        }

        [Fact]
        public void ItLimitsLineItemsByTimestamp()
        {
            IAccountHandler accountHandler = new AccountHandlerImpl();

            var events = ReadTestEvents();
            var lineItems = ReadTestLineItems();
            var exchangeRates = ReadExchangeRates();

            var sumEUR = accountHandler.SumAmounts(events, lineItems, exchangeRates, new DateTime(2017, 2, 2, 0, 0, 0, DateTimeKind.Utc), "EUR");
            var company1 = sumEUR.Single(c => c.CompanyId.IdValue == 1);
            var company2 = sumEUR.Single(c => c.CompanyId.IdValue == 2);

            // Data for all 3 companies should be present - even if one or more companies have no associated line items.
            Assert.Equal(2, sumEUR.Count);

            Assert.Equal("ABC Company Ltd", company1.CompanyName);
            Assert.InRange(company1.AmountInCompanyCurrency, 127.83M, 127.84M);
            Assert.Equal(0, company2.AmountInCompanyCurrency);
            Assert.Equal(0, company2.Sums.AmountInBaseCurrency);
        }

        private IReadOnlyList<LineItem> ReadTestLineItems()
        {
            ILineItemParser lineItemParser = new LineItemParserImpl(new JsonLoaderImpl());
            return lineItemParser.LoadLineItems("SourceData.json");
        }

        private IReadOnlyList<EventBase> ReadTestEvents()
        {
            IEventParser eventParser = new EventParserImpl(new JsonLoaderImpl());
            return eventParser.LoadEvents("Events.json");
        }

        private ExchangeRateInfo ReadExchangeRates()
        {
            IEcbXmlParser ecbXmlParser = new EcbXmlParserImpl();
            IXmlLoader xmlLoader = new XmlLoaderImpl();

            return ecbXmlParser.ReadExchangeRatesFromEcbXml(xmlLoader.LoadLocalXmlFile("eurofxref-daily.xml"));
        }
    }
}
