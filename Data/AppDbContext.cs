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

    // Only the tables you care about
    public virtual DbSet<ReturnableContainers> ReturnableContainers { get; set; }
    public virtual DbSet<ReturnableContainersStage> ReturnableContainersStage { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<ContainerAuditLog> ContainerAuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReturnableContainers>(entity =>
        {
            entity.HasKey(e => e.ItemNo);
            entity.HasIndex(e => e.ItemNo).IsUnique();

            entity.ToTable("RtrnCotnr_Main", "RtrnCotnr");

            entity.Property(e => e.ItemNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Item_No")
                .IsRequired();

            entity.Property(e => e.PackingCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Packing_Code")
                .IsRequired(false);

            entity.Property(e => e.PrefixCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Prefix_Code")
                .IsRequired(false);

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
            entity.ToTable("RtrnCotnr_Stage", "RtrnCotnr");

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

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Username);
            
            entity.ToTable("UserRoles", "dbo");

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.DisplayName)
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .HasMaxLength(100);

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");
        });

        modelBuilder.Entity<ContainerAuditLog>(entity =>
        {
            entity.HasKey(e => e.Id);
  
            entity.ToTable("ContainerAuditLogs", "dbo");

            entity.Property(e => e.ItemNo)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Action)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.UserRole)
                .HasMaxLength(20);

            entity.Property(e => e.Timestamp)
                .IsRequired();

            entity.Property(e => e.ChangedFields)
                .HasMaxLength(500);

            entity.Property(e => e.IpAddress)
                .HasMaxLength(45);

            entity.Property(e => e.UserAgent)
                .HasMaxLength(500);

            entity.Property(e => e.Notes)
                .HasMaxLength(1000);

            // Create index on commonly queried fields
            entity.HasIndex(e => e.ItemNo);
            entity.HasIndex(e => e.Username);
            entity.HasIndex(e => e.Timestamp);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
