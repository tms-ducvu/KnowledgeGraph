using KnowledgeGraph.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace KnowledgeGraph.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class KnowledgeGraphController : AbpControllerBase
{
    protected KnowledgeGraphController()
    {
        LocalizationResource = typeof(KnowledgeGraphResource);
    }
}
