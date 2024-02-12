using DPA.Application.Models.DeclaredPerson;
using DPA.Application.Utilities;
using DPA.Domain.Models;
using DPA.Shared.Enums;

namespace DPA.Application.Services
{
    public class DeclaredPersonsProcessor : IDeclaredPersonsProcessor
    {
        public DeclaredPersonsProcessor()
        {            
        }

        public DeclaredPersonOutput ProcessDeclaredPersons(IEnumerable<DeclaredPersons> data, GroupedBy groupedBy, int limit)
        {
            if(data == null || !data.Any())
            {
                return new DeclaredPersonOutput();
            }            

            var declaredPersons = new DeclaredPersonOutput();
            var maxDrop = new DeclaredPersonMaxDrop();
            var MaxIncrease = new DeclaredPersonMaxIncrease();
            long valueSum = 0;
            var entryCount = 0;
            var max = 0;
            var min = 0;
            var previousValue = 0;
            var change = 0;

            foreach (var entry in data)
            {
                valueSum = valueSum + entry.value;
                max = max > entry.value ? max : entry.value;
                min = min < entry.value ? min : entry.value;
                change = entry.value - previousValue;
                entryCount++;

                if (entryCount == 1) // for the first iteration only
                {
                    max = entry.value;
                    min = entry.value;
                    change = 0;
                }

                declaredPersons.Data.Add(new DeclaredPersonData
                {
                    DistrictName = entry.district_name,
                    Year = entry.year,
                    Month = entry.month,
                    Day = entry.day,
                    Value = entry.value,
                    Change = change,
                });

                maxDrop = maxDrop.Value < change ? maxDrop : new DeclaredPersonMaxDrop { Value = change, Group = GroupNameCreator.Create(entry, groupedBy) };
                MaxIncrease = MaxIncrease.Value > change ? MaxIncrease : new DeclaredPersonMaxIncrease { Value = change, Group = GroupNameCreator.Create(entry, groupedBy) };

                previousValue = entry.value;
            }

            double average = (double)valueSum / entryCount;

            declaredPersons.Summary = new DeclaredPersonSummary
            {
                Max = max,
                Min = min,
                Average = (int)Math.Round(average),
                MaxDrop = maxDrop,
                MaxIncrease = MaxIncrease
            };

            return declaredPersons;
        }
    }
}
