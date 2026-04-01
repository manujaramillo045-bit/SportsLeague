using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Reponse;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _sponsorService;
        private readonly ITournamentSponsorService _tournamentSponsorService;
        private readonly IMapper _mapper;

        public SponsorController(
            ISponsorService sponsorService,
            ITournamentSponsorService tournamentSponsorService,
            IMapper mapper)
        {
            _sponsorService = sponsorService;
            _tournamentSponsorService = tournamentSponsorService;
            _mapper = mapper;
        }

        // GET ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SponsorResponseDTO>>> GetAll()
        {
            var sponsors = await _sponsorService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SponsorResponseDTO>>(sponsors));
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<SponsorResponseDTO>> GetById(int id)
        {
            var sponsor = await _tournamentSponsorService.GetBySponsorAsync(id);

            if (sponsor == null)
                return NotFound(new { message = $"Sponsor con ID {id} no encontrado" });

            return Ok(_mapper.Map<SponsorResponseDTO>(sponsor));
        }

        // CREATE
        [HttpPost]
        public async Task<ActionResult<SponsorResponseDTO>> Create(SponsorRequestDTO dto)
        {
            try
            {
                var sponsor = _mapper.Map<Sponsor>(dto);
                var created = await _sponsorService.CreateAsync(sponsor);
                var responseDto = _mapper.Map<SponsorResponseDTO>(created);

                return CreatedAtAction(nameof(GetById), new { id = responseDto.Id }, responseDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, SponsorRequestDTO dto)
        {
            try
            {
                var sponsor = _mapper.Map<Sponsor>(dto);
                await _sponsorService.UpdateAsync(id, sponsor);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _sponsorService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }

        // RELACIÓN N:M

        // vincular sponsor a torneo
        [HttpPost("{id}/tournaments")]
        public async Task<ActionResult> AssignTournament(int id, TournamentSponsorRequestDTO dto)
        {
            try
            {
                await _tournamentSponsorService.AssignAsync(id, dto.TournamentId, dto.ContractAmount);
                return Ok(new { message = "Sponsor vinculado al torneo correctamente" });
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }

        // GET : listar torneos de un sponsor
        [HttpGet("{id}/tournaments")]
        public async Task<ActionResult<IEnumerable<TournamentSponsorResponseDTO>>> GetTournaments(int id)
        {
            try
            {
                var list = await _tournamentSponsorService.GetBySponsorAsync(id);
                return Ok(_mapper.Map<IEnumerable<TournamentSponsorResponseDTO>>(list));
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }

        // DELETE :desvincular sponsor de torneo
        [HttpDelete("{id}/tournaments/{tournamentId}")]
        public async Task<ActionResult> RemoveTournament(int id, int tournamentId)
        {
            try
            {
                await _tournamentSponsorService.RemoveAsync(id, tournamentId);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }
    }
}