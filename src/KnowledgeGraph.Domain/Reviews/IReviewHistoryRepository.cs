using System;
using Volo.Abp.Domain.Repositories;

namespace KnowledgeGraph.Reviews
{
    public interface IReviewHistoryRepository : IRepository<ReviewHistory, Guid>
    {
    }
}
