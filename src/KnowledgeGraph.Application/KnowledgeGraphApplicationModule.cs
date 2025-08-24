using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using KnowledgeGraph.ContactHistories;

namespace KnowledgeGraph;

[DependsOn(
    typeof(KnowledgeGraphDomainModule),
    typeof(KnowledgeGraphApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class KnowledgeGraphApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<KnowledgeGraphApplicationModule>();
        });
        
        // Explicit registration for ContactService
        context.Services.AddTransient<KnowledgeGraph.Contacts.IContactService, KnowledgeGraph.Contacts.ContactAppService>();
        
        // Explicit registration for ContactHistoryService
        context.Services.AddTransient<KnowledgeGraph.ContactHistories.IContactHistoryService, KnowledgeGraph.ContactHistories.ContactHistoryAppService>();
    }
}
