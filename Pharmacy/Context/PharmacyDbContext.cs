 

namespace Pharmacy.Context

{   //public class instead of internal 
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options) 
        {
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Request>().Property(d => d.Date).HasDefaultValueSql("GetDate()");
			


		}


		public DbSet<Admin> admins { get; set; }
        public DbSet<Patient> patients{ get; set; }
        public DbSet<Medicine> medicines { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}