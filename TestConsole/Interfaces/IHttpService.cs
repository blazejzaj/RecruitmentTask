namespace TestConsole.Interfaces
{
    public interface IHttpService
    {
        Task<string> FetchDataAsync(string url);
    }
}
