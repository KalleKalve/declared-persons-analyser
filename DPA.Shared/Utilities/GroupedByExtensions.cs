using DPA.Shared.Enums;

namespace DPA.Shared.Utilities
{
    public static class GroupedByExtensions
    {
        public static GroupedBy MapStringToEnum(string group)
        {
            if(group == null)
            {
                return GroupedBy.None;
            }

            var groupToLower = group.ToLower().Trim();

            return groupToLower switch
            {
                "y" => GroupedBy.Year,
                "m" => GroupedBy.Month,
                "d" => GroupedBy.Day,
                "ym" => GroupedBy.YearMonth,
                "yd" => GroupedBy.YearDay,
                "md" => GroupedBy.MonthDay,
                _ => GroupedBy.None,
            };
        }
    }
}
