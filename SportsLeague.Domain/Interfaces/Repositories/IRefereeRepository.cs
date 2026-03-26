using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IRefereeRepository : IGenericRepository<Referee>
    {
       // solo va a tener un metodo que es el getbynational quee es obtener todos los arbitros por nacionalidad
        Task<IEnumerable<Referee>> GetByNationalityAsync(string nationality);
    }
}
