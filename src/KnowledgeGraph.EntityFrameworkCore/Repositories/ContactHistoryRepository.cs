using KnowledgeGraph.ContactHistories;
using KnowledgeGraph.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KnowledgeGraph.Repositories
{
    public class ContactHistoryRepository : EfCoreRepository<KnowledgeGraphDbContext, ContactHistory, Guid>, IContactHistoryRepository
    {
        public ContactHistoryRepository(IDbContextProvider<KnowledgeGraphDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
