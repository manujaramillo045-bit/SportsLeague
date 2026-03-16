using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Services;


namespace SportsLeague.API.Controllers;


[ApiController] // DataAnnottations esta anotacion indica que esta clase es un controlador de API, lo que permite que se puedan manejar las solicitudes HTTP y devolver respuestas HTTP de forma automática, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs.
[Route("api/[controller]")] // esta anotacion indica la ruta base para acceder a los endpoints de este controlador, donde [controller] se reemplaza por el nombre del controlador sin la palabra "Controller", en este caso sería "api/team", lo que permite organizar los endpoints de la API de forma clara y coherente, y seguir las convenciones de diseño de APIs.

public class TeamController : ControllerBase // hereda controllerbase que es la clase base para los controladores de API, lo que permite manejar las solicitudes HTTP y devolver respuestas HTTP de forma automática, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs. Esta clase se encarga de manejar las operaciones CRUD (Create, Read, Update, Delete) para la entidad Team, usando el servicio ITeamService para realizar las operaciones correspondientes en la capa de negocio, y usando AutoMapper para convertir entre los objetos de dominio y los objetos DTO (Data Transfer Object) que se usan para las solicitudes y respuestas de la API. Además, se inyecta un logger para registrar información relevante sobre las operaciones realizadas en el controlador, lo que es importante para el monitoreo y la depuración de la aplicación.
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;
    private readonly ILogger<TeamController> _logger;

    public TeamController(

    ITeamService teamService,
    IMapper mapper,
    ILogger<TeamController> logger)
    {
        _teamService = teamService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet] // esta anotacion indica que este método maneja las solicitudes HTTP GET, lo que permite obtener una lista de todos los equipos registrados en la base de datos, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs.

    public async Task<ActionResult<IEnumerable<TeamResponseDTO>>> GetAll() // esta anotacion indica que este método maneja las solicitudes HTTP GET, lo que permite obtener una lista de todos los equipos registrados en la base de datos, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs. El método devuelve un ActionResult que contiene una lista de objetos TeamResponseDTO, lo que permite devolver una respuesta HTTP con el código de estado adecuado (200 OK) y el cuerpo de la respuesta con los datos solicitados.
    {
        var teams = await _teamService.GetAllAsync();
        var teamsDto = _mapper.Map<IEnumerable<TeamResponseDTO>>(teams);
        return Ok(teamsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamResponseDTO>> GetById(int id) // esta anotacion indica que este método maneja las solicitudes HTTP GET con un parámetro de ruta (id), lo que permite obtener un equipo específico por su ID, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs. El método devuelve un ActionResult que contiene un objeto TeamResponseDTO, lo que permite devolver una respuesta HTTP con el código de estado adecuado (200 OK) y el cuerpo de la respuesta con los datos solicitados, o una respuesta HTTP con el código de estado 404 Not Found si el equipo con el ID especificado no existe en la base de datos.
    {
        var team = await _teamService.GetByIdAsync(id);
        if (team == null)
            return NotFound(new { message = $"Equipo con ID {id} no encontrado" });
        var teamDto = _mapper.Map<TeamResponseDTO>(team);
        return Ok(teamDto);
    }

    [HttpPost]

    public async Task<ActionResult<TeamResponseDTO>> Create(TeamRequestDTO dto) // esta anotacion indica que este método maneja las solicitudes HTTP POST, lo que permite crear un nuevo equipo en la base de datos, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs. El método recibe un objeto TeamRequestDTO como parámetro, lo que permite recibir los datos necesarios para crear un nuevo equipo en la base de datos, y devuelve un ActionResult que contiene un objeto TeamResponseDTO, lo que permite devolver una respuesta HTTP con el código de estado adecuado (201 Created) y el cuerpo de la respuesta con los datos del equipo creado, o una respuesta HTTP con el código de estado 409 Conflict si ya existe un equipo con el mismo nombre en la base de datos.
    {

        try
        {
            var team = _mapper.Map<Team>(dto);
            var createdTeam = await _teamService.CreateAsync(team);
            var responseDto = _mapper.Map<TeamResponseDTO>(createdTeam);

            return CreatedAtAction(
            nameof(GetById),
            new { id = responseDto.Id },
            responseDto);
        }

        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }


    [HttpPut("{id}")]

    public async Task<ActionResult> Update(int id, TeamRequestDTO dto) // esta anotacion indica que este método maneja las solicitudes HTTP PUT con un parámetro de ruta (id), lo que permite actualizar un equipo específico por su ID, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs. El método recibe un objeto TeamRequestDTO como parámetro, lo que permite recibir los datos necesarios para actualizar el equipo en la base de datos, y devuelve un ActionResult sin contenido, lo que permite devolver una respuesta HTTP con el código de estado adecuado (204 No Content) si la actualización fue exitosa, o una respuesta HTTP con el código de estado 404 Not Found si el equipo con el ID especificado no existe en la base de datos, o una respuesta HTTP con el código de estado 409 Conflict si ya existe otro equipo con el mismo nombre en la base de datos.
    {

        try
        {
            var team = _mapper.Map<Team>(dto);
            await _teamService.UpdateAsync(id, team);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }


    [HttpDelete("{id}")]

    public async Task<ActionResult> Delete(int id) // esta anotacion indica que este método maneja las solicitudes HTTP DELETE con un parámetro de ruta (id), lo que permite eliminar un equipo específico por su ID, lo que es importante para crear una API RESTful y seguir las convenciones de diseño de APIs. El método devuelve un ActionResult sin contenido, lo que permite devolver una respuesta HTTP con el código de estado adecuado (204 No Content) si la eliminación fue exitosa, o una respuesta HTTP con el código de estado 404 Not Found si el equipo con el ID especificado no existe en la base de datos.
    {

        try
        {
            await _teamService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}