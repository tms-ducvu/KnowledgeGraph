using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace KnowledgeGraph.Data;

/* This is used if database provider does't define
 * IKnowledgeGraphDbSchemaMigrator implementation.
 */
public class NullKnowledgeGraphDbSchemaMigrator : IKnowledgeGraphDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
