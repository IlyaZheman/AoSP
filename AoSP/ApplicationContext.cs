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
                // new User { Id = 1, Surname = "Белевич", Name = "Феликс", Patronymic = "Иванович", Role = Role.Teacher },
                // new User { Id = 2, Surname = "Слобожанин", Name = "Дмитрий", Patronymic = "Кузьмич", Role = Role.Teacher },
                // new User { Id = 3, Surname = "Усенко", Name = "Игнат", Patronymic = "Ефимович", Role = Role.Admin },
                // new User { Id = 4, Surname = "Прохорова", Name = "Ася", Patronymic = "Себастьяновна", Role = Role.Student },
                // new User { Id = 5, Surname = "Панкратова", Name = "Арина", Patronymic = "Валентиновна", Role = Role.Teacher },
                // new User { Id = 6, Surname = "Брагина", Name = "Галина", Patronymic = "Юрьевна", Role = Role.Student },
                // new User { Id = 7, Surname = "Генкин", Name = "Валентин", Patronymic = "Викторович", Role = Role.Student },
                // new User { Id = 8, Surname = "Боголюбова", Name = "Виктория", Patronymic = "Юлиановна", Role = Role.Student },
                // new User { Id = 9, Surname = "Андрюшин", Name = "Прохор", Patronymic = "Евгеньевич", Role = Role.Student },
                // new User { Id = 10, Surname = "Якунькина", Name = "Маргарита", Patronymic = "Егоровна", Role = Role.Student },
                // new User { Id = 11, Surname = "Преображенский", Name = "Никифор", Patronymic = "Нифонтович", Role = Role.Admin },
                // new User { Id = 12, Surname = "Сязи", Name = "Рада", Patronymic = "Климентьевна", Role = Role.Student },
                // new User { Id = 13, Surname = "Коптильникова", Name = "Лана", Patronymic = "Романовна", Role = Role.Student },
                // new User { Id = 14, Surname = "Качурин", Name = "Яков", Patronymic = "Евгеньевич", Role = Role.Student },
                // new User { Id = 15, Surname = "Саянский", Name = "Иван", Patronymic = "Даниилович", Role = Role.Student },
                // new User { Id = 16, Surname = "Яблонева", Name = "Арина", Patronymic = "Ильишна", Role = Role.Teacher },
                // new User { Id = 17, Surname = "Барышева", Name = "Полина", Patronymic = "Николаевна", Role = Role.Student },
                // new User { Id = 18, Surname = "Рыбьякова", Name = "Алина", Patronymic = "Дмитриевна", Role = Role.Student },
                // new User { Id = 19, Surname = "Цаплин", Name = "Марк", Patronymic = "Венедиктович", Role = Role.Student },
                // new User { Id = 20, Surname = "Деменков", Name = "Роман", Patronymic = "Денисович", Role = Role.Student },
                new User {Id = 21, Login = "admin", Password = HashPasswordHelper.HashPassword("admin"), Role = Role.Admin});

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            
            builder.HasOne(x => x.Profile)
                   .WithOne(x => x.User)
                   .HasPrincipalKey<User>(x => x.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        });
    }
}