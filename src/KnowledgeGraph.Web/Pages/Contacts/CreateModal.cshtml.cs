using System.Threading.Tasks;
using KnowledgeGraph.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeGraph.Web.Pages.Contacts;

public class CreateModalModel : KnowledgeGraphPageModel
{
    [BindProperty]
    public CreateContactDto Contact { get; set; } = new CreateContactDto();

    private readonly IContactService _contactService;

    public CreateModalModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void OnGet()
    {
        Contact = new CreateContactDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _contactService.CreateAsync(Contact);
        if (result.success == true)
        {
            return NoContent();
        }
        else
        {
            // Handle error case
            ModelState.AddModelError("", result.message ?? "An error occurred while creating the contact.");
            return Page();
        }
    }
}
