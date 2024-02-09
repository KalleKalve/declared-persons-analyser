using DPA.Domain.Models;
using DPA.Domain.Repositories;

namespace DPA.Application.Repositories
{
    public interface IDeclaredPersonsRepository
    {
        Task<IEnumerable<DeclaredPerson>> GetDeclaredPersonsAsync(DeclaredPersonsQueryParameters parameters);
    }
}
