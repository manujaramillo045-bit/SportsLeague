using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.DataAccess.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository // esta clase es la implementación concreta del repositorio para la entidad Player, y hereda de la clase genérica GenericRepository que implementa los métodos básicos para realizar operaciones CRUD (Create, Read, Update, Delete) en la base de datos, y también implementa la interfaz IPlayerRepository que define los métodos específicos para trabajar con la entidad Player, como obtener jugadores por equipo, obtener un jugador por equipo y número, obtener todos los jugadores con su equipo, y obtener un jugador por su id con la información de su equipo. Al heredar de GenericRepository<Player>, PlayerRepository obtiene automáticamente los métodos básicos para trabajar con la entidad Player, lo que facilita la implementación de la lógica de acceso a datos para los jugadores en el sistema.
    {

        // metodos del repositorio especificos para Player y ya desarrollados en la interfaz IPlayerRepository, y se implementan aquí para proporcionar la funcionalidad específica para trabajar con la entidad Player en la base de datos.

        public PlayerRepository(LeagueDbContext context) : base(context) // metodo constructor que recibe el contexto de la base de datos (LeagueDbContext) y lo pasa al constructor de la clase base (GenericRepository<Player>) para inicializar el repositorio con el contexto de la base de datos. Esto permite que el repositorio tenga acceso a la base de datos y pueda realizar las operaciones CRUD (Create, Read, Update, Delete) en la entidad Player utilizando Entity Framework Core.
        {
        }

        public async Task<IEnumerable<Player>> GetByTeamAsync(int teamId) // este método obtiene una lista de jugadores que pertenecen a un equipo específico, utilizando el identificador del equipo (teamId) como parámetro. La consulta se realiza utilizando Entity Framework Core, y se incluye la información del equipo asociado a cada jugador mediante el método Include(p => p.Team). El resultado se devuelve como una lista de jugadores que cumplen con la condición especificada.
        {
            return await _dbSet
            .Where(p => p.TeamId == teamId) // where es el filtro para obtener solo los jugadores que pertenecen al equipo con el identificador teamId, y luego se incluye la información del equipo asociado a cada jugador mediante el método Include(p => p.Team). El resultado se devuelve como una lista de jugadores que cumplen con la condición especificada.
            .Include(p => p.Team) // es un Join para incluir la información del equipo asociado a cada jugador en la consulta, lo que permite obtener los datos del equipo junto con los datos del jugador en una sola consulta a la base de datos. Esto es útil para evitar realizar consultas adicionales para obtener la información del equipo después de obtener los jugadores, lo que mejora el rendimiento de la aplicación al reducir el número de consultas a la base de datos.
            .ToListAsync(); // como se retorna una lista de jugadores , se utiliza el método ToListAsync() para ejecutar la consulta de forma asíncrona y obtener el resultado como una lista de jugadores que cumplen con la condición especificada (pertenecen al equipo con el identificador teamId). El resultado se devuelve como una lista de jugadores que cumplen con la condición especificada.
        }

        public async Task<Player?> GetByTeamAndNumberAsync(int teamId, int number) // este método obtiene un jugador específico que pertenece a un equipo y tiene un número determinado. Se utiliza el identificador del equipo (teamId) y el número del jugador (number) como parámetros para realizar la consulta. La consulta se realiza utilizando Entity Framework Core, y se devuelve el primer jugador que cumple con ambas condiciones (pertenece al equipo especificado y tiene el número especificado). Si no se encuentra ningún jugador que cumpla con estas condiciones, se devuelve null.
        {
            return await _dbSet
            .FirstOrDefaultAsync(p => p.TeamId == teamId && p.Number == number);
            // linq es la forma simplificada de escribir consultas a la base de datos utilizando Entity Framework Core, y en este caso se utiliza el método FirstOrDefaultAsync para obtener el primer jugador que cumple con las condiciones especificadas (pertenece al equipo con el identificador teamId y tiene el número number). Si no se encuentra ningún jugador que cumpla con estas condiciones, se devuelve null.
        }

        public async Task<IEnumerable<Player>> GetAllWithTeamAsync() // este método obtiene una lista de todos los jugadores en la base de datos, incluyendo la información del equipo asociado a cada jugador. La consulta se realiza utilizando Entity Framework Core, y se incluye la información del equipo mediante el método Include(p => p.Team). El resultado se devuelve como una lista de jugadores con su información de equipo correspondiente.
        {
            return await _dbSet
            .Include(p => p.Team)
            .ToListAsync();
        }

        public async Task<Player?> GetByIdWithTeamAsync(int id) // este método obtiene un jugador específico por su identificador (id) y también incluye la información del equipo asociado a ese jugador. La consulta se realiza utilizando Entity Framework Core, y se incluye la información del equipo mediante el método Include(p => p.Team). El resultado se devuelve como un jugador que coincide con el identificador especificado, incluyendo la información de su equipo. Si no se encuentra ningún jugador con ese identificador, se devuelve null.
        {
            return await _dbSet
            .Include(p => p.Team)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
