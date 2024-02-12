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

            Console.WriteLine();

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

            var valuePadding = 
                output.Summary.MaxDrop.Value.ToString().Length > output.Summary.MaxIncrease.Value.ToString().Length 
                ? output.Summary.MaxDrop.Value.ToString().Length : output.Summary.MaxIncrease.Value.ToString().Length;

            PrintSummaryGroupItem("Max drop:", $"{output.Summary.MaxDrop.Value}", $"{output.Summary.MaxDrop.Group}", 24, valuePadding);
            PrintSummaryGroupItem("Max increase:", $"{output.Summary.MaxIncrease.Value}", $"{output.Summary.MaxIncrease.Group}", 24, valuePadding);

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

        private void PrintSummaryItem(string name, string value, int namePadding)
        {
            Console.WriteLine($"{name.PadRight(namePadding)} {value}");
        }

        private void PrintSummaryGroupItem(string name, string value, string group, int namePadding, int valuePadding)
        {
            Console.WriteLine($"{name.PadRight(namePadding)} {value.PadRight(valuePadding)} {group}");
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
