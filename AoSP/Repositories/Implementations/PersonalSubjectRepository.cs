using AoSP.Entities;
using AoSP.Repositories.Interfaces;

namespace AoSP.Repositories.Implementations;

public class PersonalSubjectRepository : IBaseRepository<PersonalSubject>
{
    private readonly ApplicationContext _db;

    public PersonalSubjectRepository(ApplicationContext db)
    {
        _db = db;
    }

    public IQueryable<PersonalSubject> GetAll()
    {
        return _db.PersonalSubjects;
    }

    public async Task Delete(PersonalSubject entity)
    {
        _db.PersonalSubjects.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Create(PersonalSubject entity)
    {
        await _db.PersonalSubjects.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<PersonalSubject> Update(PersonalSubject entity)
    {
        _db.PersonalSubjects.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}