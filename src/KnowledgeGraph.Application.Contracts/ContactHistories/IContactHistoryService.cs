using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KnowledgeGraph.ContactHistories
{
    public interface IContactHistoryService : IApplicationService
    {
        /// <summary>
        /// Get list contact history
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<ContactHistoryDto>> GetListAsync(FilterContactHistoryDto input);
    }
}
