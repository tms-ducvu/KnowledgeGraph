using KnowledgeGraph.Reviews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
namespace KnowledgeGraph.Web.Pages.Reviews;

public class IndexModel : PageModel
{
    private readonly IReviewService _reviewService;

    public IndexModel(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        try
        {
            var result = await _reviewService.DeleteAsync(id);

            if (result.success == true)
            {
                // Success - trả về JSON response
                return new JsonResult(new { success = true, message = "Review deleted successfully" });
            }
            else
            {
                // Handle error case - trả về JSON response
                return new JsonResult(new { success = false, message = result.message ?? "An error occurred while deleting the review." });
            }
        }
        catch (Exception)
        {
            // Handle exception - trả về JSON response
            return new JsonResult(new { success = false, message = "An unexpected error occurred while deleting the review." });
        }
    }
}
