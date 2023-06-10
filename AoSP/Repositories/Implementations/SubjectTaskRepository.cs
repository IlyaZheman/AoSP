using AoSP.Entities;
using AoSP.Repositories.Interfaces;

namespace AoSP.Repositories.Implementations;

public class SubjectTaskRepository : IBaseRepository<SubjectTask>
{
    private readonly ApplicationContext _db;

    public SubjectTaskRepository(ApplicationContext db)
    {
        _db = db;
    }

    public IQueryable<SubjectTask> GetAll()
    {
        return _db.SubjectTasks;
    }

    public async Task Delete(SubjectTask entity)
    {
        _db.SubjectTasks.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Create(SubjectTask entity)
    {
        await _db.SubjectTasks.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<SubjectTask> Update(SubjectTask entity)
    {
        _db.SubjectTasks.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}