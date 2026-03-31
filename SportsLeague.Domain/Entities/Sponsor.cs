using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Sponsor : AuditBase // herencia auditbase clase abstracta
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? WebsiteUrl { get; set; }
        public SponsorCategory Category { get; set; } // crear enum
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        // Navigation Property - Colección de torneos patrocinados
        public ICollection<TournamentSponsor> TournamentSponsors { get; set; } = new List<TournamentSponsor>(); // nueva lista de torneos patrocinados que esta en la entidad tournament sponsor. Sería la relacion 1:N entre Sponsor y TournamentSponsor, donde un sponsor puede patrocinar muchos torneos a través de la tabla intermedia TournamentSponsor.
        // navega a la tabla intermedia, no a tournament directamente porque es una relación de muchos a muchos entre sponsor y tournament, por lo que necesitamos la tabla intermedia para manejar esta relación de manera adecuada.
    }
}



