using System.Xml;

namespace TPBackend
{
    public interface IXmlLoader
    {
        XmlDocument LoadLocalXmlFile(string fileName); 
    }

    public class XmlLoaderImpl : IXmlLoader
    {
        public XmlDocument LoadLocalXmlFile(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
