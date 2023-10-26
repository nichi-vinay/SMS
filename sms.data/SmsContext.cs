using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sms.Data;

public partial class SmsContext : DbContext
{
    public SmsContext()
    {
    }

    public SmsContext(DbContextOptions<SmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompanyMaster> CompanyMasters { get; set; }

    public virtual DbSet<CustomerMaster> CustomerMasters { get; set; }

    public virtual DbSet<DashboardMaster> DashboardMasters { get; set; }

    public virtual DbSet<EnquiryTypeMaster> EnquiryTypeMasters { get; set; }

    public virtual DbSet<ItemMaster> ItemMasters { get; set; }

    public virtual DbSet<OrderItmeMaster> OrderItmeMasters { get; set; }

    public virtual DbSet<OrderMaster> OrderMasters { get; set; }

    public virtual DbSet<OrderTypeMaster> OrderTypeMasters { get; set; }

    public virtual DbSet<PurchaseItemMaster> PurchaseItemMasters { get; set; }

    public virtual DbSet<PurchaseMaster> PurchaseMasters { get; set; }

    public virtual DbSet<ReturnItemMaster> ReturnItemMasters { get; set; }

    public virtual DbSet<ReturnMaster> ReturnMasters { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<SalesItemMaster> SalesItemMasters { get; set; }

    public virtual DbSet<SalesMaster> SalesMasters { get; set; }

    public virtual DbSet<TaxTypeMaster> TaxTypeMasters { get; set; }

    public virtual DbSet<UnitTypeMaster> UnitTypeMasters { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    public virtual DbSet<VendorMaster> VendorMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=NI-LTP-124;Database=SMS;User ID=sa;Password=Admin@123;Trusted_Connection=False;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CompanyMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemNumber).HasMaxLength(50);
            entity.Property(e => e.Logo).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Mrp).HasColumnName("MRP");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CustomerMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CustomerMaster");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Comments).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FollowUpdate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DashboardMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DashboardMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<EnquiryTypeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EnquiryTypeMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EnquiryTypeName).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ItemMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ItemMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemName).HasMaxLength(50);
            entity.Property(e => e.ItemNumber).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Mrp).HasColumnName("MRP");
            entity.Property(e => e.Quantity).HasMaxLength(50);
            entity.Property(e => e.SoldQuantity).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderItmeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderItmeMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ItemName).HasMaxLength(50);
            entity.Property(e => e.ItemNumber).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderName).HasMaxLength(50);
            entity.Property(e => e.OrderNumber).HasMaxLength(50);
            entity.Property(e => e.ShipmentDetails).HasMaxLength(50);
        });

        modelBuilder.Entity<OrderTypeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("OrderTypeMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<PurchaseItemMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PurchaseItemMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemName).HasMaxLength(50);
            entity.Property(e => e.ItemNumber).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Mrp).HasColumnName("MRP");
        });

        modelBuilder.Entity<PurchaseMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PurchaseMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ShipmentDetails).HasMaxLength(50);
            entity.Property(e => e.VendorId).HasColumnName("vendorId");
        });

        modelBuilder.Entity<ReturnItemMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ReturnItemMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemName).HasMaxLength(50);
            entity.Property(e => e.ItemNumber).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Mrp).HasColumnName("MRP");
        });

        modelBuilder.Entity<ReturnMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ReturnMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InvoiceName).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.ReturnNumber).HasMaxLength(50);
            entity.Property(e => e.ShipmentDetails).HasMaxLength(50);
            entity.Property(e => e.TaxNumber).HasMaxLength(50);
            entity.Property(e => e.VendorId).HasColumnName("vendorId");
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RoleMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<SalesItemMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SalesItemMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ItemName).HasMaxLength(50);
            entity.Property(e => e.ItemNumber).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Mrp).HasColumnName("MRP");
        });

        modelBuilder.Entity<SalesMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SalesMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.InvoiceCopy).HasMaxLength(50);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ShipmentDetails).HasMaxLength(50);
        });

        modelBuilder.Entity<TaxTypeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TaxTypeMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UnitTypeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UnitTypeMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.UnitTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserMaster");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<VendorMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VendorMaster");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
