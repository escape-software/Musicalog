using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using MusicalogModel;

namespace MusicalogData.Models
{
    public partial class MusicalogDBContext : DbContext
    {
        /// <summary>
        /// Create DB context with connection string MusicalogDBConnect.
        /// </summary>
        public MusicalogDBContext() 
            : base("name=MusicalogDBContext")
        {
        }

        public virtual DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlbumConfig());
        }
    }

    /// <summary>
    /// Album configuration class which configures the relationship with other context entities.
    /// </summary>
    public class AlbumConfig : EntityTypeConfiguration<Album>
    {
        public AlbumConfig()
        {
            // Add any additional Album entity configuration data here.
        }
    }
}
