using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : AuditBase
    {
        // <T> recibe dato de entidad tipo T, significa generico, que me recibe anything. Se convierte en un objeto de tipo team, player, etc.
        // metodos genericos que me sirven para poderlos usar dentro de la capa acceso a datos con el patron de IGenericRepository
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
