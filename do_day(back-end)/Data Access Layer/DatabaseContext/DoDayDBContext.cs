using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Entities;
using Task = Data_Access_Layer.Entities.Task;

namespace Data_Access_Layer.DatabaseContext
{
    public class DoDayDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryOption> CategoryOptions { get; set; }

        public DoDayDBContext(DbContextOptions<DoDayDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Users configuration
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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email) 
                .IsUnique();

            #endregion

            #region Tasks configuration
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
            #endregion

            #region Categories configuration
            modelBuilder.Entity<Category>().ToTable("Categories");

            modelBuilder.Entity<Category>().HasKey(b => b.Id);

            modelBuilder.Entity<Category>().
              Property(b => b.Name)
              .HasMaxLength(200)
              .IsRequired();
            #endregion

            #region CategoryOptions configuration

            modelBuilder.Entity<CategoryOption>().ToTable("CategoryOptions");

            modelBuilder.Entity<CategoryOption>().HasKey(b => b.Id);

            modelBuilder.Entity<CategoryOption>()
                .Property(b => b.Value)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<CategoryOption>()
                .Property(b => b.Key)
                .IsRequired();

            modelBuilder.Entity<CategoryOption>()
                .ToTable(t => t.HasCheckConstraint("CK_CategoryOption_ValueKey_Positive", "[Key] >= 0"));

            #endregion

            #region Relationships configuration

            modelBuilder.Entity<Task>()
                .HasOne(co => co.User)          
                .WithMany(c => c.Tasks)         
                .HasForeignKey(co => co.UserId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasOne(co => co.User)          
                .WithMany(c => c.Categories)         
                .HasForeignKey(co => co.IdUser) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryOption>()
                .HasOne(co => co.Category)
                .WithMany(c => c.CategoryOptions)
                .HasForeignKey(co => co.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
