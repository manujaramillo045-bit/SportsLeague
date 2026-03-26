using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SportsLeague.DataAccess.Repositories
{
    public class RefereeRepository : GenericRepository<Referee>, IRefereeRepository
    {

        public RefereeRepository(LeagueDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Referee>> GetByNationalityAsync(string nationality) // metodo para obtener los árbitros por nacionalidad ya implementado en el repositorio específico de árbitros
        {
            return await _dbSet
            .Where(r => r.Nationality.ToLower() == nationality.ToLower())
            .ToListAsync();
        }
    }
}
