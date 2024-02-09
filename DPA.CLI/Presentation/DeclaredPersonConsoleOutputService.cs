using DPA.Application.Interfaces;
using DPA.Application.Models.DeclaredPerson;
using System.Text;

namespace declared_persons_analyser.Presentation
{
    public class DeclaredPersonConsoleOutputService : IDeclaredPersonConsoleOutputService
    {
        public void PrintOutDeclaredPersonInConsole(DeclaredPersonOutput output)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var tablePaddings = new Paddings(output.Data);

            bool includeYear = output.Data.Any(d => d.Year.HasValue);
            bool includeMonth = output.Data.Any(d => d.Month.HasValue);
            bool includeDay = output.Data.Any(d => d.Day.HasValue);

            int districtNamePadding = Math.Max("District Name".Length, output.Data.Max(d => d.DistrictName?.Length ?? 0)) + 2;
            int yearPadding = "Year".Length + 2;
            int monthPadding = "Month".Length + 2;
            int dayPadding = "Day".Length + 2;
            int valuePadding = Math.Max("Value".Length, output.Data.Max(d => d.Value.ToString().Length)) + 2;
            int changePadding = Math.Max("Change".Length, output.Data.Max(d => d.Change.ToString().Length)) + 2;


            PrintHeader(tablePaddings, includeYear, includeMonth, includeDay);

            foreach (var item in output.Data)
            {
                PrintDataItem(item, tablePaddings, includeYear, includeMonth, includeDay);
            }

            Console.WriteLine();

            PrintSummaryItem("Max:", output.Summary.Max.ToString(), 14);
            PrintSummaryItem("Min:", output.Summary.Min.ToString(), 14);
            PrintSummaryItem("Average:", output.Summary.Average.ToString(), 14);

            Console.WriteLine();

            PrintSummaryItem("Max drop:", $"{output.Summary.MaxDrop.Value} {output.Summary.MaxDrop.Group}", 24);
            PrintSummaryItem("Max increase:", $"{output.Summary.MaxIncrease.Value} {output.Summary.MaxIncrease.Group}", 24);

            Console.WriteLine();
        }

        private void PrintHeader(Paddings paddings, bool includeYear, bool includeMonth, bool includeDay)
        {
            Console.Write("District Name".PadRight(paddings.DistrictName));
            if (includeYear) Console.Write("Year".PadRight(paddings.Year));
            if (includeMonth) Console.Write("Month".PadRight(paddings.Month));
            if (includeDay) Console.Write("Day".PadRight(paddings.Day));
            Console.Write("Value".PadRight(paddings.Value));
            Console.WriteLine("Change".PadRight(paddings.Change));
        }

        private void PrintDataItem(DeclaredPersonData item, Paddings paddings, bool includeYear, bool includeMonth, bool includeDay)
        {
            Console.Write((item.DistrictName ?? "N/A").PadRight(paddings.DistrictName));
            if (includeYear) Console.Write((item.Year?.ToString() ?? "N/A").PadRight(paddings.Year));
            if (includeMonth) Console.Write((item.Month?.ToString() ?? "N/A").PadRight(paddings.Month));
            if (includeDay) Console.Write((item.Day?.ToString() ?? "N/A").PadRight(paddings.Day));
            Console.Write(item.Value.ToString().PadRight(paddings.Value));
            Console.WriteLine(item.Change.ToString().PadRight(paddings.Change));
        }

        private void PrintSummaryItem(string name, string value, int summaryPadding)
        {
            Console.WriteLine($"{name.PadRight(summaryPadding)} {value}");
        }
    }

    internal class Paddings
    {
        public int DistrictName { get; set; }
        public int Year { get; set; } = "Year".Length + 2;
        public int Month { get; set; } = "Month".Length + 2;
        public int Day { get; set; } = "Day".Length + 2;
        public int Value { get; set; }
        public int Change { get; set; }

        public Paddings(IEnumerable<DeclaredPersonData> data)
        {
            DistrictName = Math.Max("District Name".Length, data.Max(d => d.DistrictName?.Length ?? 0)) + 2;
            Value = Math.Max("Value".Length, data.Max(d => d.Value.ToString().Length)) + 2;
            Change = Math.Max("Change".Length, data.Max(d => d.Change.ToString().Length)) + 2;
        }
    }
}
