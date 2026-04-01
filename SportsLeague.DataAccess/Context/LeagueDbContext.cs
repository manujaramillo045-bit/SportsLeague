using Microsoft.EntityFrameworkCore;
using SportsLeague.Domain.Entities;

namespace SportsLeague.DataAccess.Context
{
    public class LeagueDbContext : DbContext // esta clase es la que se encarga de configurar la base de datos, y es la que se va a usar para crear el contexto de la base de datos, y es la que se va a usar para crear las tablas en la base de datos, y es la que se va a usar para hacer las operaciones CRUD (Create, Read, Update, Delete) en la base de datos y hereda de la clase DBContext que es la clase base para trabajar con Entity Framework Core, y se le pasa el tipo de dato que se va a usar para configurar la base de datos, en este caso es el LeagueDbContext, y se le pasa el constructor que recibe las opciones de configuración de la base de datos, y se le pasa el método OnModelCreating que es el que se encarga de configurar las entidades y las relaciones entre ellas, y se le pasa la entidad Team que es la que se va a usar para crear la tabla en la base de datos, y se le pasan las propiedades de la entidad Team para configurar las columnas de la tabla en la base de datos, como el nombre del equipo, la ciudad, el estadio, el logo, etc., y se le pasan las validaciones de cada propiedad para configurar las restricciones en la base de datos, como si es obligatorio o no, el tamaño máximo de caracteres, etc., y se le pasa un índice único para evitar duplicados en el campo Name.
    {
        public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
        : base(options)
        {
        }

        public DbSet<Team> Teams => Set<Team>();

        // se actuliza por la nueva entidad creada que es Player, y se le pasa el tipo de dato Player para crear la tabla en la base de datos, y se le pasa el nombre de la propiedad Players para configurar la columna en la tabla de la base de datos, y se le pasa el método Set<Player>() para configurar la entidad Player en el contexto de la base de datos.
        public DbSet<Player> Players => Set<Player>();

        // se actualiza por la nuevas entidades, Referee, Tournament y TournamentTeam

        public DbSet<Referee> Referees => Set<Referee>(); 
        public DbSet<Tournament> Tournaments => Set<Tournament>(); 
        public DbSet<TournamentTeam> TournamentTeams => Set<TournamentTeam>(); 
        public DbSet<Sponsor> Sponsors => Set<Sponsor>(); // NUEVO
        public DbSet<TournamentSponsor> TournamentSponsors => Set<TournamentSponsor>(); // NUEVO

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // validaciones de tipo de datos, longitudes de cada dato
            modelBuilder.Entity<Team>(entity =>

            {

                entity.HasKey(t => t.Id); // Primary Key

                entity.Property(t => t.Name) // llave obligatoria y con maximo de caracteres de 100

                    .IsRequired()

                    .HasMaxLength(100);

                entity.Property(t => t.City) // llave obligatoria y con maximo de caracteres de 100

                    .IsRequired()

                    .HasMaxLength(100);

                entity.Property(t => t.Stadium) // llave  NO obkligatoria y con maximo de caracteres de 150

                    .HasMaxLength(150);

                entity.Property(t => t.LogoUrl)

                    .HasMaxLength(500);

                entity.Property(t => t.CreatedAt) // llave obligatoria

                    .IsRequired();

                entity.Property(t => t.UpdatedAt) // llave NO obligatoria ya que tiene un false

                    .IsRequired(false);

                entity.HasIndex(t => t.Name) // índice único para cada equipo en el campo Name para evitar duplicados

                    .IsUnique();
            });

            // ── Player Configuration ──

            modelBuilder.Entity<Player>(entity =>

            {

                entity.HasKey(p => p.Id);

                entity.Property(p => p.FirstName)

                    .IsRequired()

                    .HasMaxLength(80);

                entity.Property(p => p.LastName)

                    .IsRequired()

                    .HasMaxLength(80);

                entity.Property(p => p.BirthDate)

                    .IsRequired();

                entity.Property(p => p.Number)

                    .IsRequired();

                entity.Property(p => p.Position)

                    .IsRequired();

                entity.Property(p => p.CreatedAt)

                    .IsRequired();

                entity.Property(p => p.UpdatedAt)

                    .IsRequired(false);


                // Relación 1:N con Team

                entity.HasOne(p => p.Team)

                    .WithMany(t => t.Players)

                    .HasForeignKey(p => p.TeamId)

                    .OnDelete(DeleteBehavior.Cascade); // Si se elimina un equipo, se eliminan sus jugadores asociados (comportamiento en cascada). Siempre cuando hayan relaciones entre tablas

                // Índice único compuesto: número de camiseta único por equipo

                entity.HasIndex(p => new { p.TeamId, p.Number })

                    .IsUnique();
            });

