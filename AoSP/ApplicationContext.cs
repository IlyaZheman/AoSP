using AoSP.Entities;
using AoSP.Enums;
using AoSP.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AoSP;

public sealed class ApplicationContext : DbContext
{
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<PersonalSubject> PersonalSubjects { get; set; } = null!;
    public DbSet<PersonalSubjectTask> PersonalSubjectTasks { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            // builder.ToTable("Users").HasKey(x => x.Id);

            builder.HasData(new User
                {
                    Id = 1,
                    Login = "admin",
                    Password = HashPasswordHelper.HashPassword("admin"),
                    Name = "Илья",
                    Surname = "Жеман",
                    Patronymic = "Иванович",
                    Role = Role.Admin,
                    GroupId = 1,
                },
                new User
                {
                    Id = 2,
                    Login = "test",
                    Password = HashPasswordHelper.HashPassword("test"),
                    Name = "Елизавета",
                    Surname = "Шарова",
                    Patronymic = "Романовна",
                    Role = Role.Student,
                    GroupId = 1,
                },
                new User
                {
                    Id = 3,
                    Login = "test",
                    Password = HashPasswordHelper.HashPassword("test"),
                    Name = "Елизавета",
                    Surname = "Шарова",
                    Patronymic = "Романовна",
                    Role = Role.Student,
                    GroupId = 2,
                },
                new User
                {
                    Id = 4,
                    Login = "test",
                    Password = HashPasswordHelper.HashPassword("test"),
                    Name = "Елизавета",
                    Surname = "Шарова",
                    Patronymic = "Романовна",
                    Role = Role.Student,
                    GroupId = 2,
                });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            // builder.Property(x => x.Password).IsRequired();
            // builder.Property(x => x.Login).HasMaxLength(100).IsRequired();

            builder.HasOne(s => s.Group)
                .WithMany(g => g.Users)
                .HasForeignKey(s => s.GroupId);

            builder.HasMany(g => g.PersonalSubjects)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);
        });

        modelBuilder.Entity<Group>(builder =>
        {
            builder.HasData(new Group
                {
                    Id = 1,
                    Title = "ИВТ",
                },
                new Group
                {
                    Id = 2,
                    Title = "ИСТ",
                });

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(g => g.Users)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId);
        });

        modelBuilder.Entity<PersonalSubject>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(new PersonalSubject
                {
                    Id = 1,
                    UserId = 2,
                },
                new PersonalSubject
                {
                    Id = 2,
                    UserId = 4,
                });

            builder.HasOne(s => s.User)
                .WithMany(g => g.PersonalSubjects)
                .HasForeignKey(s => s.UserId);

            builder.HasMany(g => g.PersonalSubjectTasks)
                .WithOne(s => s.PersonalSubject)
                .HasForeignKey(s => s.PersonalSubjectId);
        });

        modelBuilder.Entity<PersonalSubjectTask>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(new PersonalSubjectTask
                {
                    Id = 1,
                    Score = 80,
                    PersonalSubjectId = 1,
                },
                new PersonalSubjectTask
                {
                    Id = 2,
                    Score = 95,
                    PersonalSubjectId = 2,
                });

            builder.HasOne(s => s.PersonalSubject)
                .WithMany(g => g.PersonalSubjectTasks)
                .HasForeignKey(s => s.PersonalSubjectId);
        });
    }
}