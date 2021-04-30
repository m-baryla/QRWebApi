using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QRWebApi.Models
{
    public partial class QRappContext : DbContext
    {
        public QRappContext()
        {
        }

        public QRappContext(DbContextOptions<QRappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DictEmailAdress> DictEmailAdresses { get; set; }
        public virtual DbSet<DictEquipment> DictEquipments { get; set; }
        public virtual DbSet<DictLocation> DictLocations { get; set; }
        public virtual DbSet<DictPermission> DictPermissions { get; set; }
        public virtual DbSet<DictStatu> DictStatus { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketsHistory> TicketsHistories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wiki> Wikis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=QRappDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DictEmailAdress>(entity =>
            {
                entity.ToTable("DICT_EmailAdress");

                entity.Property(e => e.EmailAdressNotify)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictEquipment>(entity =>
            {
                entity.ToTable("DICT_Equipment");

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictLocation>(entity =>
            {
                entity.ToTable("DICT_Location");

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictPermission>(entity =>
            {
                entity.ToTable("DICT_Permissions");

                entity.Property(e => e.Permissions)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictStatu>(entity =>
            {
                entity.ToTable("DICT_Status");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEmailAdress).HasColumnName("Id_emailAdress");

                entity.Property(e => e.IdEquipment).HasColumnName("Id_equipment");

                entity.Property(e => e.IdLocation).HasColumnName("Id_location");

                entity.Property(e => e.IdStatus).HasColumnName("Id_status");

                entity.Property(e => e.IdUser).HasColumnName("Id_user");

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Topic)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmailAdressNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEmailAdress)
                    .HasConstraintName("FK_Tickets_DICT_EmailAdress");

                entity.HasOne(d => d.IdEquipmentNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdEquipment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_DICT_Equipment");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_DICT_Location");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_DICT_Status");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Tickets_Users");
            });

            modelBuilder.Entity<TicketsHistory>(entity =>
            {
                entity.ToTable("TicketsHistory");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdEmailAdress).HasColumnName("Id_emailAdress");

                entity.Property(e => e.IdEquipment).HasColumnName("Id_equipment");

                entity.Property(e => e.IdLocation).HasColumnName("Id_location");

                entity.Property(e => e.IdStatus).HasColumnName("Id_status");

                entity.Property(e => e.IdUser).HasColumnName("Id_user");

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Topic)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IdPermission).HasColumnName("Id_Permission");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPermissionNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdPermission)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_DICT_Permissions");
            });

            modelBuilder.Entity<Wiki>(entity =>
            {
                entity.ToTable("Wiki");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.IdEquipment).HasColumnName("Id_equipment");

                entity.Property(e => e.IdLocation).HasColumnName("Id_location");

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Topic).HasMaxLength(50);

                entity.HasOne(d => d.IdEquipmentNavigation)
                    .WithMany(p => p.Wikis)
                    .HasForeignKey(d => d.IdEquipment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wiki_DICT_Equipment");

                entity.HasOne(d => d.IdLocationNavigation)
                    .WithMany(p => p.Wikis)
                    .HasForeignKey(d => d.IdLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wiki_DICT_Location");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
