using System;

namespace TPBackend
{
    public abstract class EventBase
    {
        public EventBase(DateTime timestamp)
        {
            if (timestamp.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Timestamp must be specified in UTC", nameof(timestamp));
            }

            Timestamp = timestamp;
        }

        public DateTime Timestamp { get; }
    }

    public class EventCreateCompany: EventBase
    {
        public EventCreateCompany(DateTime timestamp, int companyId, string companyName, string baseCurrencyCode) : base(timestamp) {
            CompanyId = companyId;
            CompanyName = companyName;
            BaseCurrencyCode = baseCurrencyCode;
        }

        public int CompanyId { get; }
        public string CompanyName { get; }
        public string BaseCurrencyCode { get; }
    }

    public class EventUpdateCompany : EventBase
    {
        public EventUpdateCompany(DateTime timestamp, int companyId, string updatedName) : base(timestamp) {
            CompanyId = companyId;
            UpdatedName = updatedName;
        }

        public int CompanyId { get; }
        public string UpdatedName { get; }
    }
}
