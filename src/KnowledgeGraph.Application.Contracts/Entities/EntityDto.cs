using System;
using Volo.Abp.Application.Dtos;

namespace KnowledgeGraph.Entities
{
    public class EntityDto : FullAuditedEntityDto<Guid>
    {
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
