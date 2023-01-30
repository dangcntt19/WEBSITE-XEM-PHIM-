using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WEBSITE_FILM_002.Models
{
    public partial class WEBSITE_FILM : DbContext
    {
        public WEBSITE_FILM()
            : base("name=WEBSITE_FILM")
        {
        }

        public virtual DbSet<ACCOUNTS> ACCOUNTS { get; set; }
        public virtual DbSet<COMMENTS> COMMENTS { get; set; }
        public virtual DbSet<FILMS> FILMS { get; set; }
        public virtual DbSet<IMAGES> IMAGES { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<VIDEOS> VIDEOS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACCOUNTS>()
                .Property(e => e.ACCOUNTID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<ACCOUNTS>()
                .Property(e => e.ACCOUNTNAME)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNTS>()
                .Property(e => e.ACCOUNTPASS)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNTS>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<ACCOUNTS>()
                .HasMany(e => e.USERS)
                .WithRequired(e => e.ACCOUNTS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<COMMENTS>()
                .Property(e => e.COMMENTID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<COMMENTS>()
                .Property(e => e.USERID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<COMMENTS>()
                .Property(e => e.FILMID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<FILMS>()
                .Property(e => e.FILMID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<FILMS>()
                .Property(e => e.USERID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<FILMS>()
                .HasMany(e => e.COMMENTS)
                .WithRequired(e => e.FILMS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IMAGES>()
                .Property(e => e.IMAGEID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<IMAGES>()
                .Property(e => e.USERID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<USERS>()
                .Property(e => e.USERID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<USERS>()
                .Property(e => e.ACCOUNTID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.COMMENTS)
                .WithRequired(e => e.USERS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.FILMS)
                .WithRequired(e => e.USERS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.IMAGES)
                .WithRequired(e => e.USERS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.VIDEOS)
                .WithRequired(e => e.USERS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VIDEOS>()
                .Property(e => e.VIDEOID)
                .HasPrecision(8, 0);

            modelBuilder.Entity<VIDEOS>()
                .Property(e => e.USERID)
                .HasPrecision(8, 0);
        }
    }
}
