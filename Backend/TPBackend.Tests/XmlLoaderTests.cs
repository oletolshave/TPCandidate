using Xunit;

namespace TPBackend.Tests
{
    public class XmlLoaderTests
    {
        [Fact]
        public void ItCanReadTheXmlFileInTheTestProject()
        {
            var sut = new XmlLoaderImpl();

            var xmlDocument = sut.LoadLocalXmlFile("eurofxref-daily.xml");
            var documentElementName = xmlDocument.DocumentElement.NamespaceURI + ":" + xmlDocument.DocumentElement.LocalName;

            Assert.Equal("http://www.gesmes.org/xml/2002-08-01:Envelope", documentElementName);
        }
    }
}
