using DPA.Application.Models.DeclaredPerson;
using DPA.Domain.Models;
using DPA.Shared.Enums;

namespace DPA.Application.Services
{
    public interface IDeclaredPersonsProcessor
    {
        DeclaredPersonOutput ProcessDeclaredPersons(IEnumerable<DeclaredPersons> data, GroupedBy groupedBy, int limit);
    }
}
