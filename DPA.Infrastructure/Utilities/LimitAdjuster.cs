using DPA.Domain.Repositories;

namespace DPA.Infrastructure.Utilities
{
    public class LimitAdjuster
    {
        public static int AdjustTheLimitOfReturnedEntries(int limit, DeclaredPersonsQueryParameters groupby)
        {
            /// Since the client seems to be V3, not v4 Odata, This is where the code for adjusting returned entries with OData Top would be.
            /// Data aggregation and grouping seems like is not possible server side atm, so in order to get the limit working as expected, 
            /// this method could contain code getting enough entires to later do the aggregation and full grouping client side and then 
            /// it would be possible to return expected limit.

            return limit;
        }
    }
}
