using DPA.Domain.Models;
using DPA.Shared.Enums;

namespace DPA.Infrastructure.Utilities
{
    public class LimitAdjusterForData
    {
        public static IEnumerable<DeclaredPersons> RemoveExcessDataEntries(IEnumerable<DeclaredPersons> data, GroupedBy groupedBy, int limit)
        {           

            if (groupedBy == GroupedBy.Year)
            {
                var firstYear = data.First().year;

                var lastYear = firstYear + limit - 1;

                return data.Where(x => x.year <= lastYear.Value);

            }
            else if (groupedBy == GroupedBy.Month)
            {

                var firstMonth = data.First().month;

                var lastMonth = firstMonth + limit - 1;

                return data.Where(x => x.month <= lastMonth.Value);

            }
            else if (groupedBy == GroupedBy.YearMonth)
            {

                var firstYear = data.First().year;

                var firstMonth = data.First().month;

                var lastMonth = ((firstMonth + (limit - 1) - 1) % 12) + 1;

                var lastYear = firstYear + ((firstMonth + limit - 1) / 12);

                return data.Where(x => x.year < lastYear.Value || (x.year == lastYear && x.month <= lastMonth));

            }
            else
            {
                return data;
            }
        }
    }
}
