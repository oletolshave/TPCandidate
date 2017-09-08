using System;
using System.Collections.Generic;

namespace TPBackend
{
    public interface IRepositoryBuilder
    {
        IRepository BuildFromEventsAtPointInTime(IEnumerable<EventBase> events, IReadOnlyList<string> allCurrencyCodes, 
            DateTime asOfTime);
    }

    public class RepositoryBuilderImpl : IRepositoryBuilder
    {
        public IRepository BuildFromEventsAtPointInTime(IEnumerable<EventBase> events, IReadOnlyList<string> allCurrencyCodes, 
            DateTime asOfTime)
        {
            throw new NotImplementedException();
        }
    }
}
