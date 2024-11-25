using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Person
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(p => p.FirstName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.LastName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.DateOfBirth)
                      .IsRequired();

                entity.HasCheckConstraint("CK_Person_Age", "DATEDIFF(YEAR, DateOfBirth, GETDATE()) < 150");
            });

            // Configure Job
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasOne(j => j.Person)
                      .WithMany(p => p.Jobs)
                      .HasForeignKey(j => j.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Ignore(j => j.Person);
            });

        }

    }
}