            // ── Referee Configuration ──

            modelBuilder.Entity<Referee>(entity =>
            {

                entity.HasKey(r => r.Id);

                entity.Property(r => r.FirstName)

                    .IsRequired()

                    .HasMaxLength(80);

                entity.Property(r => r.LastName)

                    .IsRequired()

                    .HasMaxLength(80);

                entity.Property(r => r.Nationality)

                    .IsRequired()

                    .HasMaxLength(80);

                entity.Property(r => r.CreatedAt)

                    .IsRequired();

                entity.Property(r => r.UpdatedAt)

                    .IsRequired(false);

            });

            // ── Tournament Configuration ──

            modelBuilder.Entity<Tournament>(entity =>
            {

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)

                    .IsRequired()

                    .HasMaxLength(150);

                entity.Property(t => t.Season)

                    .IsRequired()

                    .HasMaxLength(20);

                entity.Property(t => t.StartDate)

                    .IsRequired();

                entity.Property(t => t.EndDate)

                    .IsRequired();

                entity.Property(t => t.Status)

                    .IsRequired();

                entity.Property(t => t.CreatedAt)

                    .IsRequired();

                entity.Property(t => t.UpdatedAt)

                    .IsRequired(false);

            });

            // ── TournamentTeam Configuration ──

            modelBuilder.Entity<TournamentTeam>(entity =>
            {

                entity.HasKey(tt => tt.Id);

                entity.Property(tt => tt.RegisteredAt)

                    .IsRequired();

                entity.Property(tt => tt.CreatedAt)

                    .IsRequired();

                entity.Property(tt => tt.UpdatedAt)

                    .IsRequired(false);


                // Relación con Tournament

                entity.HasOne(tt => tt.Tournament)

                    .WithMany(t => t.TournamentTeams)

                    .HasForeignKey(tt => tt.TournamentId)

                    .OnDelete(DeleteBehavior.Cascade);


                // Relación con Team

                entity.HasOne(tt => tt.Team)

                    .WithMany(t => t.TournamentTeams)

                    .HasForeignKey(tt => tt.TeamId)

                    .OnDelete(DeleteBehavior.Cascade);


                // Índice único compuesto: un equipo solo una vez por torneo

                entity.HasIndex(tt => new { tt.TournamentId, tt.TeamId })

                    .IsUnique();

            });

            // Sponsor Configuration 

            modelBuilder.Entity<Sponsor>()
             .HasIndex(s => s.Name) // evita 2 sponsor con el mismo nombre
             .IsUnique();

            // TournamentSponsor configuration

            modelBuilder.Entity<TournamentSponsor>(entity =>
            {
                entity.HasKey(ts => ts.Id);

                entity.Property(ts => ts.ContractAmount)
                    .IsRequired()
                    .HasPrecision(18, 2);


                entity.Property(ts => ts.JoinedAt)
                    .IsRequired();

                entity.Property(ts => ts.CreatedAt)
                    .IsRequired();

                entity.Property(ts => ts.UpdatedAt)
                    .IsRequired(false);

                // Relación con Tournament
                entity.HasOne(ts => ts.Tournament)
                    .WithMany(t => t.TournamentSponsors)
                    .HasForeignKey(ts => ts.TournamentId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relación con Sponsor
                entity.HasOne(ts => ts.Sponsor)
                    .WithMany(s => s.TournamentSponsors)
                    .HasForeignKey(ts => ts.SponsorId)
                    .OnDelete(DeleteBehavior.Cascade);

                // índice único compuesto: NO se puede permitir que este 2 veces
                entity.HasIndex(ts => new { ts.TournamentId, ts.SponsorId })
                    .IsUnique();
            });
        }
    }
}



