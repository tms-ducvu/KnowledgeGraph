using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using KnowledgeGraph.ContactHistories;

namespace KnowledgeGraph.Web.Pages.ContactHistories;

public class IndexModel : PageModel
{
    private readonly IContactHistoryService _contactHistoryService;

    public IndexModel(IContactHistoryService contactHistoryService)
    {
        _contactHistoryService = contactHistoryService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostSyncAsync(Guid id)
    {
        try
        {
            var result = await _contactHistoryService.SyncAsync(id);

            if (result != null)
            {
                // Success - trả về JSON response
                return new JsonResult(new { success = true, message = "Contact history synced successfully", data = result });
            }
            else
            {
                // Handle error case - trả về JSON response
                return new JsonResult(new { success = false, message = "Failed to sync contact history." });
            }
        }
        catch (Exception)
        {
            // Handle exception - trả về JSON response
            return new JsonResult(new { success = false, message = "An unexpected error occurred while syncing the contact history." });
        }
    }
}
