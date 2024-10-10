using RealStateApp.Models;
using Microsoft.EntityFrameworkCore;

namespace RealStateApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SalesOffice> SalesOffices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PropOwnerTable> PropOwnerTables { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmpID);

            modelBuilder.Entity<SalesOffice>()
                .HasKey(so => so.OfficeID);

            modelBuilder.Entity<Address>()
                .HasKey(a => a.AddressID);
            
            modelBuilder.Entity<Owner>()
                .HasKey(e => e.OwnerID);

            modelBuilder.Entity<Property>()
                .HasKey(so => so.PropertyID);

            modelBuilder.Entity<User>()
                .HasKey(a => a.UserID);

            // Configure composite key
            modelBuilder.Entity<PropOwnerTable>()
                .HasKey(a => new { a.OwnerID, a.PropertyID});

            // Configure one-to-many relationship between SalesOffices and Employees
            modelBuilder.Entity<Employee>()
                .HasOne<SalesOffice>()
                .WithMany()
                .HasForeignKey(e => e.SalesOfficeID);

            // Configure one-to-many relationship between Owners and PropOwnerTable
            modelBuilder.Entity<PropOwnerTable>()
                .HasOne<Owner>()
                .WithMany()
                .HasForeignKey(e => e.OwnerID);

            // Configure one-to-many relationship between Properties and PropOwnerTable
            modelBuilder.Entity<PropOwnerTable>()
                .HasOne<Property>()
                .WithMany()
                .HasForeignKey(e => e.PropertyID);

            // Configure one-to-one relationship between SalesOffices and Employees (Manager)
            modelBuilder.Entity<SalesOffice>()
                .HasOne<Employee>()
                .WithOne()
                .HasForeignKey<SalesOffice>(so => so.ManagedByEmployeeID)
                .IsRequired(false);

            // Configure one-to-many relationship between Addresses and SalesOffices
            modelBuilder.Entity<SalesOffice>()
                .HasOne<Address>()
                .WithMany()
                .HasForeignKey(so => so.AddressID);

            // Configure one-to-many relationship between SalesOffices and Properties
            modelBuilder.Entity<Property>()
                .HasOne<SalesOffice>()
                .WithMany()
                .HasForeignKey(so => so.SalesOfficeID);

            // Configure current timestamp for CreatedAt column
            modelBuilder.Entity<Employee>()
                .Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<SalesOffice>()
                .Property(s => s.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Address>()
                .Property(a => a.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Property>()
                .Property(p => p.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Owner>()
                .Property(o => o.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<PropOwnerTable>()
                .Property(po => po.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Configure decimal precision for PercentOwned property
            modelBuilder.Entity<PropOwnerTable>()
                .Property(po => po.PercentOwned)
                .HasColumnType("decimal(5, 2)");

            // Mapping datatype
            modelBuilder.Entity<Employee>()
                .Property(e => e.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .HasColumnType("DATE");

            // Mapping datatype, and make UserEmail property required and unique
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.UserID)
                .HasColumnName("UserID")
                .HasColumnType("char(36)");
                
                user.Property(u => u.UserEmail)
                .HasColumnName("UserEmail")
                .IsRequired();

                user.HasIndex(u => u.UserEmail)
                .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
