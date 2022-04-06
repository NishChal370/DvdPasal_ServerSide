using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DvD_Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DvD_Api.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Dvdcategory> Dvdcategories { get; set; }
        public virtual DbSet<Dvdcopy> Dvdcopies { get; set; }
        public virtual DbSet<Dvdtitle> Dvdtitles { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<LoanType> LoanTypes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MembershipCategory> MembershipCategories { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=localhost;initial catalog=DvD_db;trusted_connection=true;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.ActorNumber)
                    .HasName("Actor_PK");

                entity.ToTable("Actor");

                entity.Property(e => e.ActorNumber).ValueGeneratedNever();

                entity.Property(e => e.ActorLastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActorName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dvdcategory>(entity =>
            {
                entity.HasKey(e => e.CategoryNumber)
                    .HasName("DVDCategory_PK");

                entity.ToTable("DVDCategory");

                entity.Property(e => e.CategoryNumber).ValueGeneratedNever();

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dvdcopy>(entity =>
            {
                entity.HasKey(e => e.CopyNumber)
                    .HasName("DVDCopy_PK");

                entity.ToTable("DVDCopy");

                entity.Property(e => e.CopyNumber).ValueGeneratedNever();

                entity.Property(e => e.DatePurchased).HasColumnType("date");

                entity.Property(e => e.Dvdnumber).HasColumnName("DVDNumber");

                entity.HasOne(d => d.DvdnumberNavigation)
                    .WithMany(p => p.Dvdcopies)
                    .HasForeignKey(d => d.Dvdnumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DVDTitle_FKv2");
            });

            modelBuilder.Entity<Dvdtitle>(entity =>
            {
                entity.HasKey(e => e.Dvdnumber)
                    .HasName("DVDTitle_PK");

                entity.ToTable("DVDTitle");

                entity.Property(e => e.Dvdnumber)
                    .ValueGeneratedNever()
                    .HasColumnName("DVDNumber");

                entity.Property(e => e.DateReleased).HasColumnType("date");

                entity.Property(e => e.PenaltyCharge).HasColumnType("decimal(28, 0)");

                entity.Property(e => e.StandardCharge).HasColumnType("decimal(28, 0)");

                entity.HasOne(d => d.CategoryNumberNavigation)
                    .WithMany(p => p.Dvdtitles)
                    .HasForeignKey(d => d.CategoryNumber)
                    .HasConstraintName("DVDCategory_FK");

                entity.HasOne(d => d.ProducerNumberNavigation)
                    .WithMany(p => p.Dvdtitles)
                    .HasForeignKey(d => d.ProducerNumber)
                    .HasConstraintName("Producer_FK");

                entity.HasOne(d => d.StudioNumberNavigation)
                    .WithMany(p => p.Dvdtitles)
                    .HasForeignKey(d => d.StudioNumber)
                    .HasConstraintName("Studio_FK");

                entity.HasMany(d => d.ActorNumbers)
                    .WithMany(p => p.Dvdnumbers)
                    .UsingEntity<Dictionary<string, object>>(
                        "CastMember",
                        l => l.HasOne<Actor>().WithMany().HasForeignKey("ActorNumber").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Actor_FK"),
                        r => r.HasOne<Dvdtitle>().WithMany().HasForeignKey("Dvdnumber").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("DVDTitle_FK"),
                        j =>
                        {
                            j.HasKey("Dvdnumber", "ActorNumber").HasName("CastMember_PK");

                            j.ToTable("CastMember");

                            j.IndexerProperty<int>("Dvdnumber").HasColumnName("DVDNumber");
                        });
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.LoanNumber)
                    .HasName("Loan_PK");

                entity.ToTable("Loan");

                entity.Property(e => e.LoanNumber).ValueGeneratedNever();

                entity.Property(e => e.DateDue).HasColumnType("date");

                entity.Property(e => e.DateOut).HasColumnType("date");

                entity.Property(e => e.DateReturned).HasColumnType("date");

                entity.HasOne(d => d.CopyNumberNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.CopyNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DVDCopy_FK");

                entity.HasOne(d => d.MemberNumberNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.MemberNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Member_FK");

                entity.HasOne(d => d.TypeNumberNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.TypeNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LoanType_FK");
            });

            modelBuilder.Entity<LoanType>(entity =>
            {
                entity.HasKey(e => e.LoanTypeNumber)
                    .HasName("LoanType_PK");

                entity.ToTable("LoanType");

                entity.Property(e => e.LoanTypeNumber).ValueGeneratedNever();

                entity.Property(e => e.LoanType1)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("LoanType");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MemberNumber)
                    .HasName("Member_PK");

                entity.ToTable("Member");

                entity.Property(e => e.MemberNumber).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategoryNumberNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CategoryNumber)
                    .HasConstraintName("MembershipCategory_FK");
            });

            modelBuilder.Entity<MembershipCategory>(entity =>
            {
                entity.HasKey(e => e.McategoryNumber)
                    .HasName("MembershipCategory_PK");

                entity.ToTable("MembershipCategory");

                entity.Property(e => e.McategoryNumber).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.HasKey(e => e.ProducerNumber)
                    .HasName("Producer_PK");

                entity.ToTable("Producer");

                entity.Property(e => e.ProducerNumber).ValueGeneratedNever();

                entity.Property(e => e.ProducerName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Studio>(entity =>
            {
                entity.HasKey(e => e.StudioNumber)
                    .HasName("Studio_PK");

                entity.ToTable("Studio");

                entity.Property(e => e.StudioNumber).ValueGeneratedNever();

                entity.Property(e => e.StudioName)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });   
        }
    }
}
