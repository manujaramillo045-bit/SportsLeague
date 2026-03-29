namespace SportsLeague.Domain.Entities
{
    public class TournamentSponsor : AuditBase // tabla intermedia entre tournament y sponsor
    {
        public int Id { get; set; } // PK de la tabla intermedia, es importante tener un Id para poder manejar esta relación de manera más flexible, especialmente si queremos agregar datos adicionales a esta relación (como el monto del contrato o la fecha en que se unieron). Esto nos permite identificar de manera única cada relación entre un torneo y un sponsor, lo que facilita la gestión de estas relaciones en la base de datos y en el código.

        // FK 
        public int TournamentId { get; set; }
        public int SponsorId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow; // valor por defecto al momento de crear la relación entre torneo y sponsor

        // propiedades de navegacion de objeto unico NO lista xq es tabla intermedia entre tournament y sponsor

        public Tournament Tournament { get; set; } = null!;
        public Sponsor Sponsor { get; set; } = null!;

        // dato de negocio
        public decimal ContractAmount { get; set; }
    }
}
