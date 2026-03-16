using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        // ITeamRepository está heredando de IGenericRepository, y este recibe un objeto de tipo Team 
        // 2 metodos especificos, name and city siempre acompañados de Async xq son asincronicos
        Task<Team?> GetByNameAsync(string name);
        Task<IEnumerable<Team>> GetByCityAsync(string city);
    }
}
