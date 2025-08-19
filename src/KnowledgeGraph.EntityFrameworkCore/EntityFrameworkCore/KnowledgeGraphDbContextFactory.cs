using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KnowledgeGraph.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class KnowledgeGraphDbContextFactory : IDesignTimeDbContextFactory<KnowledgeGraphDbContext>
{
    public KnowledgeGraphDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        KnowledgeGraphEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<KnowledgeGraphDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new KnowledgeGraphDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../KnowledgeGraph.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
