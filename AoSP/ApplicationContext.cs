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
    public DbSet<Mark> Marks { get; set; } = null!;

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

            builder.HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId)
                .IsRequired(false);

            builder.HasMany(g => g.Subjects)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId)
                .IsRequired(false);
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasData(new User
            {
                Id = "000000000000",
                Login = "admin",
                Password = Helper.HashPassword("admin"),
                Role = Role.Admin,
            });

            builder.HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId)
                .IsRequired(false);

            builder.HasMany(g => g.PersonalSubjects)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .IsRequired(false);

            builder.HasOne(x => x.Subject)
                .WithOne(x => x.Teacher)
                .HasForeignKey<User>(x => x.SubjectId)
                .IsRequired(false);
        });

        modelBuilder.Entity<PersonalSubject>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.User)
                .WithMany(g => g.PersonalSubjects)
                .HasForeignKey(s => s.UserId)
                .IsRequired(false);

            builder.HasMany(g => g.PersonalSubjectTasks)
                .WithOne(s => s.PersonalSubject)
                .HasForeignKey(s => s.PersonalSubjectId)
                .IsRequired(false);

            builder.HasOne(s => s.Subject)
                .WithMany(g => g.PersonalSubjects)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired(false);
        });

        modelBuilder.Entity<PersonalSubjectTask>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.PersonalSubject)
                .WithMany(g => g.PersonalSubjectTasks)
                .HasForeignKey(s => s.PersonalSubjectId)
                .IsRequired(false);

            builder.HasOne(s => s.SubjectTask)
                .WithMany(g => g.PersonalSubjectTasks)
                .HasForeignKey(s => s.SubjectTaskId)
                .IsRequired(false);
        });

        modelBuilder.Entity<Subject>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.Group)
                .WithMany(g => g.Subjects)
                .HasForeignKey(s => s.GroupId)
                .IsRequired(false);

            builder.HasMany(g => g.SubjectTasks)
                .WithOne(s => s.Subject)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired(false);

            builder.HasMany(g => g.Marks)
                .WithOne(s => s.Subject)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired(false);

            builder.HasOne(x => x.Teacher)
                .WithOne(x => x.Subject)
                .HasForeignKey<User>(x => x.SubjectId)
                .IsRequired(false);

            builder.HasMany(g => g.PersonalSubjects)
                .WithOne(s => s.Subject)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired(false);
        });

        modelBuilder.Entity<SubjectTask>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.Subject)
                .WithMany(g => g.SubjectTasks)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired(false);

            builder.HasMany(g => g.PersonalSubjectTasks)
                .WithOne(s => s.SubjectTask)
                .HasForeignKey(s => s.SubjectTaskId)
                .IsRequired(false);
        });

        modelBuilder.Entity<Mark>(builder =>
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(s => s.Subject)
                .WithMany(g => g.Marks)
                .HasForeignKey(s => s.SubjectId)
                .IsRequired(false);
        });
    }
}