using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentRepository : IGenericRepository<Tournament>
    {
        Task<IEnumerable<Tournament>> GetByStatusAsync(TournamentStatus status); // obtiene todos los torneos por estado y devuelve una lista de torneos por estado
        Task<Tournament?> GetByIdWithTeamsAsync(int id); // obtiene un torneo por id con la información de los equipos participantes, y devuelve un torneo con la información de los equipos participantes, o null si no se encuentra ningún torneo con ese id.
    }
}
