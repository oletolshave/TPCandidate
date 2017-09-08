using Xunit;

namespace TPBackend.Tests
{
    public abstract class RepositoryTests<TRepos>
        where TRepos: IRepository, new()
    {
        [Fact]
        public void ItCanLoadSomeCurrenciesAndReadThemBack()
        {
            var sut = new TRepos();

            var idEUR = sut.AddCurrency("EUR");
            var idUSD = sut.AddCurrency("USD");
            var idDKK = sut.AddCurrency("DKK");

            var codeEUR = sut.LookupCurrency(idEUR);
            var codeDKK = sut.LookupCurrency(idDKK);
            var codeUSD = sut.LookupCurrency(idUSD);

            Assert.Equal("EUR", codeEUR);
            Assert.Equal("DKK", codeDKK);
            Assert.Equal("USD", codeUSD);
        }

        [Fact]
        public void ItThrowsOnCurrenciesNotFound()
        {
            var sut = new TRepos();

            // No ids should be present initially in the repository, so we expect a CurrencyNotFoundException.
            Assert.Throws<CurrencyNotFoundException>(() =>
            {
                sut.LookupCurrency(new CurrencyId(0));
            });
        }

        [Fact]
        public void ItCanLoadSomeCompaniesAndReadThemBack()
        {
            var sut = new TRepos();

            var idEUR = sut.AddCurrency("EUR");

            var companyDK = new CompanyData(new CompanyId(1), "DK Company A/S", idEUR);
            sut.AddOrUpdateCompany(companyDK);

            var companySE = new CompanyData(new CompanyId(2), "SE Company AB", idEUR);
            sut.AddOrUpdateCompany(companySE);

            var verifyDK = sut.LookupCompany(new CompanyId(1));
            var verifySE = sut.LookupCompany(new CompanyId(2));

            Assert.Equal(companyDK.Name, verifyDK.Name);
            Assert.Equal(companySE.Name, verifySE.Name);
        }        

        [Fact]
        public void ItThrowsOnNonExistingCurrency()
        {
            var sut = new TRepos();

            Assert.Throws<CurrencyNotFoundException>(() => sut.AddOrUpdateCompany(
                new CompanyData(new CompanyId(1), "Company", new CurrencyId(1))));
        }
    }

    public class RepositoryImplTest : RepositoryTests<RepositoryImpl> { }
}
