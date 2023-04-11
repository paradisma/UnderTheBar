using Microsoft.EntityFrameworkCore;

namespace UnderTheBarWebAPI.Entities
{
    public partial class UserContext : DbContext
    {
        public UserContext()
        {
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=10.19.19.13;port=3306;user=root;password=elf4256;database=UnderTheBar");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).IsRequired().HasMaxLength(16);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
