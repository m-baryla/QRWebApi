using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QRWebApi.Models
{
    public partial class QRAppDBContext : DbContext
    {
        public QRAppDBContext()
        {
        }

        public QRAppDBContext(DbContextOptions<QRAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DictEmailAdress> DictEmailAdresses { get; set; }
        public virtual DbSet<DictEquipment> DictEquipments { get; set; }
        public virtual DbSet<DictLocation> DictLocations { get; set; }
        public virtual DbSet<DictPriority> DictPriorities { get; set; }
        public virtual DbSet<DictStatus> DictStatus { get; set; }
        public virtual DbSet<DictTicketType> DictTicketTypes { get; set; }
        public virtual DbSet<EmailSenderConfig> EmailSenderConfigs { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
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

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictLocation>(entity =>
            {
                entity.ToTable("DICT_Location");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictPriority>(entity =>
            {
                entity.ToTable("DICT_Priority");

                entity.Property(e => e.PriorityType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictStatus>(entity =>
            {
                entity.ToTable("DICT_Status");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictTicketType>(entity =>
            {
                entity.ToTable("DICT_TicketType");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmailSenderConfig>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EmailSenderConfig");

                entity.Property(e => e.EmailPassword)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EmailUser)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailFrom)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MailHost)
                    .IsRequired()
                    .HasMaxLength(200)
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

                entity.Property(e => e.IdPriority).HasColumnName("Id_priority");

                entity.Property(e => e.IdStatus).HasColumnName("Id_status");

                entity.Property(e => e.IdTicketType).HasColumnName("Id_ticketType");

                entity.Property(e => e.Topic)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
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

                entity.HasOne(d => d.IdPriorityNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdPriority)
                    .HasConstraintName("FK_Tickets_DICT_Priority");

                entity.HasOne(d => d.IdStatusesNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_DICT_Status");

                entity.HasOne(d => d.IdTicketTypeNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTicketType)
                    .HasConstraintName("FK_Tickets_DICT_TicketType");
            });

            modelBuilder.Entity<Wiki>(entity =>
            {
                entity.ToTable("Wiki");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.IdEquipment).HasColumnName("Id_equipment");

                entity.Property(e => e.IdLocation).HasColumnName("Id_location");

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
