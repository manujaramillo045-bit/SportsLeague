using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IPlayerRepository : IGenericRepository<Player> // todas las entidades repositorias generan herencia de la interfaz IGenericRepository, que es una interfaz genérica que define los métodos básicos para realizar operaciones CRUD (Create, Read, Update, Delete) en la base de datos. Al heredar de esta interfaz, IPlayerRepository obtiene automáticamente estos métodos básicos para trabajar con la entidad Player, lo que facilita la implementación de la lógica de acceso a datos para los jugadores en el sistema.
    {
        //metodos abstractos especificos para Player

        Task<IEnumerable<Player>> GetByTeamAsync(int teamId); // obtener el jugador por equipo , se le pasa el id del equipo y devuelve una lista de jugadores que pertenecen a ese equipo
        Task<Player?> GetByTeamAndNumberAsync(int teamId, int number); // obtener un jugador por equipo y número, se le pasa el id del equipo y el número del jugador, y devuelve el jugador que coincide con esos criterios
        Task<IEnumerable<Player>> GetAllWithTeamAsync(); // obtener todos los jugadores con su equipo, devuelve una lista de jugadores con la información de su equipo incluida, lo que permite acceder a los detalles del equipo al que pertenece cada jugador
        Task<Player?> GetByIdWithTeamAsync(int id); // obtener un jugador por su id con la información de su equipo, se le pasa el id del jugador y devuelve el jugador que coincide con ese id, incluyendo la información de su equipo, lo que permite acceder a los detalles del equipo al que pertenece el jugador
    }
}
