using System;
using Volo.Abp.Domain.Entities.Auditing;

/// <summary>
/// Summary description for EntityHistory
/// </summary>
namespace KnowledgeGraph.Entities
{
    public class EntityHistory : FullAuditedAggregateRoot<Guid>
    {
        public Guid EntityId { get; set; }

        public string? EntityName { get; set; }

        public string? EntityCode { get; set; }

        public string? EntityBusinessType { get; set; }

        public string? EntityAddress { get; set; }

        public string? EntityPhone { get; set; }

        public string? EntityEmail { get; set; }

        public string? EntityWebsite { get; set; }

        public bool EntityIsActive { get; set; }
    }
}
