using System;
using System.Threading.Tasks;
using KnowledgeGraph.Entities;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KnowledgeGraph.Web.Pages.Entities;

public class EditModalModel : KnowledgeGraphPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public UpdateEntityDto Entity { get; set; }

    private readonly IEntityService _entityService;

    public EditModalModel(IEntityService entityService)
    {
        _entityService = entityService;
    }

    public async Task OnGetAsync()
    {
        var entityDto = await _entityService.GetAsync(Id);
        Entity = ObjectMapper.Map<EntityDto, UpdateEntityDto>(entityDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _entityService.UpdateAsync(Id, Entity);
        return NoContent();
    }
}
