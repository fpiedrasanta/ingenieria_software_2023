using Microsoft.EntityFrameworkCore;
using api_programacion_iii.Entities.Resources;
using api_programacion_iii.Entities.Common;

namespace api_programacion_iii.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Resource>? Resources { get; set; }

    public DbSet<ResourceType>? ResourceTypes { get; set; }

    public DbSet<Image>? Images { get; set; }
}