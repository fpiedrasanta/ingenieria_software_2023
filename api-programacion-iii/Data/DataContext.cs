using Microsoft.EntityFrameworkCore;
using api_programacion_iii.Entities.Resources;
using api_programacion_iii.Entities.Common;
using System.Diagnostics.CodeAnalysis;

namespace api_programacion_iii.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    [NotNull]
    public DbSet<Resource>? Resources { get; set; }

    [NotNull]
    public DbSet<ResourceType>? ResourceTypes { get; set; }

    [NotNull]
    public DbSet<Image>? Images { get; set; }
}