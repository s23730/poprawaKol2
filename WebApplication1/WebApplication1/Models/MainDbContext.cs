using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MainDbContext()
        {
        }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("STRING DO SQL");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>(p =>
            {
                p.HasKey(e => e.OrganizationID);
                p.Property(e => e.OrganizationID).ValueGeneratedOnAdd();
                p.Property(e => e.OrganizationName).IsRequired().HasMaxLength(100);
                p.Property(e => e.OrganizationDomain).IsRequired().HasMaxLength(50);

                p.HasData(
                    new Organization { OrganizationID=1, OrganizationName="O1", OrganizationDomain="DOM1" },
                    new Organization { OrganizationID=2, OrganizationName="O2", OrganizationDomain="DOM2" }
                );
            });
            modelBuilder.Entity<Team>(d =>
            {
                d.HasKey(d => d.TeamID);
                d.Property(e => e.TeamID).ValueGeneratedOnAdd();
                d.HasOne(p => p.Organization).WithMany(i => i.Teams).HasForeignKey(i => i.OrganizationID);
                d.Property(d => d.TeamName).IsRequired().HasMaxLength(100);
                d.Property(d => d.TeamDescription).HasMaxLength(500);

                d.HasData(
                    new Team { TeamID=1, OrganizationID=1,TeamName="T1",TeamDescription="TD1" },
                    new Team { TeamID=2, OrganizationID=2,TeamName="T2",TeamDescription="TD2" }
                );
            }
            );
            modelBuilder.Entity<File>(p =>
            {
                p.HasKey(p => new { p.FileID,p.TeamID});
                p.Property(p => p.FileName).IsRequired().HasMaxLength(100);
                p.Property(p => p.FileExtension).IsRequired().HasMaxLength(4);
                p.Property(p => p.FileSize).IsRequired();
                p.HasOne(e => e.Team).WithMany(e => e.Files).HasForeignKey(e => e.TeamID);

                p.HasData(
                    new File { FileID=1,TeamID=1,FileName="F1",FileExtension="txt",FileSize=100 },
                    new File { FileID=2,TeamID=2,FileName="F2",FileExtension="cs",FileSize=200 }
                );
            }
            );
            modelBuilder.Entity<Member>(m =>
            {
                m.HasKey(m => m.MemberID);
                m.Property(e => e.MemberID).ValueGeneratedOnAdd();
                m.Property(m => m.MemberName).IsRequired().HasMaxLength(20);
                m.Property(m => m.MemberSurname).IsRequired().HasMaxLength(50);
                m.Property(m => m.MemberNickName).HasMaxLength(20);
                m.HasOne(m => m.Organization).WithMany(m => m.Members).HasForeignKey(m => m.OrganizationID);

                m.HasData(
                    new Member { MemberID=1,OrganizationID=1,MemberName="Brad",MemberSurname="Ogleg",MemberNickName="OG" },
                    new Member { MemberID=1,OrganizationID=1,MemberName="Patricia",MemberSurname="Ghost",MemberNickName="Wraith" }
                );
            }
            );
            modelBuilder.Entity<Membership>(p =>
            {
                p.HasKey(p => new { p.MemberID, p.TeamID });
                p.Property(p => p.MembershipDate).IsRequired();
                p.HasOne(e => e.Member).WithMany(e => e.Memberships).HasForeignKey(e => e.MemberID);
                p.HasOne(e => e.Team).WithMany(e => e.Memberships).HasForeignKey(e => e.TeamID);

                p.HasData(
                    new Membership { MemberID=1,TeamID=1,MembershipDate=DateTime.Now }
                );
            }
            );
        }
    }
}
