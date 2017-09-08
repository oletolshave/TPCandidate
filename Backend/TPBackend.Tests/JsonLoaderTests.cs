using System.IO;
using Xunit;

namespace TPBackend.Tests
{
    public class JsonLoaderTests
    {
        [Fact]
        public void ItCanLoadTheEventsTestFile()
        {
            IJsonLoader sut = new JsonLoaderImpl();

            // We simply test that we can load something.
            var loadedObject = sut.Load<object>("Events.json");

            Assert.NotNull(loadedObject);
        }

         [Fact]
         public void ItCannotLoadANonExistingFile()
        {
            IJsonLoader sut = new JsonLoaderImpl();

            Assert.Throws<FileNotFoundException>(() => sut.Load<object>("NonExistingFile.json"));
        }
    }
}
