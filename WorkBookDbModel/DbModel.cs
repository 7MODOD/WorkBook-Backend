using Microsoft.EntityFrameworkCore;
using WorkBook.Models;

namespace WorkBook.WorkBookDbModel
{
    public class DbModel: DbContext
    {
        public DbSet<CommentModel> Comment { get; set; }
        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<CustomerAuthModel> CustomerAuth { get; set; }
        public DbSet<ProfessionModel> Profession { get; set; }
        public DbSet<ProjectModel> Project { get; set; }
        public DbSet<RatingModel> Rating { get; set; }
        public DbSet<WorkerAuthModel> WorkerAuth { get; set; }
        public DbSet<WorkerProfessionModel> WorkerProfession { get; set; }
        public DbSet<WorkersModel> Worker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=workBook.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkersModel>()
                .HasOne(w => w.WorkerAuth)
                .WithOne(wa => wa.Worker)
                .HasForeignKey<WorkerAuthModel>(wa => wa.WorkerId);


            modelBuilder.Entity <CustomerModel>()
                .HasOne(w => w.CustomerAuth)
                .WithOne(wa => wa.Customer)
                .HasForeignKey<CustomerAuthModel>(wa => wa.CustomerId);

            modelBuilder.Entity<WorkerProfessionModel>()
                .HasOne(wp => wp.Worker)
                .WithMany(w => w.WorkerProfessions)
                .HasForeignKey(wp => wp.WorkerId);

            modelBuilder.Entity<WorkerProfessionModel>()
                .HasOne(wp => wp.Profession)
                .WithMany(p => p.WorkerProfessions)
                .HasForeignKey(wp => wp.ProfessionId);

            modelBuilder.Entity<WorkerProfessionModel>()
                .HasMany(w => w.Projects)
                .WithOne(p => p.WorkerProfession)
                .HasForeignKey(p => p.WorkerProfessionId);

            modelBuilder.Entity<ProjectModel>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Project)
                .HasForeignKey(c => c.ProjectId);

            modelBuilder.Entity<CustomerModel>()
                .HasMany(c => c.Projects)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.ProjectId);

            modelBuilder.Entity<CommentModel>()
                .HasOne(c => c.Customer)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.CustomerId).IsRequired(false);

            modelBuilder.Entity<WorkersModel>()
                .HasMany(c => c.Comments)
                .WithOne(u => u.Worker)
                .HasForeignKey(c => c.WorkerId).IsRequired(false);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rating)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.Project)
                .WithMany(p => p.Rating)
                .HasForeignKey(r => r.ProjectId);

            modelBuilder.Entity<RatingModel>()
                .HasOne(r => r.WorkerProfession)
                .WithMany(wp => wp.Rating)
                .HasForeignKey(r => r.WorkerProfessionId);


        }
        /*public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => (e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseModel)entityEntry.Entity).UpdatedAt = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseModel)entityEntry.Entity).CreatedAt = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }*/
    }
}
