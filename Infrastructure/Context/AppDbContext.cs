using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}


        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MaintenanceRequest> Requests { get; set; }
        public DbSet<RequestDetail> RequestDetails { get; set; }
        public DbSet<RequestHistory> RequestHistories { get; set; }
        public DbSet<TechnicianCategory> TechnicianCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var relationShips = modelBuilder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys());

            foreach (var relationship in relationShips)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<TechnicianCategory>(entity =>
            {
                entity.HasOne(tc => tc.Technician)
                      .WithMany(u => u.TechnicianCategories)
                      .HasForeignKey(tc => tc.TechnicianId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tc => tc.Category)
                      .WithMany(c => c.TechnicianCategories)
                      .HasForeignKey(tc => tc.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MaintenanceRequest>(entity =>
            {
                entity.HasOne(x => x.Employee)
                      .WithMany(x => x.CreatedRequests)
                      .HasForeignKey(x => x.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(x => x.Technician)
                      .WithMany(x => x.AssignedTasks)
                      .HasForeignKey(x => x.TechnicianId)
                      .IsRequired(false) 
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(x => x.Category)
                      .WithMany(x => x.MaintenanceRequests)
                      .HasForeignKey(x => x.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
