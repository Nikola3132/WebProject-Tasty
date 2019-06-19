namespace Tasty.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using Tasty.Data;

    public interface ISeeder
    {
        Task SeedAsync(TastyDbContext dbContext, IServiceProvider serviceProvider);
    }
}
