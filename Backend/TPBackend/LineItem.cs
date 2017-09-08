using System;

namespace TPBackend
{
    public class LineItem
    {
        public LineItem(DateTime timestamp, int companyId, string currencyCode, decimal amount) {
            if (timestamp.Kind != DateTimeKind.Utc)
                throw new ArgumentException("Timestamp must be in UTC.", nameof(timestamp));

            Timestamp = timestamp;
            CompanyId = companyId;
            CurrencyCode = currencyCode;
            Amount = amount;
        }

        public DateTime Timestamp { get; }
        public int CompanyId { get; }
        public string CurrencyCode { get; }
        public decimal Amount { get; }
    }
}
