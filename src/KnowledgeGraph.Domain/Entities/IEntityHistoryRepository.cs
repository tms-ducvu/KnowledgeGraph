using System;
using Volo.Abp.Domain.Repositories;

namespace KnowledgeGraph.Entities
{
    public interface IEntityHistoryRepository : IRepository<EntityHistory, Guid>
    {
    }
}
