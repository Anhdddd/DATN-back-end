using DATN_back_end.Entities;
using Microsoft.EntityFrameworkCore;

namespace DATN_back_end.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<ProvinceOrCity> ProvinceOrCities { get; set; }
        public DbSet<UserJobPosting> UserJobPostings { get; set; }
        public DbSet<UserSavedCompany> UserSavedCompanies { get; set; }
        public DbSet<UserSavedJobPosting> UserSavedJobPostings { get; set; }
        public DbSet<CVData> CVDatas { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Company>()
                    .HasOne(c => c.Owner)
                    .WithOne(u => u.Company)
                    .HasForeignKey<Company>(c => c.OwnerId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
