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

            if (!string.IsNullOrWhiteSpace(parameters.Select))
            {
                command = command.Select(parameters.Select);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                command = command.Filter(parameters.Filter);
            }

            if (parameters.Top != null)
            {
                command = command.Top(parameters.Top.Value);
            }

            if (parameters.Skip != null)
            {
                command = command.Skip(parameters.Skip.Value);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Orderby))
            {
                if(parameters.OrderByDesc)
                {
                    command = command.OrderByDescending(parameters.Orderby);
                }
                else
                {
                    command = command.OrderBy(parameters.Orderby);
                }                
            }

            var result =  await command.FindEntriesAsync();

            return result;
        }
    }
}
