using System.ComponentModel.DataAnnotations;

namespace KnowledgeGraph.Contacts
{
    public class CreateContactDto
    {
        [MaxLength(255)]
        public string? Name { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string? Email { get; set; }

        [MaxLength(255)]
        public string? PhoneCode { get; set; }

        [MaxLength(255)]
        public string? PhoneNumber { get; set; }

        [MaxLength(255)]
        public string? AddressStreet { get; set; }

        [MaxLength(255)]
        public string? AddressCity { get; set; }

        [MaxLength(255)]
        public string? AddressCountry { get; set; }

        [MaxLength(255)]
        public string? AddressZipCode { get; set; }
    }
}
