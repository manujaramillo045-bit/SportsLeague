using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    // 5 metodos

    public interface IRefereeService
    {

        Task<IEnumerable<Referee>> GetAllAsync(); // Retorna uma lista de árbitros
        Task<Referee?> GetByIdAsync(int id); // Retorna um árbitro específico por ID, ou null se não encontrado
        Task<Referee> CreateAsync(Referee referee); // crear una nuevo árbitro y retornar el objeto creado con su ID asignado
        Task UpdateAsync(int id, Referee referee); // actualizar un árbitro existente por ID, usando los datos del objeto referee, y retornar el objeto actualizado, o null si no se encontró el árbitro con el ID especificado
        Task DeleteAsync(int id); // eliminar un árbitro existente por ID, y retornar un valor booleano que indique si la eliminación fue exitosa (true) o si no se encontró el árbitro con el ID especificado (false)
    }
}
