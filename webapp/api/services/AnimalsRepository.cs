using Microsoft.EntityFrameworkCore;
using domain;

namespace services;
public class AnimalsRepository : IAnimalService
{
    private DbContext _ctx;
    public AnimalsRepository(DbContext dbContext) => _ctx = dbContext;

    public async Task<Animal[]> GetAll()
    {
        return await _ctx.Set<Animal>()
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task<Animal> Add(string name)
    {
        var set = _ctx.Set<Animal>();
        _ctx.Add(new Animal() { Name = name });

        await _ctx.SaveChangesAsync();

        return set.Local.First(a => a.Name == name);
    }

    public async Task Delete(int id)
    {
        var set = _ctx.Set<Animal>();
        var animal = await set.FirstOrDefaultAsync(a => a.Id == id);

        if (animal == null)
        {
            throw new KeyNotFoundException();
        }

        set.Remove(animal);
        await _ctx.SaveChangesAsync();
    }
}

