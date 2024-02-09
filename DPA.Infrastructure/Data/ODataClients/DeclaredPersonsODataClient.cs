using DPA.Domain.Models;
using Simple.OData.Client;

namespace DPA.Infrastructure.Data.ODataClients
{
    public class DeclaredPersonsODataClient : IDeclaredPersonsODataClient
    {
        private readonly ODataClient _client;

        public DeclaredPersonsODataClient(string serviceUrl)
        {
            var settings = new ODataClientSettings(new Uri(serviceUrl));
            _client = new ODataClient(settings);
        }

        public async Task<IEnumerable<DeclaredPerson>> GetDeclaredPersonsAsync(DeclaredPersonsODataQueryParameters parameters)
        {
            var command = _client.For<DeclaredPerson>("DeclaredPersons");

            //if (!string.IsNullOrWhiteSpace(parameters.Filter))
            //{
            //    command = command.Filter(parameters.Filter);
            //}

            var result =  await command.FindEntriesAsync();

            return result;
        }
    }
}
