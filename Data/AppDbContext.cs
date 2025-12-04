using Microsoft.EntityFrameworkCore;
using YmmcContainerTrackerApi.Models;

namespace YmmcContainerTrackerApi.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // ✅ Only the tables you care about
    public virtual DbSet<ReturnableContainers> ReturnableContainers { get; set; }
    public virtual DbSet<ReturnableContainersStage> ReturnableContainersStage { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReturnableContainers>(entity =>
        {
            entity.HasKey(e => e.ItemNo);

            entity.ToTable("ReturnableContainers", "dbo");

            entity.Property(e => e.ItemNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Item_No");

            entity.Property(e => e.PackingCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Packing_Code");

            entity.Property(e => e.PrefixCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prefix_Code");

            entity.Property(e => e.ContainerNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Container_Number");

            entity.Property(e => e.OutsideLength)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Outside_Length");

            entity.Property(e => e.OutsideWidth)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Outside_Width");

            entity.Property(e => e.OutsideHeight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Outside_Height");

            entity.Property(e => e.CollapsedHeight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Collapsed_Height");

            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Weight");

            entity.Property(e => e.PackQuantity)
                .HasColumnName("Pack_Quantity");

            entity.Property(e => e.AlternateId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Alternate_ID");
        });

        modelBuilder.Entity<ReturnableContainersStage>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("ReturnableContainers_Stage", "dbo");

            entity.Property(e => e.ItemNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Item_No");

            entity.Property(e => e.PackingCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Packing_Code");

            entity.Property(e => e.PrefixCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Prefix_Code");

            entity.Property(e => e.ContainerNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Container_Number");

            entity.Property(e => e.OutsideLength)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Outside_Length");

            entity.Property(e => e.OutsideWidth)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Outside_Width");

            entity.Property(e => e.OutsideHeight)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Outside_Height");

            entity.Property(e => e.CollapsedHeight)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Collapsed_Height");

            entity.Property(e => e.Weight)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Weight");

            entity.Property(e => e.PackQuantity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Pack_Quantity");

            entity.Property(e => e.AlternateId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Alternate_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
