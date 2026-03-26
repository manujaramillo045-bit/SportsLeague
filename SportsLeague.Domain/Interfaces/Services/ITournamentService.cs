using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ITournamentService
    {
        Task<IEnumerable<Tournament>> GetAllAsync(); // obtener todos los torneos
        Task<Tournament?> GetByIdAsync(int id); // obtener un torneo por id, y devuelve un torneo con la información del torneo, o null si no se encuentra ningún torneo con ese id.
        Task<Tournament> CreateAsync(Tournament tournament); // crear un nuevo torneo, y devuelve el torneo creado con su id asignado.
        Task UpdateAsync(int id, Tournament tournament); // actualizar un torneo existente por id, usando los datos del objeto tournament, y devuelve el torneo actualizado, o null si no se encuentra ningún torneo con ese id.
        Task DeleteAsync(int id); // eliminar un torneo existente por id, y devuelve un valor booleano que indique si la eliminación fue exitosa (true) o si no se encuentra ningún torneo con ese id (false).
        Task UpdateStatusAsync(int id, TournamentStatus newStatus); // actualizar el estado de un torneo existente por id, usando el nuevo estado especificado en newStatus, y devuelve el torneo actualizado con su nuevo estado, o null si no se encuentra ningún torneo con ese id.
        Task RegisterTeamAsync(int tournamentId, int teamId); // registrar un equipo en un torneo existente por id, usando el id del torneo (tournamentId) y el id del equipo (teamId), y devuelve un valor booleano que indique si el registro fue exitoso (true) o si no se encuentra ningún torneo con ese id o ningún equipo con ese id (false).
        Task<IEnumerable<Team>> GetTeamsByTournamentAsync(int tournamentId); // obtener los equipos registrados en un torneo existente por id, usando el id del torneo (tournamentId), y devuelve una lista de equipos registrados en ese torneo, o null si no se encuentra ningún torneo con ese id.
    }
}
