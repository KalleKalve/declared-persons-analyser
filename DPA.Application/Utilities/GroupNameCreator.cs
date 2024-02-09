using DPA.Domain.Models;
using DPA.Shared.Enums;

namespace DPA.Application.Utilities
{
    public static class GroupNameCreator
    {
        public static string Create(DeclaredPersons entry, GroupedBy groupBy)
        {
            switch (groupBy)
            {
                case GroupedBy.Year:
                    return entry.year?.ToString() ?? "Unknown Year";
                case GroupedBy.Month:
                    return entry.month?.ToString() ?? "Unknown Month";
                case GroupedBy.Day:
                    return entry.day?.ToString() ?? "Unknown Day";
                case GroupedBy.YearMonth:
                    return (entry.year?.ToString() ?? "Unknown Year") + "." +
                           (entry.month?.ToString() ?? "Unknown Month");
                case GroupedBy.YearDay:
                    return (entry.year?.ToString() ?? "Unknown Year") + "." +
                           (entry.day?.ToString() ?? "Unknown Day");
                case GroupedBy.MonthDay:
                    return (entry.month?.ToString() ?? "Unknown Month") + "." +
                           (entry.day?.ToString() ?? "Unknown Day");
                default:
                    return string.Empty;
            }
        }
    }
}
