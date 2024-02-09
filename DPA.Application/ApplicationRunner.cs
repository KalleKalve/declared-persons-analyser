using DPA.Application.Configuration;
using DPA.Application.Interfaces;
using DPA.Application.Repositories;
using DPA.Application.Services;
using DPA.Domain.Repositories;
using DPA.Shared.Utilities;

namespace DPA.Application
{
    public class ApplicationRunner
    {
        private readonly ApplicationRunnerOptions _options;
        private readonly IJsonExportService _jsonExportService;
        private readonly IDeclaredPersonConsoleOutputService _consoleOutputService;
        private readonly IDeclaredPersonsRepository _declaredPersonsRepository;
        private readonly IDeclaredPersonsProcessor _declaredPersonsProcessor;

        public ApplicationRunner(
            ApplicationRunnerOptions options,
            IJsonExportService jsonExportService,
            IDeclaredPersonConsoleOutputService consoleOutputService,
            IDeclaredPersonsRepository declaredPersonsRepository,
            IDeclaredPersonsProcessor declaredPersonsProcessor)
        {
            _options = options;
            _jsonExportService = jsonExportService;
            _consoleOutputService = consoleOutputService;
            _declaredPersonsRepository = declaredPersonsRepository;
            _declaredPersonsProcessor = declaredPersonsProcessor;
        }

        public async Task RunAsync()
        {
            var queryParameters = new DeclaredPersonsQueryParameters
            {
                DistrictId = _options.DistrictId,
                Year = _options.Year,
                Month = _options.Month,
                Day = _options.Day,
                Group = GroupedByExtensions.MapStringToEnum(_options.Group),
                Limit = _options.Limit
            };

            var data = await _declaredPersonsRepository.GetDeclaredPersonsAsync(queryParameters);

            var declaredPersons = _declaredPersonsProcessor.ProcessDeclaredPersons(data, queryParameters.Group);

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
