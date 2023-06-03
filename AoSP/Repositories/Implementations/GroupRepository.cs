using AoSP.Entities;
using AoSP.Repositories.Interfaces;

namespace AoSP.Repositories.Implementations;

public class GroupRepository : IBaseRepository<Group>
{
    private readonly ApplicationContext _db;

    public GroupRepository(ApplicationContext db)
    {
        _db = db;
    }

    public IQueryable<Group> GetAll()
    {
        return _db.Groups;
    }

    public async Task Delete(Group entity)
    {
        _db.Groups.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Create(Group entity)
    {
        await _db.Groups.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Group> Update(Group entity)
    {
        _db.Groups.Update(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
}