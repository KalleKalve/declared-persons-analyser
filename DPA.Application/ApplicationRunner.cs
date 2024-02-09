using DPA.Application.Configuration;
using DPA.Application.Interfaces;
using DPA.Application.Models.DeclaredPerson;

namespace DPA.Application
{
    public class ApplicationRunner
    {
        private readonly ApplicationRunnerOptions _options;
        private readonly IJsonExportService _jsonExportService;
        private readonly IDeclaredPersonConsoleOutputService _consoleOutputService;
        //private readonly 

        public ApplicationRunner(ApplicationRunnerOptions options, IJsonExportService jsonExportService, IDeclaredPersonConsoleOutputService consoleOutputService)
        {
            _options = options;
            _jsonExportService = jsonExportService;
            _consoleOutputService = consoleOutputService;
        }

        public async Task RunAsync()
        {
            // Application logic here


            var declaredPersons = new DeclaredPersonOutput
            {
                Data = new List<DeclaredPersonData>
            {
                new DeclaredPersonData { DistrictName = "District 1234234234234234", Year = 2021, Month = 12, Value = 100000000, Change = 5 },
                // Add more data items...
            },
                Summary = new DeclaredPersonSummary
                {
                    Max = 2003434,
                    Min = 50234234,
                    Average = 1253434,
                    MaxDrop = new DeclaredPersonMaxDrop { Value = -200000, Group = "2021.12" },
                    MaxIncrease = new DeclaredPersonMaxIncrease { Value = 30345345, Group = "2021.11" }
                }
            };

            _consoleOutputService.PrintOutDeclaredPersonInConsole(declaredPersons);


            if (!string.IsNullOrWhiteSpace(_options.OutputFileName))
            {
                await _jsonExportService
                    .ExportToJsonFileAsync(declaredPersons, _options.OutputFileName)
                    .ConfigureAwait(false);
            }
        }
    }
}
