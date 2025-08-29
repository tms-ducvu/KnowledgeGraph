using KnowledgeGraph.Entities;
using KnowledgeGraph.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KnowledgeGraph.Repositories
{
    public class EntityRepository : EfCoreRepository<KnowledgeGraphDbContext, Entity, Guid>, IEntityRepository
    {
        public EntityRepository(IDbContextProvider<KnowledgeGraphDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
