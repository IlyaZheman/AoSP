using AoSP.Entities;
using AoSP.Repositories.Interfaces;

namespace AoSP.Repositories.Implementations;

public class MarkRepository : IBaseRepository<Mark>
{
    private readonly ApplicationContext _db;

    public MarkRepository(ApplicationContext db)
    {
        _db = db;
    }

    public IQueryable<Mark> GetAll()
    {
        return _db.Marks;
    }

    public async Task Delete(Mark entity)
    {
        _db.Marks.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Create(Mark entity)
    {
        await _db.Marks.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Mark> Update(Mark entity)
    {
        _db.Marks.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}