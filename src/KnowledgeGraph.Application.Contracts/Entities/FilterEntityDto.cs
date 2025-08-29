using Volo.Abp.Application.Dtos;

namespace KnowledgeGraph.Entities
{
    public class FilterEntityDto : PagedAndSortedResultRequestDto
    {
        public string? EntityName { get; set; }
        public string? EntityCode { get; set; }
        public string? EntityBusinessType { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
