using SportsLeague.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ITournamentTeamRepository : IGenericRepository<TournamentTeam>
    {
        Task<TournamentTeam?> GetByTournamentAndTeamAsync(int tournamentId, int teamId); // obtiene la relación entre un torneo y un equipo por los ids del torneo y del equipo, y devuelve la relación entre el torneo y el equipo, o null si no se encuentra ninguna relación con esos ids.
        Task<IEnumerable<TournamentTeam>> GetByTournamentAsync(int tournamentId); // obtiene todas las relaciones entre un torneo y los equipos registrados en ese torneo por el id del torneo, y devuelve una lista de relaciones entre el torneo y los equipos registrados en ese torneo, o null si no se encuentra ningún torneo con ese id.
    }
}
