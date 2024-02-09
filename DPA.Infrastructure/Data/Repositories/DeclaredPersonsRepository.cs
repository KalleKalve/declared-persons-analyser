using DPA.Application.Repositories;
using DPA.Domain.Models;
using DPA.Domain.Repositories;
using DPA.Infrastructure.Data.ODataClients;

namespace DPA.Infrastructure.Data.Repositories
{
    public class DeclaredPersonsRepository : IDeclaredPersonsRepository
    {
        private readonly IDeclaredPersonsODataClient _odataClient;

        public DeclaredPersonsRepository(IDeclaredPersonsODataClient odataClient)
        {
            _odataClient = odataClient;
        }

        public async Task<IEnumerable<DeclaredPerson>> GetDeclaredPersonsAsync(DeclaredPersonsQueryParameters parameters)
        {
            var odataQueryParameters = TranslateToODataQueryParameters(parameters);

            var declaredPersons = await _odataClient.GetDeclaredPersonsAsync(odataQueryParameters);

            return declaredPersons;
        }

        private DeclaredPersonsODataQueryParameters TranslateToODataQueryParameters(DeclaredPersonsQueryParameters parameters)
        {
            var translatedQueryParameters = new DeclaredPersonsODataQueryParameters();

            translatedQueryParameters.Top = parameters.Limit;

            return translatedQueryParameters;
        }
    }
}
