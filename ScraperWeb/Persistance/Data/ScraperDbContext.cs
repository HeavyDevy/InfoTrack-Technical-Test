using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance.EFMaps;
using ScraperWeb.Domain.Entities;

namespace ScraperWeb.Persistance.Data
{
    public class ScraperDbContext : DbContext
    {
        public ScraperDbContext(DbContextOptions<ScraperDbContext> options)
        : base(options) { }

        public DbSet<Search> Searches => Set<Search>();

        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync();
        }
    }
}