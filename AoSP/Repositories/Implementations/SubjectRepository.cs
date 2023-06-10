using AoSP.Entities;
using AoSP.Repositories.Interfaces;

namespace AoSP.Repositories.Implementations;

public class SubjectRepository : IBaseRepository<Subject>
{
    private readonly ApplicationContext _db;

    public SubjectRepository(ApplicationContext db)
    {
        _db = db;
    }

    public IQueryable<Subject> GetAll()
    {
        return _db.Subjects;
    }

    public async Task Delete(Subject entity)
    {
        _db.Subjects.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Create(Subject entity)
    {
        await _db.Subjects.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Subject> Update(Subject entity)
    {
        _db.Subjects.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}