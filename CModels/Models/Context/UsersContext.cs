using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CModels.Models;
using Microsoft.Extensions.Configuration;

namespace CModels.Context
{
    public partial class UsersContext : DbContext
    {

        private static string Connection;
        public static string Config;
        public IConfiguration Configuration { get; }

        //public UsersContext()
        //{
        //}

        public UsersContext(DbContextOptions<UsersContext> options, IConfiguration config)
            : base(options)
        {
            Configuration = config;
            Connection = Configuration.GetConnectionString("DevelopLocal");
        }

        public static string GetConnection()
        {
            return Connection;
        }

        public virtual DbSet<UsersData> UsersData { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=DESKTOP-I07C51R;Database=Users;Persist Security Info=True;User ID=Deybi;Password=Private2019;MultipleActiveResultSets=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersData>(entity =>
            {
                entity.HasKey(e => e.IdUserData)
                    .HasName("PK__UsersDat__860CD80C7EEBBCC0");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
