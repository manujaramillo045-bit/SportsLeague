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

        }

    }
}



