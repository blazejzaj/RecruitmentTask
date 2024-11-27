namespace TestConsole.Services
{
    public interface IFileService
    {
        void AppendToFile(string filePath, string content);
    }
}
