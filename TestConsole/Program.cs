using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using TestConsole.Configuration;
using TestConsole.Interfaces;
using TestConsole.Services;

namespace TestConsole
{
    class Program
    {
        private readonly IHttpService _httpService;
        private readonly IFileService _fileService;

        public Program(IHttpService httpService, IFileService fileService)
        {
            _httpService = httpService;
            _fileService = fileService;
        }

        public async Task RunAsync()
        {
            string url = "https://catfact.ninja/fact";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "output.txt");

            Console.WriteLine("Naciśnij Enter, aby dodać nową linię do pliku.");
            Console.WriteLine("Naciśnij 'O', aby otworzyć plik w Notatniku.");
            Console.WriteLine("Naciśnij 'Esc', aby zakończyć program.");

            while (true)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.Enter:
                        try
                        {
                            var data = await _httpService.FetchDataAsync(url);
                            _fileService.AppendToFile(filePath, data);
                            Console.WriteLine($"Dane zostały zapisane w pliku: output.txt");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                        }
                        break;

                    case ConsoleKey.O:
                        try
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = "notepad.exe",
                                Arguments = filePath
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Wystąpił błąd przy otwieraniu pliku: {ex.Message}");
                        }
                        break;

                    case ConsoleKey.Escape:
                        Console.WriteLine("Zakończenie aplikacji.");
                        return;

                    default:
                        break;
                }
            }
        }

        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.ConfigureDependencyInjection();

            var serviceProvider = services.BuildServiceProvider();

            var program = ActivatorUtilities.CreateInstance<Program>(serviceProvider);

            await program.RunAsync();
        }
    }
}
