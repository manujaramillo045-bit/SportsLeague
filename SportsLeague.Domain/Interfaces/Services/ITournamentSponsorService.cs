using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ITournamentSponsorService
    {
        Task AssignAsync(int sponsorId, int tournamentId, decimal contractAmount);

        Task<IEnumerable<TournamentSponsor>> GetBySponsorAsync(int sponsorId);

        Task RemoveAsync(int sponsorId, int tournamentId);
    }
}