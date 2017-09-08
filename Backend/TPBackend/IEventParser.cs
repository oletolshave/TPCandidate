using System.Collections.Generic;

namespace TPBackend
{
    public interface IEventParser
    {
        IReadOnlyList<EventBase> LoadEvents(string fileName);
    }

    public class EventParserImpl : IEventParser
    {
        private readonly IJsonLoader jsonLoader;

        public EventParserImpl(IJsonLoader jsonLoader)
        {
            this.jsonLoader = jsonLoader;
        }

        public IReadOnlyList<EventBase> LoadEvents(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
