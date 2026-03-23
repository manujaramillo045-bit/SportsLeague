using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IPlayerService
    {
        //mismos metodos del repositorio pero con logica de negocio adicional si es necesario y por eso no tienen herencia directa con el repositorio
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player?> GetByIdAsync(int id);
        Task<IEnumerable<Player>> GetByTeamAsync(int teamId);
        Task<Player> CreateAsync(Player player);
        Task UpdateAsync(int id, Player player);
        Task DeleteAsync(int id);
    }
}
