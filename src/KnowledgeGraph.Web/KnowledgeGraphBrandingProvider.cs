using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using KnowledgeGraph.Localization;

namespace KnowledgeGraph.Web;

[Dependency(ReplaceServices = true)]
public class KnowledgeGraphBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<KnowledgeGraphResource> _localizer;

    public KnowledgeGraphBrandingProvider(IStringLocalizer<KnowledgeGraphResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
