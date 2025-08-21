using System;
using Volo.Abp.Application.Dtos;

namespace KnowledgeGraph.ContactHistories
{
    public class ContactHistoryDto : CreationAuditedEntityDto<Guid>
    {
        public Guid ContactId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string? AddressStreet { get; set; }

        public string? AddressCity { get; set; }

        public string? AddressCountry { get; set; }

        public string? AddressZipCode { get; set; }

        public string? SyncStatus { get; set; }
        
        public string? SyncEror { get; set; }

        public DateTime? LastSyncTime{ get; set; }
    }
}
