using KnowledgeGraph.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
namespace KnowledgeGraph.Web.Pages.Contacts;

public class IndexModel : PageModel
{
    private readonly IContactService _contactService;

    public IndexModel(IContactService contactService)
    {
        _contactService = contactService;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        try
        {
            var result = await _contactService.DeleteAsync(id);

            if (result.success == true)
            {
                // Success - trả về JSON response
                return new JsonResult(new { success = true, message = "Contact deleted successfully" });
            }
            else
            {
                // Handle error case - trả về JSON response
                return new JsonResult(new { success = false, message = result.message ?? "An error occurred while deleting the contact." });
            }
        }
        catch (Exception)
        {
            // Handle exception - trả về JSON response
            return new JsonResult(new { success = false, message = "An unexpected error occurred while deleting the contact." });
        }
    }
}
