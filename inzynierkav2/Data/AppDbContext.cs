using inzynierka.Models;
using inzynierka.Models.Comments;
using inzynierkav2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracainz.Data
{
    public class AppDbContext : IdentityDbContext<WebUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie_Category>().HasKey(am => new
            {
                am.CategoryId,
                am.MovieId
            });

            modelBuilder.Entity<Movie_Category>().HasOne(m => m.Movie).WithMany(am => am.Movie_Categories).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Movie_Category>().HasOne(m => m.Category).WithMany(am => am.Movie_Categories).HasForeignKey(m => m.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Movie_Category> Movie_Categories { get; set; }
       // public DbSet<Nibyuser> Nibyusers { get; set; }
        public DbSet<MovieType> MovieTypes { get; set; }




        public DbSet<Storage> Storages { get; set; }
        public DbSet<StorageItem> StorageItems { get; set; }
        public DbSet<StorageCardItem> StorageCardItems { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

    }
}
