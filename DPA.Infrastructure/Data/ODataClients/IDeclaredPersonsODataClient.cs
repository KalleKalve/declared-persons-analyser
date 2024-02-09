using DPA.Domain.Models;
using DPA.Domain.Repositories;

namespace DPA.Infrastructure.Data.ODataClients
{
    public interface IDeclaredPersonsODataClient
    {
        Task<IEnumerable<DeclaredPerson>> GetDeclaredPersonsAsync(DeclaredPersonsODataQueryParameters parameters);
    }
}
