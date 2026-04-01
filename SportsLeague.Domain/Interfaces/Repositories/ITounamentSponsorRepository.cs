using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentSponsorRepository : IGenericRepository<TournamentSponsor>
    {
        // Obtener relación específica (para validar duplicados)
        Task<TournamentSponsor?> GetBySponsorAndTournamentAsync(int sponsorId, int tournamentId);

        // Listar todos los torneos de un sponsor
        Task<IEnumerable<TournamentSponsor>> GetBySponsorAsync(int sponsorId);
    }
}