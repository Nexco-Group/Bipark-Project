using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ronisim.Models
{
    public partial class ronisimContext : DbContext
    {
        public ronisimContext()
        {
        }

        public ronisimContext(DbContextOptions<ronisimContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Balance> Balance { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<iUser> iUser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=temp2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessLevel)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(100);

                entity.Property(e => e.JoiningDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasMaxLength(100);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Consider).HasDefaultValueSql("((1))");

                entity.Property(e => e.Date)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId).HasColumnName("Request_Id");

                entity.Property(e => e.Time)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Who)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.WhoId).HasColumnName("Who_Id");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.DeviceName).HasMaxLength(100);

                entity.Property(e => e.DeviceType).HasMaxLength(100);

                entity.Property(e => e.TrackDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TrackTime)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Who)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.WhoId).HasColumnName("Who_Id");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Date)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Link).HasMaxLength(100);

                entity.Property(e => e.iUser).HasDefaultValueSql("((0))");

                entity.Property(e => e.Ruser)
                    .HasColumnName("RUser")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Text).HasMaxLength(150);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.CreatDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");


                entity.Property(e => e.OrderDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTime)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FromTime)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ToTime)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PackId).HasColumnName("Pack_Id");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.TotalPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.WhoId).HasColumnName("Who_Id");
            });

            modelBuilder.Entity<iUser>(entity =>
            {
                entity.Property(e => e.AccessLevel)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Email)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("FName")
                    .HasMaxLength(100);

                entity.Property(e => e.JoiningDate)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasColumnName("LName")
                    .HasMaxLength(100);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Verify)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });
        }
    }
}
