using KnowledgeGraph.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace KnowledgeGraph.Permissions;

public class KnowledgeGraphPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(KnowledgeGraphPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(KnowledgeGraphPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<KnowledgeGraphResource>(name);
    }
}
