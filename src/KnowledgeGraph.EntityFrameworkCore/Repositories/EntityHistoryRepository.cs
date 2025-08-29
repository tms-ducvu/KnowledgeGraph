using KnowledgeGraph.Entities;
using KnowledgeGraph.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KnowledgeGraph.Repositories
{
    public class EntityHistoryRepository : EfCoreRepository<KnowledgeGraphDbContext, EntityHistory, Guid>, IEntityHistoryRepository
    {
        public EntityHistoryRepository(IDbContextProvider<KnowledgeGraphDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
