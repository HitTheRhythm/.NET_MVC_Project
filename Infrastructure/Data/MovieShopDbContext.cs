using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public class MovieShopDbContext: DbContext
    {
        // DbSets as properties
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
        {
                
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }

        // to use fluent API we need to override a nmethod OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<User>(ConfigureUser);
            //modelBuilder.Entity<UserRole>(ConfigureUserRole);
            //modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);


        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }
        private void ConfigureMovie (EntityTypeBuilder<Movie> builder)
        {
            // specify all the Fluent API rules for this model
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256).IsRequired();
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Ignore(m => m.Rating);
        }
        
        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(128);
            builder.Property(t => t.Gender);
            builder.Property(t => t.TmdbUrl);
            builder.Property(t => t.ProfilePath).HasMaxLength(2084);

        }

        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(128);
            builder.Property(t => t.Gender);
            builder.Property(t => t.TmdbUrl);
            builder.Property(t => t.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.MovieId);
            builder.Property(t => t.UserId);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast>builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(t =>new {  t.MovieId, t.CastId, t.Character });
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(t=>t.Id);
            builder.Property(t => t.UserId);
            builder.Property(t => t.PurchaseNumber);
            builder.Property(t => t.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.PurchaseDateTime);
            builder.Property(t => t.MovieId);

        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(t => new { t.MovieId, t.UserId });
            builder.Property(t => t.Rating).HasColumnType("decimal(18,2)");
            builder.Property(t => t.ReviewText);
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FirstName);
            builder.Property(t => t.LastName);
            builder.Property(t => t.DateOfBirth);
            builder.Property(t => t.Email);
            builder.Property(t => t.HashedPassword);
            builder.Property(t => t.Salt);
            builder.Property(t => t.PhoneNumber);
            builder.Property(t => t.TwoFactorEnabled);
            builder.Property(t => t.LockoutEndDate);
            builder.Property(t => t.LastLoginDateTime);
            builder.Property(t => t.IsLocked);
            builder.Property(t => t.AccessFailedCount);
        }
    }
}
