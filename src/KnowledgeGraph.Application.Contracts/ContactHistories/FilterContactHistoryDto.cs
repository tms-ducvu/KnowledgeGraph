using System;
using Volo.Abp.Application.Dtos;

namespace KnowledgeGraph.ContactHistories
{
    public class FilterContactHistoryDto : PagedAndSortedResultRequestDto
    {

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }
    }
}
