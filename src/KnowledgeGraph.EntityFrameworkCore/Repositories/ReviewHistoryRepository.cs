using KnowledgeGraph.Reviews;
using KnowledgeGraph.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KnowledgeGraph.Repositories
{
    public class ReviewHistoryRepository : EfCoreRepository<KnowledgeGraphDbContext, ReviewHistory, Guid>, IReviewHistoryRepository
    {
        public ReviewHistoryRepository(IDbContextProvider<KnowledgeGraphDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
