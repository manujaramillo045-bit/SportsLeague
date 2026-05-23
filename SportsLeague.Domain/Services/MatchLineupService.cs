using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupRequestDTO
    {
        public int PlayerId { get; set; }
        public bool IsStarter { get; set; }
        public string Position { get; set; } = string.Empty;
    }

    public class MatchLineupResponseDTO
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public bool IsStarter { get; set; }
        public string Position { get; set; } = string.Empty;
    }

    public class MatchLineupService
    {
        private readonly IMatchLineupRepository _matchLineupRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;

        public MatchLineupService(
            IMatchLineupRepository matchLineupRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository)
        {
            _matchLineupRepository = matchLineupRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
        }

        public async Task<MatchLineupResponseDTO> AddToLineupAsync(int matchId, MatchLineupRequestDTO dto)
        {
            var match = await _matchRepository.GetByIdAsync(matchId);
            if (match == null)
                throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            var player = await _playerRepository.GetByIdAsync(dto.PlayerId);
            if (player == null)
                throw new KeyNotFoundException($"No se encontró el jugador con ID {dto.PlayerId}");

            if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
                throw new InvalidOperationException("El jugador no pertenece a ninguno de los equipos del partido");

            var exists = await _matchLineupRepository.ExistsByMatchAndPlayerAsync(matchId, dto.PlayerId);
            if (exists)
                throw new InvalidOperationException("El jugador ya está registrado en la alineación de este partido");

            if (dto.IsStarter)
            {
                var startersCount = await _matchLineupRepository.CountStartersByMatchAndTeamAsync(matchId, player.TeamId);
                if (startersCount >= 11)
                    throw new InvalidOperationException("El equipo ya tiene 11 titulares registrados en este partido");
            }

            if (match.Status != MatchStatus.Scheduled)
                throw new InvalidOperationException("Solo se pueden registrar alineaciones en partidos Scheduled");

            var matchLineup = new MatchLineup
            {
                MatchId = matchId,
                PlayerId = dto.PlayerId,
                IsStarter = dto.IsStarter,
                Position = dto.Position,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _matchLineupRepository.AddAsync(matchLineup);
            await _matchLineupRepository.SaveChangesAsync();

            var playerWithTeam = await GetPlayerWithTeamAsync(dto.PlayerId);

            return new MatchLineupResponseDTO
            {
                Id = matchLineup.Id,
                MatchId = matchLineup.MatchId,
                PlayerId = matchLineup.PlayerId,
                PlayerName = $"{playerWithTeam?.FirstName} {playerWithTeam?.LastName}" ?? "N/A",
                TeamName = playerWithTeam?.Team?.Name ?? "Sin equipo",
                IsStarter = matchLineup.IsStarter,
                Position = matchLineup.Position
            };
        }

        public async Task<IEnumerable<MatchLineupResponseDTO>> GetLineupByMatchAsync(int matchId)
        {
            var lineups = await _matchLineupRepository.GetByMatchAsync(matchId);

            return lineups.Select(ml => new MatchLineupResponseDTO
            {
                Id = ml.Id,
                MatchId = ml.MatchId,
                PlayerId = ml.PlayerId,
                PlayerName = $"{ml.Player?.FirstName} {ml.Player?.LastName}" ?? "N/A",
                TeamName = ml.Player?.Team?.Name ?? "N/A",
                IsStarter = ml.IsStarter,
                Position = ml.Position
            });
        }

        public async Task<IEnumerable<MatchLineupResponseDTO>> GetLineupByMatchAndTeamAsync(int matchId, int teamId)
        {
            var lineups = await _matchLineupRepository.GetByMatchAndTeamAsync(matchId, teamId);

            return lineups.Select(ml => new MatchLineupResponseDTO
            {
                Id = ml.Id,
                MatchId = ml.MatchId,
                PlayerId = ml.PlayerId,
                PlayerName = $"{ml.Player?.FirstName} {ml.Player?.LastName}" ?? "N/A",
                TeamName = ml.Player?.Team?.Name ?? "N/A",
                IsStarter = ml.IsStarter,
                Position = ml.Position
            });
        }

        public async Task RemoveFromLineupAsync(int matchId, int lineupId)
        {
            await _matchLineupRepository.DeleteAsync(lineupId);
            await _matchLineupRepository.SaveChangesAsync();
        }

        private async Task<Player?> GetPlayerWithTeamAsync(int playerId)
        {
          
            var allPlayers = await _playerRepository.GetAllAsync();
            return allPlayers.FirstOrDefault(p => p.Id == playerId);
        }
    }
}