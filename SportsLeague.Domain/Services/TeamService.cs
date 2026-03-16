using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;


namespace SportsLeague.Domain.Services;


public class TeamService : ITeamService // implementa y hereda la interfaz ITeamService
{
    // 2 variables ITeamRepository y Ilogger
    private readonly ITeamRepository _teamRepository;
    private readonly ILogger<TeamService> _logger; // ILogger es nuevo, se debe inyectar para poderlo usar

    public TeamService(ITeamRepository teamRepository, ILogger<TeamService> logger) // a este constructor se le pasan las dependencias de ITeamRepository y ILogger<TeamService> para poder usarlas en la clase, y se asignan a las variables privadas para poder usarlas en los métodos de la clase
    {
        _teamRepository = teamRepository; // se inicializa la variable _teamRepository con la instancia de ITeamRepository que se inyecta en el constructor, lo que permite usar los métodos del repositorio para hacer operaciones CRUD en la base de datos, como GetAllAsync(), GetByIdAsync(), CreateAsync(), UpdateAsync() y DeleteAsync().
        _logger = logger; // se inicializa la variable _logger con la instancia de ILogger<TeamService> que se inyecta en el constructor, lo que permite usar los métodos del logger para hacer registros de información, advertencias y errores en la aplicación, lo que es importante para el monitoreo y la depuración de la aplicación, como LogInformation(), LogWarning() y LogError().
    }

    public async Task<IEnumerable<Team>> GetAllAsync() // este método es asíncrono y devuelve una lista de equipos, se invoca o llama al método GetAllAsync() del repositorio para obtener todos los equipos de la base de datos, y se registra una información en el logger indicando que se están recuperando todos los equipos
    {
        _logger.LogInformation("Retrieving all teams");
        return await _teamRepository.GetAllAsync();
    }

    public async Task<Team?> GetByIdAsync(int id) // este método es asíncrono y devuelve un equipo por su id, se invoca o llama al método GetByIdAsync() del repositorio para obtener el equipo con el id especificado de la base de datos, y se registra una información en el logger indicando que se está recuperando el equipo con el id especificado, y si no se encuentra el equipo, se registra una advertencia en el logger indicando que no se encontró el equipo con el id especificado
    {
        _logger.LogInformation("Retrieving team with ID: {TeamId}", id);
        var team = await _teamRepository.GetByIdAsync(id);
        if (team == null)
            _logger.LogWarning("Team with ID {TeamId} not found", id);
        return team;
    }

    public async Task<Team> CreateAsync(Team team) // este método es asíncrono y devuelve el equipo creado, se realiza una validación de negocio para verificar que el nombre del equipo sea único, se invoca o llama al método GetByNameAsync() del repositorio para obtener un equipo con el mismo nombre de la base de datos, y si se encuentra un equipo con el mismo nombre, se registra una advertencia en el logger indicando que ya existe un equipo con el nombre especificado, y se lanza una excepción InvalidOperationException indicando que ya existe un equipo con el nombre especificado, si no se encuentra un equipo con el mismo nombre, se registra una información en el logger indicando que se está creando el equipo con el nombre especificado, y se invoca o llama al método CreateAsync() del repositorio para crear el equipo en la base de datos
    {
        // Validación de negocio: nombre único
        var existingTeam = await _teamRepository.GetByNameAsync(team.Name);
        if (existingTeam != null)
        {
            _logger.LogWarning("Team with name '{TeamName}' already exists", team.Name);
            throw new InvalidOperationException(
            $"Ya existe un equipo con el nombre '{team.Name}'");
        }
        _logger.LogInformation("Creating team: {TeamName}", team.Name);
        return await _teamRepository.CreateAsync(team);
    }

    public async Task UpdateAsync(int id, Team team) // este método es asíncrono y no devuelve nada, se realiza una validación de negocio para verificar que el equipo con el id especificado exista, se invoca o llama al método GetByIdAsync() del repositorio para obtener el equipo con el id especificado de la base de datos, y si no se encuentra el equipo, se registra una advertencia en el logger indicando que no se encontró el equipo con el id especificado, y se lanza una excepción KeyNotFoundException indicando que no se encontró el equipo con el id especificado, si se encuentra el equipo, se realiza una validación de negocio para verificar que el nombre del equipo sea único (si cambió), si el nombre del equipo cambió, se invoca o llama al método GetByNameAsync() del repositorio para obtener un equipo con el mismo nombre de la base de datos, y si se encuentra un equipo con el mismo nombre, se registra una advertencia en el logger indicando que ya existe un equipo con el nombre especificado, y se lanza una excepción InvalidOperationException indicando que ya existe un equipo con el nombre especificado, si no se encuentra un equipo con el mismo nombre o si el nombre no cambió, se actualizan las propiedades del equipo existente con los valores del equipo pasado como parámetro, se registra una información en el logger indicando que se está actualizando el equipo con el id especificado, y se invoca o llama al método UpdateAsync() del repositorio para actualizar el equipo en la base de datos
    {
        var existingTeam = await _teamRepository.GetByIdAsync(id);
        if (existingTeam == null)
        {
            _logger.LogWarning("Team with ID {TeamId} not found for update", id);
            throw new KeyNotFoundException(
            $"No se encontró el equipo con ID {id}");
        }

        // Validar nombre único (si cambió)

        if (existingTeam.Name != team.Name)
        {
            var teamWithSameName = await _teamRepository.GetByNameAsync(team.Name);
            if (teamWithSameName != null)
            {
                throw new InvalidOperationException(
                $"Ya existe un equipo con el nombre '{team.Name}'");
            }
        }
        existingTeam.Name = team.Name;
        existingTeam.City = team.City;
        existingTeam.Stadium = team.Stadium;
        existingTeam.LogoUrl = team.LogoUrl;
        existingTeam.FoundedDate = team.FoundedDate;
        _logger.LogInformation("Updating team with ID: {TeamId}", id);
        await _teamRepository.UpdateAsync(existingTeam);
    }

    public async Task DeleteAsync(int id) // este método es asíncrono y no devuelve nada, se realiza una validación de negocio para verificar que el equipo con el id especificado exista, se invoca o llama al método ExistsAsync() del repositorio para verificar si el equipo con el id especificado existe en la base de datos, y si no existe el equipo, se registra una advertencia en el logger indicando que no se encontró el equipo con el id especificado para eliminación, y se lanza una excepción KeyNotFoundException indicando que no se encontró el equipo con el id especificado, si existe el equipo, se registra una información en el logger indicando que se está eliminando el equipo con el id especificado, y se invoca o llama al método DeleteAsync() del repositorio para eliminar el equipo de la base de datos
    {
        var exists = await _teamRepository.ExistsAsync(id);
        if (!exists)
        {
            _logger.LogWarning("Team with ID {TeamId} not found for deletion", id);
            throw new KeyNotFoundException(
            $"No se encontró el equipo con ID {id}");
        }
        _logger.LogInformation("Deleting team with ID: {TeamId}", id);
        await _teamRepository.DeleteAsync(id);
    }
}
