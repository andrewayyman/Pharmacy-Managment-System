

namespace Pharmacy.Context
{
    internal class PharmacyDbContext : DbContext
    {
    
        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            // to open the connection to the database
            optionsBuilder.UseSqlServer("Server=WILDRABBIT;Database=PharmacyDb;Trusted_Connection=True;TrustServerCertificate=true;");

        }
     
        public DbSet<Admin> admins { get; set; }
        public DbSet<Patient> patients{ get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}