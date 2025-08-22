using System.Threading.Tasks;
using KnowledgeGraph.Localization;
using KnowledgeGraph.Permissions;
using KnowledgeGraph.MultiTenancy;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.UI.Navigation;



namespace KnowledgeGraph.Web.Menus;

public class KnowledgeGraphMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<KnowledgeGraphResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                KnowledgeGraphMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //Contacts Group
        context.Menu.AddItem(
            new ApplicationMenuItem(
                "KnowledgeGraph.Contacts",
                l["Menu:Contacts"],
                icon: "fa fa-address-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "KnowledgeGraph.Contacts.Contacts",
                    l["Menu:Contacts"],
                    url: "/Contacts"
                )
            ).AddItem(
                new ApplicationMenuItem(
                    "KnowledgeGraph.Contacts.ContactHistories",
                    l["Menu:ContactHistories"],
                    url: "/ContactHistories"
                )
            )
        );

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);
        
        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 2);
        
        return Task.CompletedTask;
    }
}
