using DPA.Domain.Repositories;
using DPA.Shared.Enums;

namespace DPA.Infrastructure.Utilities
{
    public class LimitAdjuster
    {
        public static int? AdjustTheLimitOfReturnedEntries(int limit, DeclaredPersonsQueryParameters parameters)
        {
            /// Since the client seems to be V3, not v4 Odata, This is where the code for adjusting returned entry count is.
            /// Data aggregation and grouping seems like is not possible server side atm, so in order to get the -limit command working as expected, 
            /// this method contains code getting enough entires from the server to later do the aggregation and grouping client side and use -limit value 
            /// against the final processed data.

            var maxDaysInAYear = 366;
            var maxDaysinAMonth = 31;

            if (parameters.Group == GroupedBy.Year)
            {
                var adjustedLimit = limit * maxDaysInAYear;

                return adjustedLimit;
            }
            else if (parameters.Group == GroupedBy.Month)
            {
                return null; // no limit. Need all values for the months. See filters to limit what months.
            }
            else if (parameters.Group == GroupedBy.YearMonth)
            {
                var adjustedLimit = limit * maxDaysinAMonth;

                return adjustedLimit;
            }
            else
            {
                return limit;
            }
        }
    }
}
