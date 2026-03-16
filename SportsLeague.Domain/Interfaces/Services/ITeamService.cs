using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ITeamService
    {
        // metodos que se van a implementar en la capa de servicios, que es la capa intermedia entre el controlador y el repositorio, donde se va a manejar la logica de negocio, y como son asincronicos trabajan con la palabra reservada Task, que es una clase que representa una operacion asincronica, y el tipo de dato que devuelve es el tipo de dato que se va a devolver cuando la operacion asincronica se complete, en este caso es una coleccion de equipos, un equipo por id, un equipo creado, un equipo actualizado y un equipo eliminado
        Task<IEnumerable<Team>> GetAllAsync(); // no recibe ningun parametro  porque mandamos la entidad team y se devuelve una coleccion llamada inmumerable
        Task<Team?> GetByIdAsync(int id); // recibe un parametro que es el id
        Task<Team> CreateAsync(Team team); // recibe el equipo que se crea
        Task UpdateAsync(int id, Team team); // recibe el id que se va a actualizar y el equipo con los nuevos datos (logo, estadio, sede, etc)
        Task DeleteAsync(int id); // recibe el id del equipo que se va a eliminar, no devuelve nada porque se elimina
    }
}
