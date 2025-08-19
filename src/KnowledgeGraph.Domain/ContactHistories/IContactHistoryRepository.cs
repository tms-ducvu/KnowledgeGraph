using System;
using Volo.Abp.Domain.Repositories;

namespace KnowledgeGraph.ContactHistories
{
    public interface IContactHistoryRepository : IRepository<ContactHistory, Guid>
    {
    }
}
