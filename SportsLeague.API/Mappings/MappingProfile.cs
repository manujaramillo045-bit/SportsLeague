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
    }
}
