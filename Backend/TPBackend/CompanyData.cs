namespace TPBackend
{
    public class CompanyData
    {
        public CompanyData(CompanyId companyId, string name, CurrencyId baseCurrencyId)
        {
            CompanyId = companyId;
            Name = name;
            BaseCurrencyId = baseCurrencyId;
        }

        public CompanyId CompanyId { get; }
        public string Name { get; }
        public CurrencyId BaseCurrencyId { get; }
    }
}
