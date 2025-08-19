using System;
using Volo.Abp.Domain.Entities.Auditing;

/// <summary>
/// Summary description for Contact
/// </summary>
namespace KnowledgeGraph.Contacts
{
    public class Contact : FullAuditedAggregateRoot<Guid>
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string? AddressStreet { get; set; }

        public string? AddressCity { get; set; }

        public string? AddressCountry { get; set; }

        public string? AddressZipCode { get; set; }
    }
}
