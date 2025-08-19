using System.Threading.Tasks;

namespace KnowledgeGraph.Data;

public interface IKnowledgeGraphDbSchemaMigrator
{
    Task MigrateAsync();
}
