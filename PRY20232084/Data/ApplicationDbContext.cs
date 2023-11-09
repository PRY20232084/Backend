using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;
using Microsoft.Data.SqlClient.DataClassification;
using PRY20232084.Models.Entities;
using PRY20232084.Models;

namespace PRY20232084.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                        
            /* START APPLICATION USER RELATIONSHIPS*/
            
            // ONE TO MANY RELATIONSHIP BETWEEN MEASUREMENTUNIT AND APPLICATIONUSER
            builder.Entity<MeasurementUnit>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(f => f.MeasurementUnits)
                .HasForeignKey(p => p.CreatedBy);

            // ONE TO MANY RELATIONSHIP BETWEEN STYLE AND APPLICATIONUSER
            builder.Entity<ProductStyle>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(f => f.Styles)
                .HasForeignKey(p => p.CreatedBy);

            // ONE TO MANY RELATIONSHIP BETWEEN SIZE AND APPLICATIONUSER
            builder.Entity<ProductSize>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(f => f.Sizes)
                .HasForeignKey(p => p.CreatedBy);

            // ONE TO MANY RELATIONSHIP BETWEEN PRODUCT AND APPLICATIONUSER
            builder.Entity<Product>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.CreatedBy);

            // ONE TO MANY RELATIONSHIP BETWEEN RAWMATERIAL AND APPLICATIONUSER
            builder.Entity<RawMaterial>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(f => f.RawMaterials)
                .HasForeignKey(p => p.CreatedBy);

            // ONE TO MANY RELATIONSHIP BETWEEN MOVEMENT AND APPLICATIONUSER
            builder.Entity<Movement>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(f => f.Movements)
                .HasForeignKey(p => p.CreatedBy);

            /* END APPLICATION USER RELATIONSHIPS*/

            // ONE TO MANY RELATIONSHIP BETWEEN PRODUCT AND SIZE
            builder.Entity<Product>()
                .HasOne(p => p.SizeRef)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.Size_ID);

            // ONE TO MANY RELATIONSHIP BETWEEN PRODUCT AND STYLE
            builder.Entity<Product>()
                .HasOne(p => p.StyleRef)
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.Style_ID);

            // ONE TO MANY RELATIONSHIP BETWEEN RAWMATERIAL AND MEASUREMENTUNIT
            builder.Entity<RawMaterial>()
                .HasOne(p => p.MeasurementUnit)
                .WithMany(f => f.RawMaterials)
                .HasForeignKey(p => p.MeasurementUnit_ID);

            // MANY TO MANY RELATIONSHIP BETWEEN RAWMATERIAL AND PRODUCT
            builder.Entity<ProductDetail>()
                .HasKey(pd => new { pd.RawMaterial_ID, pd.Product_ID });

            builder.Entity<ProductDetail>()
                .HasOne(pd => pd.RawMaterialRef)
                .WithMany(p => p.ProductDetails)
                .HasForeignKey(psba => psba.RawMaterial_ID);

            builder.Entity<ProductDetail>()
                .HasOne(pd => pd.ProductRef)
                .WithMany(au => au.ProductDetails)
                .HasForeignKey(psba => psba.Product_ID);

            // MANY TO MANY RELATIONSHIP BETWEEN RAWMATERIAL AND MOVEMENT
            builder.Entity<RawMaterialMovementDetail>()
                .HasOne(pd => pd.RawMaterialRef)
                .WithMany(p => p.RawMaterialMovementDetails)
                .HasForeignKey(psba => psba.RawMaterial_ID);

            builder.Entity<RawMaterialMovementDetail>()
                .HasOne(pd => pd.MovementRef)
                .WithMany(au => au.RawMaterialMovementDetails)
                .HasForeignKey(psba => psba.Movement_ID);

            // MANY TO MANY RELATIONSHIP BETWEEN PRODUCT AND MOVEMENT
            builder.Entity<ProductMovementDetail>()
                .HasOne(pd => pd.ProductRef)
                .WithMany(p => p.ProductMovementDetails)
                .HasForeignKey(psba => psba.Product_ID);

            builder.Entity<ProductMovementDetail>()
                .HasOne(pd => pd.MovementRef)
                .WithMany(au => au.ProductMovementDetails)
                .HasForeignKey(psba => psba.Movement_ID);
        }

        public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public virtual DbSet<Movement> Movements { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<ProductMovementDetail> ProductMovementDetails { get; set; }
        public virtual DbSet<RawMaterial> RawMaterials { get; set; }
        public virtual DbSet<RawMaterialMovementDetail> RawMaterialMovementDetails { get; set; }
        public virtual DbSet<ProductSize> Sizes { get; set; }
        public virtual DbSet<ProductStyle> Styles { get; set; }
    }
}