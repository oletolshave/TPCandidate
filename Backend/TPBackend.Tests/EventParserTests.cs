using System;
using System.Linq;
using Xunit;

namespace TPBackend.Tests
{
    public class EventParserTests
    {
        [Fact]
        public void ItCanLoadAndParseTheEventsFile()
        {
            // Everything takes place in February for no particular reason...
            var minTime = new DateTime(2017, 2, 1, 0, 0, 0, DateTimeKind.Utc);
            var maxTime = minTime.AddMonths(1);

            IJsonLoader jsonLoader = new JsonLoaderImpl();
            IEventParser sut = new EventParserImpl(jsonLoader);
            
            var events = sut.LoadEvents("Events.json");

            Assert.Equal(4, events.Count);

            foreach (var parsedEvent in events)
            {
                Assert.True(minTime <= parsedEvent.Timestamp);
                Assert.True(parsedEvent.Timestamp < maxTime);

                if (parsedEvent is EventCreateCompany eventCreateCompany)
                {
                    Assert.True(eventCreateCompany.CompanyId > 0);
                }
                else if (parsedEvent is EventUpdateCompany eventModifyCompanyBaseCurrency)
                {
                    Assert.True(eventModifyCompanyBaseCurrency.CompanyId > 0);
                }
                else
                    throw new System.Exception("Unexpected event type.");
            }
        }
    }
}
