using System;
using Volo.Abp.Domain.Repositories;

namespace KnowledgeGraph.Contacts
{
    public interface IContactRepository : IRepository<Contact, Guid>
    {
    }
}
