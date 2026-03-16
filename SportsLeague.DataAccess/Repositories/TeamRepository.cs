using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;


namespace SportsLeague.DataAccess.Repositories;


public class TeamRepository : GenericRepository<Team>, ITeamRepository // herencia de la clase GenericRepository y recibe un atributo T que en este caso será team y a su vez se implementa la interfaz ITeamRepository que serán los 2 metodos de esa interfaz que se crearan
{
    public TeamRepository(LeagueDbContext context) : base(context)
    {
    }

    public async Task<Team?> GetByNameAsync(string name) // metodo creado de la interfaz ITeamRepository. Aqui se devuelve un objeto de tipo TEAM
    {
        return await _dbSet // invoca _dbset
        .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower()); // con el metodo .first0rdefaultAsync se expresa que team tal que team.name buscar las coincidencias con el name que se pasa por el parametro de la clase ejem: real madrid = Real Madrid (!=) todo será en minuscula
    }

    public async Task<IEnumerable<Team>> GetByCityAsync(string city) // metodo creado de la interfaz ITeamRepository. Aqui se devuelve una lista de tipo TEAM
    {
        return await _dbSet // invoca _dbset
        .Where(t => t.City.ToLower() == city.ToLower()) //comparar los city y luego enlistar
        .ToListAsync();
    }
}