using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Entities
{
    public class CreateEntityDto
    {
        [MaxLength(255)]
        public string? EntityName { get; set; }

        [MaxLength(50)]
        public string? EntityCode { get; set; }

        [MaxLength(100)]
        public string? EntityBusinessType { get; set; }

        public string? EntityAddress { get; set; }

        [MaxLength(20)]
        public string? EntityPhone { get; set; }

        [EmailAddress]
        public string? EntityEmail { get; set; }

        [Url]
        public string? EntityWebsite { get; set; }

        public bool EntityIsActive { get; set; } = false;
    }
}
