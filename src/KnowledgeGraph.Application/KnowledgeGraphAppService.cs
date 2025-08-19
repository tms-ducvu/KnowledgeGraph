using KnowledgeGraph.Localization;
using Volo.Abp.Application.Services;

namespace KnowledgeGraph;

/* Inherit your application services from this class.
 */
public abstract class KnowledgeGraphAppService : ApplicationService
{
    protected KnowledgeGraphAppService()
    {
        LocalizationResource = typeof(KnowledgeGraphResource);
    }
}
