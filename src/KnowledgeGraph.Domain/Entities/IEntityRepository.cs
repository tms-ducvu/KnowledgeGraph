using System;
using Volo.Abp.Domain.Repositories;

namespace KnowledgeGraph.Entities
{
    public interface IEntityRepository : IRepository<Entity, Guid>
    {
    }
}
