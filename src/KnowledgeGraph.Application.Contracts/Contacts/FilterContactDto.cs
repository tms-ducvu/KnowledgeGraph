using Volo.Abp.Application.Dtos;

namespace KnowledgeGraph.Contacts
{
    public class FilterContactDto : PagedAndSortedResultRequestDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
