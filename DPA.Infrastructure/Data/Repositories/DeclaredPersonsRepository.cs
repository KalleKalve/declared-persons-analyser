using DPA.Application.Repositories;
using DPA.Domain.Models;
using DPA.Domain.Repositories;
using DPA.Infrastructure.Data.ODataClients;
using DPA.Infrastructure.Utilities;
using DPA.Shared.Enums;

namespace DPA.Infrastructure.Data.Repositories
{
    public class DeclaredPersonsRepository : IDeclaredPersonsRepository
    {
        private readonly IDeclaredPersonsODataClient _odataClient;

        public DeclaredPersonsRepository(IDeclaredPersonsODataClient odataClient)
        {
            _odataClient = odataClient;
        }

        public async Task<IEnumerable<DeclaredPersons>> GetDeclaredPersonsAsync(DeclaredPersonsQueryParameters parameters)
        {
            var odataQueryParameters = TranslateToODataQueryParameters(parameters);

            var declaredPersons = await _odataClient.GetDeclaredPersonsAsync(odataQueryParameters);

            var groupedDeclaredPersons = Group(declaredPersons, parameters.Group);

            var orderedDeclaredPersons = Sort(groupedDeclaredPersons, parameters);

            var limitAdjustedDeclaredPersonsData = LimitAdjusterForData.RemoveExcessDataEntries(orderedDeclaredPersons, parameters.Group, parameters.Limit);

            return limitAdjustedDeclaredPersonsData;
        }

        private DeclaredPersonsODataQueryParameters TranslateToODataQueryParameters(DeclaredPersonsQueryParameters parameters)
        {
            var translatedQueryParameters = new DeclaredPersonsODataQueryParameters();

            translatedQueryParameters.Filter = FilterStringBuilder
                .BuildFilterString(parameters.DistrictId, parameters.Year, parameters.Month, parameters.Day, parameters.Group, parameters.Limit);

            translatedQueryParameters.Top = LimitAdjuster
                .AdjustTheLimitOfReturnedEntries(parameters.Limit, parameters);

            translatedQueryParameters.Orderby = new Dictionary<string, bool> // refactor if proper ordering is necessary
            {
                { nameof(DeclaredPersons.year), false},
                { nameof(DeclaredPersons.month), false},
                { nameof(DeclaredPersons.day), false},
            };

            return translatedQueryParameters;
        }

        private IEnumerable<DeclaredPersons> Group(IEnumerable<DeclaredPersons> ungroupedDeclaredPersons, GroupedBy group)
        {
            if (group == GroupedBy.Year)
            {
                var grouped = ungroupedDeclaredPersons
                    .GroupBy(e => new { e.district_name, e.year })
                    .Select(group => new DeclaredPersons
                    {
                        district_name = group.Key.district_name,
                        year = group.Key.year,
                        month = null,
                        day = null,
                        value = group.Sum(e => e.value)
                    });

                return grouped;
            }
            else if (group == GroupedBy.Month)
            {
                var grouped = ungroupedDeclaredPersons
                    .GroupBy(e => new { e.district_name, e.month })
                    .Select(group => new DeclaredPersons
                    {
                        district_name = group.Key.district_name,
                        year = null,
                        month = group.Key.month,
                        day = null,
                        value = group.Sum(e => e.value)
                    });

                return grouped;
            }
            else if (group == GroupedBy.Day)
            {
                var grouped = ungroupedDeclaredPersons
                    .GroupBy(e => new { e.district_name, e.day })
                    .Select(group => new DeclaredPersons
                    {
                        district_name = group.Key.district_name,
                        year = null,
                        month = null,
                        day = group.Key.day,
                        value = group.Sum(e => e.value)
                    });

                return grouped;
            }
            else if (group == GroupedBy.YearMonth)
            {
                var grouped = ungroupedDeclaredPersons
                    .GroupBy(e => new { e.district_name, e.year, e.month })
                    .Select(group => new DeclaredPersons
                    {
                        district_name = group.Key.district_name,
                        year = group.Key.year,
                        month = group.Key.month,
                        day = null,
                        value = group.Sum(e => e.value)
                    });

                return grouped;
            }
            else if (group == GroupedBy.YearDay)
            {
                var grouped = ungroupedDeclaredPersons
                    .GroupBy(e => new { e.district_name, e.year, e.day })
                    .Select(group => new DeclaredPersons
                    {
                        district_name = group.Key.district_name,
                        year = group.Key.year,
                        month = null,
                        day = group.Key.day,
                        value = group.Sum(e => e.value)
                    });

                return grouped;
            }
            else if (group == GroupedBy.MonthDay)
            {
                var grouped = ungroupedDeclaredPersons
                    .GroupBy(e => new { e.district_name, e.month, e.day })
                    .Select(group => new DeclaredPersons
                    {
                        district_name = group.Key.district_name,
                        year = null,
                        month = group.Key.month,
                        day = group.Key.day,
                        value = group.Sum(e => e.value)
                    });

                return grouped;
            }
            else
            {
                return ungroupedDeclaredPersons;
            }
        }

        private IEnumerable<DeclaredPersons> Sort(IEnumerable<DeclaredPersons> groupedDeclaredPersons, DeclaredPersonsQueryParameters parameters)
        {
            return groupedDeclaredPersons.OrderBy(x => x.year).ThenBy(x => x.month).ThenBy(x => x.day); // introduce custom sorting
        }
    }
}
