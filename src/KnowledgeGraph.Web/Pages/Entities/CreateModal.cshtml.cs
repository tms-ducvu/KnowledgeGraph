using System.Threading.Tasks;
using KnowledgeGraph.Entities;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KnowledgeGraph.Web.Pages.Entities;

public class CreateModalModel : KnowledgeGraphPageModel
{
    [BindProperty]
    public CreateEntityDto Entity { get; set; } = new CreateEntityDto();

    private readonly IEntityService _entityService;

    public CreateModalModel(IEntityService entityService)
    {
        _entityService = entityService;
    }

    public void OnGet()
    {
        Entity = new CreateEntityDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _entityService.CreateAsync(Entity);
        if (result.success == true)
        {
            return NoContent();
        }
        else
        {
            // Handle error case
            ModelState.AddModelError("", result.message ?? "An error occurred while creating the entity.");
            return Page();
        }
    }
}
