using System;
using System.Collections.Generic;

namespace TPBackend
{
    public interface ICompanyAccount
    {
        CompanyId CompanyId { get; }
        string CompanyName { get; }

        string CompanyCurrencyCode { get; }
        decimal AmountInCompanyCurrency { get; }

        IAccountSum Sums { get; }
    }

    public interface IAccountSum
    {
        IReadOnlyList<CurrencyId> AllCurrencyIds { get; }
        decimal GetAmountInCurrency(CurrencyId currencyId);
        decimal GetAmountInCurrency(string currencyCode);
        decimal AmountInBaseCurrency { get; }
    }

    public interface IAccountHandler
    {
        IReadOnlyList<ICompanyAccount> SumAmounts(IReadOnlyList<EventBase> events, IReadOnlyList<LineItem> lineItems, 
            ExchangeRateInfo exchangeRates, DateTime asOfTime, string baseCurrencyCode);
    }

    public class AccountHandlerImpl : IAccountHandler
    {
        public IReadOnlyList<ICompanyAccount> SumAmounts(IReadOnlyList<EventBase> events, IReadOnlyList<LineItem> lineItems, 
            ExchangeRateInfo exchangeRates, DateTime asOfTime, string baseCurrencyCode)
        {
            throw new NotImplementedException();
        }
    }
}
