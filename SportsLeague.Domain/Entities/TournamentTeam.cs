using System;
using System.Collections.Generic;
using System.Text;

namespace SportsLeague.Domain.Entities
{
    public class TournamentTeam : AuditBase // tabla intermedia entre tournament y team
    {
        public int TournamentId { get; set; }
        public int TeamId { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        // en una tabla intermedia debe haber conexion entre sus dos tablas SON de objeto UNICO y no lista

        public Tournament Tournament { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
