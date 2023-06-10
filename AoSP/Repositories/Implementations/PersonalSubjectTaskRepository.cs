using AoSP.Entities;
using AoSP.Repositories.Interfaces;

namespace AoSP.Repositories.Implementations;

public class PersonalSubjectTaskRepository : IBaseRepository<PersonalSubjectTask>
{
    private readonly ApplicationContext _db;

    public PersonalSubjectTaskRepository(ApplicationContext db)
    {
        _db = db;
    }

    public IQueryable<PersonalSubjectTask> GetAll()
    {
        return _db.PersonalSubjectTasks;
    }

    public async Task Delete(PersonalSubjectTask entity)
    {
        _db.PersonalSubjectTasks.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Create(PersonalSubjectTask entity)
    {
        await _db.PersonalSubjectTasks.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<PersonalSubjectTask> Update(PersonalSubjectTask entity)
    {
        _db.PersonalSubjectTasks.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}