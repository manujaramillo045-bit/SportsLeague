using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Player : AuditBase // herencia de la clase AuditBase para incluir propiedades de auditoría como CreatedDate, UpdatedDate, etc. Esto es útil para rastrear cuándo se creó o actualizó un jugador en la base de datos.
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int Number { get; set; }
        public PlayerPosition Position { get; set; } // enum que creamos para representar las posiciones de los jugadores en el equipo (portero, defensa, mediocampista, delantero)

        // Foreign Key
        public int TeamId { get; set; }

        // Navigation Property
        public Team Team { get; set; } = null!; // team es la propiedad de navegación que permite acceder al equipo al que pertenece el jugador. El operador null! se utiliza para indicar que esta propiedad no puede ser nula, lo que es importante para garantizar la integridad de los datos y evitar errores en tiempo de ejecución.
    }
}
