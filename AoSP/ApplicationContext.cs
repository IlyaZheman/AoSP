using AoSP.Entities;
using AoSP.Enums;
using AoSP.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AoSP;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users").HasKey(x => x.Id);

            builder.HasData(
                new User { Id = 1, Login = "admin", Password = HashPasswordHelper.HashPassword("admin"), Role = Role.Admin },
                new User { Id = 2, Login = "test", Password = HashPasswordHelper.HashPassword("test"), Role = Role.Student});

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Login).HasMaxLength(100).IsRequired();

            builder.HasOne(x => x.Profile)
                   .WithOne(x => x.User)
                   .HasPrincipalKey<User>(x => x.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Profile>(builder =>
        {
            builder.ToTable("Profiles").HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(
                new Profile { Id = 1, UserId = 1 },
                new Profile { Id = 2, UserId = 2});
        });
    }
}