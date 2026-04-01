using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;
using System.Text.RegularExpressions;

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ILogger<SponsorService> _logger;

        public SponsorService(
            ISponsorRepository sponsorRepository,
            ILogger<SponsorService> logger)
        {
            _sponsorRepository = sponsorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sponsors");
            return await _sponsorRepository.GetAllAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving sponsor with ID: {SponsorId}", id);

            var sponsor = await _sponsorRepository.GetByIdAsync(id);

            if (sponsor == null)
                _logger.LogWarning("Sponsor with ID {SponsorId} not found", id);

            return sponsor;
        }

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            // Validar nombre duplicado
            var exists = await _sponsorRepository.ExistsByNameAsync(sponsor.Name);
            if (exists)
            {
                _logger.LogWarning("Sponsor with name {Name} already exists", sponsor.Name);
                throw new InvalidOperationException("Ya existe un sponsor con ese nombre");
            }

            // Validar email
            if (!IsValidEmail(sponsor.ContactEmail))
            {
                _logger.LogWarning("Invalid email for sponsor: {Email}", sponsor.ContactEmail);
                throw new InvalidOperationException("Email inválido");
            }

            _logger.LogInformation("Creating sponsor: {Name}", sponsor.Name);

            return await _sponsorRepository.CreateAsync(sponsor);
        }

        public async Task UpdateAsync(int id, Sponsor sponsor)
        {
            var existingSponsor = await _sponsorRepository.GetByIdAsync(id);

            if (existingSponsor == null)
            {
                throw new KeyNotFoundException($"No se encontró el sponsor con ID {id}");
            }

            // Validar nombre duplicado
            var exists = await _sponsorRepository.ExistsByNameAsync(sponsor.Name);
            if (exists && existingSponsor.Name != sponsor.Name)
            {
                throw new InvalidOperationException("Ya existe un sponsor con ese nombre");
            }

            // Validar email
            if (!IsValidEmail(sponsor.ContactEmail))
            {
                throw new InvalidOperationException("Email inválido");
            }

            existingSponsor.Name = sponsor.Name;
            existingSponsor.ContactEmail = sponsor.ContactEmail;
            existingSponsor.Phone = sponsor.Phone;
            existingSponsor.WebsiteUrl = sponsor.WebsiteUrl;
            existingSponsor.Category = sponsor.Category;

            _logger.LogInformation("Updating sponsor with ID: {SponsorId}", id);

            await _sponsorRepository.UpdateAsync(existingSponsor);
        }

        public async Task DeleteAsync(int id)
        {
            var exists = await _sponsorRepository.ExistsAsync(id);

            if (!exists)
            {
                throw new KeyNotFoundException($"No se encontró el sponsor con ID {id}");
            }

            _logger.LogInformation("Deleting sponsor with ID: {SponsorId}", id);

            await _sponsorRepository.DeleteAsync(id);
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"); // metodo regex  para validar el formato del correo electronico 
        }
    }
}