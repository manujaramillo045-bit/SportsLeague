using AutoMapper;
using SportsLeague.API.DTOs.Reponse;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Mappings;

public class MappingProfile : Profile // heredo de la clase Profile que es la clase base para configurar los mapeos en AutoMapper, y se le pasa el constructor que es el que se encarga de configurar los mapeos entre las clases, y se le pasan los mapeos entre las clases TeamRequestDTO y Team, y entre las clases Team y TeamResponseDTO, lo que permite convertir objetos de un tipo a otro de forma automática, lo que es importante para separar las capas de la aplicación y evitar acoplamientos innecesarios entre ellas.
{
    public MappingProfile()
    {
        // Team mappings
        CreateMap<TeamRequestDTO, Team>(); // mapeo entre la clase TeamRequestDTO y la clase Team, lo que permite convertir un objeto de tipo TeamRequestDTO a un objeto de tipo Team de forma automática, lo que es importante para separar las capas de la aplicación y evitar acoplamientos innecesarios entre ellas.
        CreateMap<Team, TeamResponseDTO>(); // mapeo entre la clase Team y la clase TeamResponseDTO, lo que permite convertir un objeto de tipo Team a un objeto de tipo TeamResponseDTO de forma automática, lo que es importante para separar las capas de la aplicación y evitar acoplamientos innecesarios entre ellas.

        // Player mappings
        CreateMap<PlayerRequestDTO, Player>();
        CreateMap<Player, PlayerResponseDTO>()
        .ForMember(                                // metodo que me genera una relacion entre la propiedad TeamName de PlayerResponseDTO y la propiedad Name de Team, lo que permite mapear el nombre del equipo al que pertenece el jugador en la respuesta de la API, lo que es importante para proporcionar información completa sobre el jugador y su equipo en la respuesta de la API.
        dest => dest.TeamName,
        opt => opt.MapFrom(src => src.Team.Name));

        // Referee mappings
        CreateMap<RefereeRequestDTO, Referee>();
        CreateMap<Referee, RefereeResponseDTO>();

        // Tournament mappings
        CreateMap<TournamentRequestDTO, Tournament>();
        CreateMap<Tournament, TournamentResponseDTO>()
        .ForMember(
        //condicion ternaria para mapear el número de equipos inscritos en el torneo, si la propiedad TournamentTeams no es nula se cuenta el número de equipos inscritos, si es nula se devuelve 0, lo que permite mapear el número de equipos inscritos en el torneo en la respuesta de la API, lo que es importante para proporcionar información completa sobre el torneo y su participación en la respuesta de la API.
        dest => dest.TeamsCount, // metodo que me genera una relacion entre la propiedad TeamsCount de TournamentResponseDTO y la propiedad TournamentTeams de Tournament, lo que permite mapear el número de equipos inscritos en el torneo en la respuesta de la API, lo que es importante para proporcionar información completa sobre el torneo y su participación en la respuesta de la API.
        opt => opt.MapFrom(src =>
        src.TournamentTeams != null ? src.TournamentTeams.Count : 0)); // cuantos equipos hay inscritos en el torneo, si no hay ninguno se devuelve 0
    }
}

  /*
  
 * ? = entonces
 * : = sino
 
  */