namespace SportsLeague.Domain.Enums
{
    //  enum es un tipo de dato que representa un conjunto de constantes relacionadas. En este caso, el enum PlayerPosition define las posiciones que un jugador puede tener en un equipo de fútbol. Cada posición tiene un valor entero asociado, comenzando desde 0 para Goalkeeper, 1 para Defender, 2 para Midfielder y 3 para Forward. Esto facilita la gestión de las posiciones de los jugadores en el código y mejora la legibilidad.
    public enum PlayerPosition

    {
        Goalkeeper = 0,
        Defender = 1,
        Midfielder = 2,
        Forward = 3
    }
}

