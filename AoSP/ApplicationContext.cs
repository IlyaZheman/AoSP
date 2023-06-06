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
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<SubjectTask> SubjectTasks { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(new Group
            {
                Id = 1,
                Title = "None",
            });
            
            builder.HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId);
            
            builder.HasMany(g => g.Subjects)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId);
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(new User
            {
                Id = 1,
                Login = "admin",
                Password = HashPasswordHelper.HashPassword("admin"),
                Role = Role.Admin,
                GroupId = 1,
            });

            builder.HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);

            builder.HasMany(g => g.PersonalSubjects)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);
        });

        modelBuilder.Entity<PersonalSubject>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

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

            builder.HasOne(s => s.PersonalSubject)
                .WithMany(g => g.PersonalSubjectTasks)
                .HasForeignKey(s => s.PersonalSubjectId);
        });

        modelBuilder.Entity<Subject>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.Group)
                .WithMany(g => g.Subjects)
                .HasForeignKey(s => s.GroupId);

            builder.HasMany(g => g.SubjectTasks)
                .WithOne(s => s.Subject)
                .HasForeignKey(s => s.SubjectId);

            builder.HasMany(g => g.Marks)
                .WithOne(s => s.Subject)
                .HasForeignKey(s => s.SubjectId);
        });

        modelBuilder.Entity<SubjectTask>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.Subject)
                .WithMany(g => g.SubjectTasks)
                .HasForeignKey(s => s.SubjectId);
        });

        modelBuilder.Entity<Mark>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.Subject)
                .WithMany(g => g.Marks)
                .HasForeignKey(s => s.SubjectId);
        });
    }
}