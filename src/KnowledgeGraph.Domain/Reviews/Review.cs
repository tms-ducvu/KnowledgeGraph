using System;
using Volo.Abp.Domain.Entities.Auditing;

/// <summary>
/// Summary description for Review
/// </summary>
namespace KnowledgeGraph.Reviews
{
    public class Review : FullAuditedAggregateRoot<Guid>
    {
        public Guid ReviewEntityId { get; set; }

        public string? ReviewerName { get; set; }

        public Guid? ReviewPlatformId { get; set; }

        public DateTime ReviewReviewDate { get; set; }

        public int ReviewRating { get; set; }

        public string? ReviewContent { get; set; }

        public DateTime? ReviewSyncTime { get; set; }
    }
}
