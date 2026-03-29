using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Tournament : AuditBase
    {
        public string Name { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.Pending;

        // Navigation Properties

        public ICollection<TournamentTeam> TournamentTeams { get; set; } = new List<TournamentTeam>();
        public ICollection<TournamentSponsor> TournamentSponsors { get; set; } = new List<TournamentSponsor>();
        
        //se actualiza nueva lista de torneos patrocinados que esta en la entidad tournament sponsor. Sería la relacion 1:N entre Tournament y TournamentSponsor, donde un torneo puede tener muchos sponsors a través de la tabla intermedia TournamentSponsor
        // inicializada en lista vacía
    }
}
