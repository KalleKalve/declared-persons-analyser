using DPA.Domain.Models;
using DPA.Shared.Enums;
using System.Text;

namespace DPA.Infrastructure.Utilities
{
    public class FilterStringBuilder
    {
        public static string BuildFilterString(int? district, int? year, int? month, int? day, GroupedBy groupedBy, int limit)
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
            else if (groupedBy == GroupedBy.Month) // needs all the data, so no limit from server
            {
                if(limit < 12 && limit != 0)
                {
                    AppendAndIfNeeded(filter);

                    filter.Append("(");

                    for (int i = 1; i <= limit; i++)
                    {
                        AppendOrIfNeeded(filter, i);
                        filter.Append($"{nameof(DeclaredPersons.month)} eq {i}");
                    }

                    filter.Append(")");
                }
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

        private static void AppendOrIfNeeded(StringBuilder filter, int iteration)
        {
            if (iteration > 1)
            {
                filter.Append(" or ");
            }
        }
    }
}
