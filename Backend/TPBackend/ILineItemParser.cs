using System;
using System.Collections.Generic;

namespace TPBackend
{
    public interface ILineItemParser
    {
        IReadOnlyList<LineItem> LoadLineItems(string fileName);
    }

    public class LineItemParserImpl : ILineItemParser
    {
        private readonly IJsonLoader jsonLoader;

        public LineItemParserImpl(IJsonLoader jsonLoader)
        {
            this.jsonLoader = jsonLoader;
        }

        public IReadOnlyList<LineItem> LoadLineItems(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
