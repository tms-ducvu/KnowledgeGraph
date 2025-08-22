using System;
using System.Threading.Tasks;
using KnowledgeGraph.Contacts;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KnowledgeGraph.Web.Pages.Contacts;

public class EditModalModel : KnowledgeGraphPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public UpdateContactDto Contact { get; set; }

    private readonly IContactService _contactService;

    public EditModalModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    public async Task OnGetAsync()
    {
        var contactDto = await _contactService.GetAsync(Id);
        Contact = ObjectMapper.Map<ContactDto, UpdateContactDto>(contactDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _contactService.UpdateAsync(Id, Contact);
        return NoContent();
    }
}
