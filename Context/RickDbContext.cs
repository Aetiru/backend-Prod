using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend
{
    // TodoDbContext class inherits from DbContext
    public class RickDbContext : DbContext
    {

        // DbSettings field to store the connection string
        private readonly DbSettings _dbsettings;

        // Constructor to inject the DbSettings model
        public RickDbContext(IOptions<DbSettings> dbSettings)
        {

            _dbsettings = dbSettings.Value;
        }

        // Configuring the database provider and connection string

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbsettings.ConnectionString);
        }

        // DbSet property to represent the Rick table
        public DbSet<Rick> Ricks { get; set; }

        public DbSet<Episode> Episodes { get; set; }


        // Configuring the database provider and connection string
        protected void OModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rick>(entity =>
                {
                    entity.ToTable("RickAPI");
                    entity.HasKey(x => x.Id);

                    entity.Property(e => e.OriginJson).HasColumnType("nvarchar(max)");
                    entity.Property(e => e.LocationJson).HasColumnType("nvarchar(max)");
                });

            modelBuilder.Entity<Episode>(entity =>
    {
        entity.ToTable("Episodes"); // Nombre de la tabla en la base de datos
        entity.HasKey(x => x.Id); // Establecer la clave primaria

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(255);

        entity.Property(e => e.AirDate)
            .IsRequired();

        entity.Property(e => e.EpisodeCode)
            .IsRequired()
            .HasMaxLength(10);

        entity.Property(e => e.Url)
            .IsRequired();

        entity.Property(e => e.Created)
            .IsRequired();

        // Si quieres configurar la propiedad Characters como un campo JSON, necesitarás
        // una configuración adicional dependiendo de la base de datos que estés usando.
        // Esto puede variar dependiendo de si estás utilizando SQL Server, PostgreSQL, etc.
        // Para SQL Server, podrías usar un string y manejar la serialización/deserialización manualmente.
    });




        }


    }

}