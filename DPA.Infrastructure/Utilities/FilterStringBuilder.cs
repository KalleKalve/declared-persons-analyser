using DPA.Domain.Models;
using System.Text;

namespace DPA.Infrastructure.Utilities
{
    public class FilterStringBuilder
    {
        public static string BuildFilterString(int? district, int? year, int? month, int? day)
        {
            StringBuilder filter = new StringBuilder();
            
            if (district.HasValue)
            {
                AppendAndIfNeeded(filter);
                filter.Append($"{nameof(DeclaredPersons.district_id)} eq {district.Value}");
            }

            if (year.HasValue)
            {
                AppendAndIfNeeded(filter);
                filter.Append($"{nameof(DeclaredPersons.year)} eq {year.Value}");
            }

            if (month.HasValue)
            {
                AppendAndIfNeeded(filter);
                filter.Append($"{nameof(DeclaredPersons.month)} eq {month.Value}");
            }

            if (day.HasValue)
            {
                AppendAndIfNeeded(filter);
                filter.Append($"{nameof(DeclaredPersons.day)} eq {day.Value}");
            }

            return filter.ToString();
        }

        private static void AppendAndIfNeeded(StringBuilder filter)
        {
            if (filter.Length > 0)
            {
                filter.Append(" and ");
            }
        }
    }
}
