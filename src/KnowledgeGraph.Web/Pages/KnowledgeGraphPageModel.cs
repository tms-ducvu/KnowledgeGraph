using KnowledgeGraph.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KnowledgeGraph.Web.Pages;

public abstract class KnowledgeGraphPageModel : AbpPageModel
{
    protected KnowledgeGraphPageModel()
    {
        LocalizationResourceType = typeof(KnowledgeGraphResource);
    }
}
