using declared_persons_analyser.Presentation;
using DPA.Application;
using DPA.Application.Configuration;
using DPA.Application.Interfaces;
using DPA.Application.Repositories;
using DPA.Application.Services;
using DPA.Infrastructure.Data.ODataClients;
using DPA.Infrastructure.Data.Repositories;
using DPA.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Parsing;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var applicationOptions = GetCommandLineOptions(args, configuration);

        if (applicationOptions == null) return;


        using IHost host = CreateHostBuilder(applicationOptions, configuration).Build();

        using var scope = host.Services.CreateScope();
        
        var services = scope.ServiceProvider;

        try
        {
            var app = services.GetRequiredService<ApplicationRunner>();

            await app.RunAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        await host.RunAsync();
    }

    static IHostBuilder CreateHostBuilder(ApplicationRunnerOptions options, IConfiguration configuration)
    {

        return Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                var odataServiceUrl = hostContext.Configuration.GetValue<string>("ODataServiceUrl");

                if(!string.IsNullOrEmpty(options.Source))
                {
                    odataServiceUrl = options.Source;
                }

                services.AddSingleton<IDeclaredPersonsODataClient>(new DeclaredPersonsODataClient(odataServiceUrl));
                services.AddSingleton<IDeclaredPersonsRepository, DeclaredPersonsRepository>();
                services.AddSingleton(options);
                services.AddTransient<ApplicationRunner>();
                services.AddSingleton<IJsonExportService, JsonExportService>();
                services.AddSingleton<IDeclaredPersonConsoleOutputService, DeclaredPersonConsoleOutputService>();
                services.AddTransient<IDeclaredPersonsProcessor, DeclaredPersonsProcessor>();
            });
    }

    private static ApplicationRunnerOptions GetCommandLineOptions(string[] args, IConfigurationRoot configuration)
    {
        var defaultLimit = configuration.GetValue<int>("DefaultReturnLimit");

        var rootCommand = new RootCommand("Declared Persons Analyser");

        var sourceOption = new Option<string>("-source", "Service URL");
        var districtOption = new Option<int>("-district", "District Id");
        var yearOption = new Option<int?>("-year", () => null, "Year");
        var monthOption = new Option<int?>("-month", () => null, "Month");
        var dayOption = new Option<int?>("-day", () => null, "Day");
        var limitOption = new Option<int>("-limit", () => defaultLimit, $"Limit the amount of entries returned. Default is {defaultLimit}.");
        var groupOption = new Option<string>("-group", "Grouping mode (possible values: y, m, d, ym, yd, md)");
        var outOption = new Option<string>("-out", "Output JSON file name (including .json)");

        districtOption.IsRequired = true;

        rootCommand.AddOption(sourceOption);
        rootCommand.AddOption(districtOption);
        rootCommand.AddOption(yearOption);
        rootCommand.AddOption(monthOption);
        rootCommand.AddOption(dayOption);
        rootCommand.AddOption(limitOption);
        rootCommand.AddOption(groupOption);
        rootCommand.AddOption(outOption);

        var parseResult = rootCommand.Parse(args);

        if (parseResult.Errors.Any())
        {
            foreach (var error in parseResult.Errors)
            {
                Console.Error.WriteLine(error.ToString());
            }

            return null;
        }

        var options =  new ApplicationRunnerOptions
        {
            Source = parseResult.GetValueForOption<string>(sourceOption),
            DistrictId = parseResult.GetValueForOption<int>(districtOption),
            Year = parseResult.GetValueForOption<int?>(yearOption),
            Month = parseResult.GetValueForOption<int?>(monthOption),
            Day = parseResult.GetValueForOption<int?>(dayOption),
            Limit = parseResult.GetValueForOption<int>(limitOption),
            Group = parseResult.GetValueForOption<string>(groupOption),
            OutputFileName = parseResult.GetValueForOption<string>(outOption)
        };

        return options;
    }
}