using System;
using System.Collections.Generic;
using Xunit;

namespace TPBackend.Tests
{
    public abstract class RepositoryBuilderTests<TRepBuilder>
        where TRepBuilder: IRepositoryBuilder, new()
    {
        [Fact]
        public void ItCanGenerateAnEmptyRepository()
        {
            var sut = new TRepBuilder();
            var emptyListOfExtraCurrencyCodes = new List<string>();

            var emptyRepository = sut.BuildFromEventsAtPointInTime(new EventBase[0], emptyListOfExtraCurrencyCodes, 
                new DateTime(2017, 7, 1));
            Assert.Equal(0, emptyRepository.AllCompanyIds.Count);
            Assert.Equal(0, emptyRepository.AllCurrencyIds.Count);
        }

        [Fact]
        public void ItCanBuildForASingleCompany()
        {
            var sut = new TRepBuilder();

            var emptyListOfExtraCurrencyCodes = new List<string>();
            var creationDate = new DateTime(2016, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var id = 19;

            var events = new EventBase[]
            {
                new EventCreateCompany(creationDate, id, "Company", "EUR"),
                new EventCreateCompany(creationDate.AddDays(1), id+1, "Company 2", "EUR")
            };

            var repositoryAfterCreation = sut.BuildFromEventsAtPointInTime(events, emptyListOfExtraCurrencyCodes, 
                creationDate);
            var repositoryBeforeCreation = sut.BuildFromEventsAtPointInTime(events, emptyListOfExtraCurrencyCodes, 
                creationDate.AddSeconds(-1));
            var repositoryAfterSecondCreation = sut.BuildFromEventsAtPointInTime(events, emptyListOfExtraCurrencyCodes, 
                creationDate.AddDays(2));

            var company = repositoryAfterCreation.LookupCompany(new CompanyId(id));

            var currencyCode = repositoryAfterCreation.LookupCurrency(company.BaseCurrencyId);

            Assert.Throws<CompanyNotFoundException>(() => repositoryAfterCreation.LookupCompany(new CompanyId(id + 1)));
            var company2 = repositoryAfterSecondCreation.LookupCompany(new CompanyId(id + 1));

            Assert.Equal("Company", company.Name);
            Assert.Equal("EUR", currencyCode);
            Assert.Equal(0, repositoryBeforeCreation.AllCurrencyIds.Count);
            Assert.Equal(0, repositoryBeforeCreation.AllCompanyIds.Count);
            Assert.Equal(1, repositoryAfterSecondCreation.AllCurrencyIds.Count);
            Assert.Equal(2, repositoryAfterSecondCreation.AllCompanyIds.Count);
        }

        [Fact]
        public void ItCanMergeCurrenciesFromBothEventsAndCurrenctCodeList()
        {
            var sut = new TRepBuilder();

            var listOfExtraCurrencyCodes = new List<string>() { "DKK", "EUR" };
            var creationDate = new DateTime(2016, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var events = new EventBase[]
            {
                new EventCreateCompany(creationDate, 1, "Company", "EUR")
            };

            var repositoryAfterCreation = sut.BuildFromEventsAtPointInTime(events, listOfExtraCurrencyCodes, creationDate);

            Assert.Equal(2, repositoryAfterCreation.AllCurrencyIds.Count);
        }

        [Fact]
        public void ItCanModifyNameForACompany()
        {
            var sut = new TRepBuilder();

            var creationDate = new DateTime(2017, 2, 28, 0, 0, 0, DateTimeKind.Utc);

            var emptyListOfExtraCurrencyCodes = new List<string>();

            var events = new EventBase[]
            {
                new EventUpdateCompany(creationDate.AddDays(1), 1, "Company Inc."),
                new EventCreateCompany(creationDate, 1, "Company", "EUR")
            };

            var repositoryRightAfterCreation = sut.BuildFromEventsAtPointInTime(events, emptyListOfExtraCurrencyCodes, creationDate);
            var repositoryAfterModification = sut.BuildFromEventsAtPointInTime(events, emptyListOfExtraCurrencyCodes, creationDate.AddDays(2));

            var companyNameRightAfterCreation = repositoryRightAfterCreation.LookupCompany(new CompanyId(1)).Name;
            var companyNameAfterModification = repositoryAfterModification.LookupCompany(new CompanyId(1)).Name;

            Assert.Equal(1, repositoryRightAfterCreation.AllCompanyIds.Count);
            Assert.Equal(1, repositoryAfterModification.AllCompanyIds.Count);

            Assert.Equal(1, repositoryRightAfterCreation.AllCurrencyIds.Count);
            Assert.Equal(1, repositoryAfterModification.AllCurrencyIds.Count);

            Assert.Equal("Company", companyNameRightAfterCreation);
            Assert.Equal("Company Inc.", companyNameAfterModification);
        }
    }

    public class RepositoryBuilderImplTests : RepositoryBuilderTests<RepositoryBuilderImpl> { }
}
