namespace TestConsole.Services
{
    public class FileService : IFileService
    {
        public void AppendToFile(string filePath, string content)
        {
            File.AppendAllText(filePath, content + "\n");
        }
    }
}
