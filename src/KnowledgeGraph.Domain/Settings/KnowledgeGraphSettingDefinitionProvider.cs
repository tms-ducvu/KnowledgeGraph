using Volo.Abp.Settings;

namespace KnowledgeGraph.Settings;

public class KnowledgeGraphSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(KnowledgeGraphSettings.MySetting1));
    }
}
