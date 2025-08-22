using System;
using System.Threading.Tasks;
using KnowledgeGraph.ContactHistories;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KnowledgeGraph.Web.Pages.ContactHistories;

public class ViewModalModel : KnowledgeGraphPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ContactHistoryDto ContactHistory { get; set; }

    private readonly IContactHistoryService _contactHistoryService;

    public ViewModalModel(IContactHistoryService contactHistoryService)
    {
        _contactHistoryService = contactHistoryService;
        ContactHistory = new ContactHistoryDto(); // Khởi tạo default
    }

    public async Task OnGetAsync()
    {
        try
        {
            var contactHistoryDto = await _contactHistoryService.GetAsync(Id);
            ContactHistory = contactHistoryDto ?? new ContactHistoryDto();
        }
        catch (Exception)
        {
            // Log error và khởi tạo default
            ContactHistory = new ContactHistoryDto();
        }
    }
}
