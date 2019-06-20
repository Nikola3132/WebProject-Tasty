namespace Tasty.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Tasty.Data.Common;

    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(TastyDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TastyDbContext Context { get; set; }

        public Task RunQueryAsync(string query, params object[] parameters)
        {
            return this.Context.Database.ExecuteSqlCommandAsync(query, parameters);
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
