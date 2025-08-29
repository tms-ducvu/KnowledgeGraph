using System;
using Volo.Abp.Domain.Repositories;

namespace KnowledgeGraph.Reviews
{
    public interface IReviewRepository : IRepository<Review, Guid>
    {
    }
}
