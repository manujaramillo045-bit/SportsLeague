using Microsoft.AspNetCore.Mvc;
using SportsLeague.Domain.Services;

namespace SportsLeague.API.Controllers
{
    [Route("api/match/{matchId}/lineup")]
    [ApiController]
    public class MatchLineupController : ControllerBase
    {
        private readonly MatchLineupService _matchLineupService;

        public MatchLineupController(MatchLineupService matchLineupService)
        {
            _matchLineupService = matchLineupService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToLineup(int matchId, [FromBody] MatchLineupRequestDTO requestDto)
        {
            try
            {
                // Mapeo: RequestDTO de API -> RequestDTO del Service
                var serviceRequest = new Domain.Services.MatchLineupRequestDTO
                {
                    PlayerId = requestDto.PlayerId,
                    IsStarter = requestDto.IsStarter,
                    Position = requestDto.Position
                };

                var result = await _matchLineupService.AddToLineupAsync(matchId, serviceRequest);

                // Mapeo: ResponseDTO del Service -> ResponseDTO de API
                var responseDto = new MatchLineupResponseDTO
                {
                    Id = result.Id,
                    MatchId = result.MatchId,
                    PlayerId = result.PlayerId,
                    PlayerName = result.PlayerName,
                    TeamName = result.TeamName,
                    IsStarter = result.IsStarter,
                    Position = result.Position
                };

                return StatusCode(201, responseDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLineup(int matchId)
        {
            var result = await _matchLineupService.GetLineupByMatchAsync(matchId);

            // Mapeo: Lista de ResponseDTO del Service -> Lista de ResponseDTO de API
            var responseDtos = result.Select(r => new MatchLineupResponseDTO
            {
                Id = r.Id,
                MatchId = r.MatchId,
                PlayerId = r.PlayerId,
                PlayerName = r.PlayerName,
                TeamName = r.TeamName,
                IsStarter = r.IsStarter,
                Position = r.Position
            });

            return Ok(responseDtos);
        }

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetLineupByTeam(int matchId, int teamId)
        {
            var result = await _matchLineupService.GetLineupByMatchAndTeamAsync(matchId, teamId);

            // Mapeo: Lista de ResponseDTO del Service -> Lista de ResponseDTO de API
            var responseDtos = result.Select(r => new MatchLineupResponseDTO
            {
                Id = r.Id,
                MatchId = r.MatchId,
                PlayerId = r.PlayerId,
                PlayerName = r.PlayerName,
                TeamName = r.TeamName,
                IsStarter = r.IsStarter,
                Position = r.Position
            });

            return Ok(responseDtos);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromLineup(int matchId, int id)
        {
            try
            {
                await _matchLineupService.RemoveFromLineupAsync(matchId, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}