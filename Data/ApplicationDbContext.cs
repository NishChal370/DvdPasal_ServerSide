using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DvD_Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DvD_Api.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<RopeyUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<DvDimage> DvDimages { get; set; }
        public virtual DbSet<Dvdcategory> Dvdcategories { get; set; }
        public virtual DbSet<Dvdcopy> Dvdcopies { get; set; }
        public virtual DbSet<Dvdtitle> Dvdtitles { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<LoanType> LoanTypes { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MembershipCategory> MembershipCategories { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.ActorNumber)
                    .HasName("Actor_PK");
            });

            modelBuilder.Entity<DvDimage>(entity =>
            {
                entity.HasOne(d => d.DvDnumberNavigation)
                    .WithMany(p => p.DvDimages)
                    .HasForeignKey(d => d.DvDnumber)
                    .HasConstraintName("DVDTitle_FKv3");
            });

            modelBuilder.Entity<Dvdcategory>(entity =>
            {
                entity.HasKey(e => e.CategoryNumber)
                    .HasName("DVDCategory_PK");
            });

            modelBuilder.Entity<Dvdcopy>(entity =>
            {
                entity.HasKey(e => e.CopyNumber)
                    .HasName("DVDCopy_PK");

                entity.HasOne(d => d.DvdnumberNavigation)
                    .WithMany(p => p.Dvdcopies)
                    .HasForeignKey(d => d.Dvdnumber)
                    .HasConstraintName("DVDTitle_FKv2");
            });

            modelBuilder.Entity<Dvdtitle>(entity =>
            {
                entity.HasKey(e => e.DvdNumber)
                    .HasName("DVDTitle_PK");

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
                    .WithMany(p => p.DvdNumbers)
                    .UsingEntity<Dictionary<string, object>>(
                        "CastMember",
                        l => l.HasOne<Actor>().WithMany().HasForeignKey("ActorNumber").HasConstraintName("Actor_FK"),
                        r => r.HasOne<Dvdtitle>().WithMany().HasForeignKey("DvdNumber").HasConstraintName("DVDTitle_FK"),
                        j =>
                        {
                            j.HasKey("DvdNumber", "ActorNumber").HasName("CastMember_PK");

                            j.ToTable("CastMember");
                        });
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.LoanNumber)
                    .HasName("Loan_PK");

                entity.HasOne(d => d.CopyNumberNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.CopyNumber)
                    .HasConstraintName("DVDCopy_FK");

                entity.HasOne(d => d.MemberNumberNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.MemberNumber)
                    .HasConstraintName("Member_FK");

                entity.HasOne(d => d.TypeNumberNavigation)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.TypeNumber)
                    .HasConstraintName("LoanType_FK");
            });

            modelBuilder.Entity<LoanType>(entity =>
            {
                entity.HasKey(e => e.LoanTypeNumber)
                    .HasName("LoanType_PK");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.MemberNumber)
                    .HasName("Member_PK");

                entity.HasOne(d => d.CategoryNumberNavigation)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.CategoryNumber)
                    .HasConstraintName("MembershipCategory_FK");
            });

            modelBuilder.Entity<MembershipCategory>(entity =>
            {
                entity.HasKey(e => e.McategoryNumber)
                    .HasName("MembershipCategory_PK");
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.HasKey(e => e.ProducerNumber)
                    .HasName("Producer_PK");
            });

            modelBuilder.Entity<Studio>(entity =>
            {
                entity.HasKey(e => e.StudioNumber)
                    .HasName("Studio_PK");
            });


        }
    }
}
