using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Entities;
using Task = Data_Access_Layer.Entities.Task;

namespace Data_Access_Layer.DatabaseContext
{
    public class DoDayDBContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DoDayDBContext(DbContextOptions<DoDayDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Users configuration
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>().HasKey(b => b.Id);

            modelBuilder.Entity<User>()
                .Property(b => b.Username)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(b => b.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(b => b.LastName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(b => b.Email)
                .IsRequired();

            //Tasks configuration

            modelBuilder.Entity<Task>().ToTable("Tasks");

            modelBuilder.Entity<Task>().HasKey(b => b.Id);

            modelBuilder.Entity<Task>().
                Property(b => b.Name)
                .HasMaxLength(500)
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(b => b.DateCreated)
                .HasColumnType("date")
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(b => b.Description)
                .HasMaxLength(3000)
                .IsRequired(false);
        }
    }
}
