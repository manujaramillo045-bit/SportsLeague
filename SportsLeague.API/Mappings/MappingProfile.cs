using AutoMapper;
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
    }
}