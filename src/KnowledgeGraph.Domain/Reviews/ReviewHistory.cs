using System;
using Volo.Abp.Domain.Entities.Auditing;

/// <summary>
/// Summary description for ReviewHistory
/// </summary>
namespace KnowledgeGraph.Reviews
{
    public class ReviewHistory : FullAuditedAggregateRoot<Guid>
    {
        public Guid ReviewId { get; set; }

        public Guid ReviewEntityId { get; set; }

        public string? ReviewerName { get; set; }

        public Guid? ReviewPlatformId { get; set; }

        public DateTime ReviewReviewDate { get; set; }

        public int ReviewRating { get; set; }

        public string? ReviewContent { get; set; }

        public DateTime? ReviewSyncTime { get; set; }

        public string? SyncStatus { get; set; }

        public string? SyncLog { get; set; }
    }
}
