using KnowledgeGraph.Reviews;
using KnowledgeGraph.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KnowledgeGraph.Repositories
{
    public class ReviewRepository : EfCoreRepository<KnowledgeGraphDbContext, Review, Guid>, IReviewRepository
    {
        public ReviewRepository(IDbContextProvider<KnowledgeGraphDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
