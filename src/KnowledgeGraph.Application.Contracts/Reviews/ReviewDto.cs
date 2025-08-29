using System;
using Volo.Abp.Application.Dtos;

namespace KnowledgeGraph.Reviews
{
    public class ReviewDto : FullAuditedEntityDto<Guid>
    {
        public Guid ReviewEntityId { get; set; }
        public string? EntityName { get; set; }

        public string? ReviewerName { get; set; }

        public Guid? ReviewPlatformId { get; set; }

        public DateTime ReviewReviewDate { get; set; }

        public int ReviewRating { get; set; }

        public string? ReviewContent { get; set; }

        public DateTime? ReviewSyncTime { get; set; }
    }
}
