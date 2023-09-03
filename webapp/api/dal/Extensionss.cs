using domain;

namespace dal;
public static class DbExtensions
{

    public static WebApplication SeedData(this WebApplication host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetService<EfCtx>();

            if (context == null)
            {
                throw new Exception("Db is not initiilized");
            }

            if (IsNeedSeed(context))
            {
                Seed(context);
            }
        }
        return host;
    }

    private static bool IsNeedSeed(EfCtx ctx)
    {
        return !ctx.Set<Animal>().Any();
    }

    private static void Seed(EfCtx ctx)
    {
        SeedAnimals(ctx);
        ctx.SaveChanges();
    }

    private static void SeedAnimals(EfCtx ctx)
    {
        Animal[] animals = {
            new Animal() { Name = "Dog" },
            new Animal() { Name = "Cat" },
            new Animal() { Name = "Rat" }
        };

        ctx.AddRange(animals);
    }
}
