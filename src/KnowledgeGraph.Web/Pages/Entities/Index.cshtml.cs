using KnowledgeGraph.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KnowledgeGraph.Web.Pages.Entities;

public class IndexModel : KnowledgeGraphPageModel
{
    private readonly IEntityService _entityService;

    public IndexModel(IEntityService entityService)
    {
        _entityService = entityService;
    }
    public void OnGet()
    {
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        try
        {
            var result = await _entityService.DeleteAsync(id);

            if (result.success == true)
            {
                // Success - trả về JSON response
                return new JsonResult(new { success = true, message = "Entity deleted successfully" });
            }
            else
            {
                // Handle error case - trả về JSON response
                return new JsonResult(new { success = false, message = result.message ?? "An error occurred while deleting the entity." });
            }
        }
        catch (Exception)
        {
            // Handle exception - trả về JSON response
            return new JsonResult(new { success = false, message = "An unexpected error occurred while deleting the entity." });
        }
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostToggleActiveAsync(Guid id)
    {
        try
        {
            var result = await _entityService.ToggleActiveAsync(id);

            if (result.success == true)
            {
                // Success - trả về JSON response
                return new JsonResult(new { success = true, message = result.message ?? "Entity status toggled successfully" });
            }
            else
            {
                // Handle error case - trả về JSON response
                return new JsonResult(new { success = false, message = result.message ?? "An error occurred while toggling entity status." });
            }
        }
        catch (Exception)
        {
            // Handle exception - trả về JSON response
            return new JsonResult(new { success = false, message = "An unexpected error occurred while toggling entity status." });
        }
    }
}
