using KnowledgeGraph.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace KnowledgeGraph.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(KnowledgeGraphEntityFrameworkCoreModule),
    typeof(KnowledgeGraphApplicationContractsModule)
)]
public class KnowledgeGraphDbMigratorModule : AbpModule
{
}
