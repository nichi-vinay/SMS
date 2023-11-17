using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sms.api.Data;
using sms.data.Models;

namespace sms.data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "User" },
                new IdentityRole() { Name = "Executive", ConcurrencyStamp = "2", NormalizedName = "Executive" }
                );
        }

        public virtual DbSet<CompanyMaster> CompanyMaster { get; set; }

        public virtual DbSet<CustomerMaster> CustomerMaster { get; set; }

        public virtual DbSet<DashboardMaster> DashboardMaster { get; set; }

        public virtual DbSet<EnquiryTypeMaster> EnquiryTypeMaster { get; set; }

        public virtual DbSet<ItemMaster> ItemMaster { get; set; }

        public virtual DbSet<OrderItmeMaster> OrderItmeMaster { get; set; }

        public virtual DbSet<OrderMaster> OrderMaster { get; set; }

        public virtual DbSet<OrderTypeMaster> OrderTypeMaster { get; set; }

        public virtual DbSet<PurchaseItemMaster> PurchaseItemMaster { get; set; }

        public virtual DbSet<PurchaseMaster> PurchaseMaster { get; set; }

        public virtual DbSet<ReturnItemMaster> ReturnItemMaster { get; set; }

        public virtual DbSet<ReturnMaster> ReturnMaster { get; set; }



        public virtual DbSet<SalesItemMaster> SalesItemMaster { get; set; }

        public virtual DbSet<SalesMaster> SalesMaster { get; set; }

        public virtual DbSet<TaxTypeMaster> TaxTypeMaster { get; set; }

        public virtual DbSet<UnitTypeMaster> UnitTypeMaster { get; set; }

        public virtual DbSet<CustomerTypeMaster> CustomerTypeMaster { get; set; }

        public virtual DbSet<VendorMaster> VendorMaster { get; set; }

    }
}
