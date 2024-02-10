using DPA.Domain.Repositories;

namespace DPA.Infrastructure.Utilities
{
    public class LimitAdjuster
    {
        public static int AdjustTheLimitOfReturnedEntries(int limit, DeclaredPersonsQueryParameters parameters)
        {
            /// Since the client seems to be V3, not v4 Odata, This is where the code for adjusting returned entries with OData Top would be.
            /// Data aggregation and grouping seems like is not possible server side atm, so in order to get the limit working as expected, 
            /// this method could contain code getting enough entires to later do the aggregation and full grouping client side and then 
            /// it would be possible to return expected limit.
            /// 
            /// Solution in this case could be to use parameters like grouping and filter values to determine how many entries are needed 
            /// to get required amount of data to do calculations. 
            /// 
            /// Example: -limit 4 - group ym, taking month first, one could calculate the minimum Top value of the Odata query to get required data.
            /// 4 x 31(max days in month) = Top parameter value. Later apply -limit 4 to proccesed data.
            /// 
            /// If the Adjusted Limit becomes too high, on OData client side there could be pagination implemented to receive data in large batches, but not all at once.

            return limit;
        }
    }
}
