using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorService
    {
     
            Task<IEnumerable<Sponsor>> GetAllAsync(); // obtener
            Task<Sponsor?> GetByIdAsync(int id);
            Task<Sponsor> CreateAsync(Sponsor sponsor); // crear
            Task UpdateAsync(int id, Sponsor sponsor); // actualizar
            Task DeleteAsync(int id); // eliminar
        
    }
}
