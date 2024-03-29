﻿using DPA.Domain.Models;
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

        public async Task<IEnumerable<DeclaredPersons>> GetDeclaredPersonsAsync(DeclaredPersonsODataQueryParameters parameters)
        {
            var command = _client.For<DeclaredPersons>("DeclaredPersons");

            if (!string.IsNullOrWhiteSpace(parameters.Select))
            {
                command = command.Select(parameters.Select);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                command = command.Filter(parameters.Filter);
            }

            if (parameters.Top.HasValue)
            {
                command = command.Top(parameters.Top.Value);
            }

            if (parameters.Skip.HasValue)
            {
                command = command.Skip(parameters.Skip.Value);
            }

            if (parameters.Orderby != null)
            {
                command = command.OrderBy(parameters.Orderby);
            }

            var annotations = new ODataFeedAnnotations();
            var result = new List<DeclaredPersons>();

            var entries = await command.FindEntriesAsync(annotations);
            result.AddRange(entries);

            while (annotations.NextPageLink != null)
            {
                var nextPage = await _client.For<DeclaredPersons>("DeclaredPersons").FindEntriesAsync(annotations.NextPageLink, annotations);
                result.AddRange(nextPage);
            }  

            return result;
        }
    }
}
