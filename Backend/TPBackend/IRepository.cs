using System.Collections.Generic;
using System.Linq;

namespace TPBackend
{
    public interface IRepository
    {
        string LookupCurrency(CurrencyId currencyId);
        CurrencyId AddCurrency(string currencyCode);
        IReadOnlyList<CurrencyId> AllCurrencyIds { get; }

        CompanyData LookupCompany(CompanyId companyId);
        void AddOrUpdateCompany(CompanyData companyData);
        IReadOnlyList<CompanyId> AllCompanyIds { get; }
    }

    public class RepositoryImpl : IRepository
    {
        public IReadOnlyList<CurrencyId> AllCurrencyIds => throw new System.NotImplementedException();

        public IReadOnlyList<CompanyId> AllCompanyIds => throw new System.NotImplementedException();

        public CurrencyId AddCurrency(string currencyCode)
        {
            throw new System.NotImplementedException();
        }

        public void AddOrUpdateCompany(CompanyData companyData)
        {
            throw new System.NotImplementedException();
        }

        public CompanyData LookupCompany(CompanyId companyId)
        {
            throw new System.NotImplementedException();
        }

        public string LookupCurrency(CurrencyId currencyId)
        {
            throw new System.NotImplementedException();
        }
    }
}
