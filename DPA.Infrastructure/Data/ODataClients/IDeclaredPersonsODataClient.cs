using DPA.Domain.Models;

namespace DPA.Infrastructure.Data.ODataClients
{
    public interface IDeclaredPersonsODataClient
    {
        Task<IEnumerable<DeclaredPersons>> GetDeclaredPersonsAsync(DeclaredPersonsODataQueryParameters parameters);
    }
}
