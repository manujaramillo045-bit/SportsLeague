namespace SportsLeague.Domain.Entities
{
    public class Team : AuditBase
    {
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Stadium { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public DateTime FoundedDate { get; set; }

        // Navigation Property - Colección de jugadores
        public ICollection<Player> Players { get; set; } = new List<Player>(); // nueva lista de jugadores que esta en la entidad player. Sería la relacion 1:N entre Team y Player, donde un equipo puede tener muchos jugadores.
        public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>(); // se conecta con la tabla intermedia
    }
}
