namespace TPBackend
{
    public interface IJsonLoader
    {
        T Load<T>(string fileName);
    }

    public class JsonLoaderImpl : IJsonLoader
    {
        public T Load<T>(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
