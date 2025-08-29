using System;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Reviews
{
    public class UpdateReviewDto
    {
        [Required]
        public Guid ReviewEntityId { get; set; }

        [Required]
        [MaxLength(255)]
        public string? ReviewerName { get; set; }

        public Guid? ReviewPlatformId { get; set; }

        [Required]
        public DateTime ReviewReviewDate { get; set; }

        [Required]
        [Range(1, 5)]
        public int ReviewRating { get; set; }

        [MaxLength(4000)]
        public string? ReviewContent { get; set; }

        public DateTime? ReviewSyncTime { get; set; }
    }
}
