using System;
using Volo.Abp.Application.Dtos;

namespace KnowledgeGraph.Reviews
{
    public class FilterReviewDto : PagedAndSortedResultRequestDto
    {
        public Guid? ReviewEntityId { get; set; }
        public string? ReviewerName { get; set; }
        public Guid? ReviewPlatformId { get; set; }
        public int? ReviewRating { get; set; }
        public DateTime? ReviewReviewDateFrom { get; set; }
        public DateTime? ReviewReviewDateTo { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
