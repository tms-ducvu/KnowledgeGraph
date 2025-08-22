using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KnowledgeGraph.ContactHistories
{
    public interface IContactHistoryService : IApplicationService
    {
        /// <summary>
        /// Get contact history info by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ContactHistoryDto> GetAsync(Guid id);

        /// <summary>
        /// Sync contact history by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ContactHistoryDto> SyncAsync(Guid id);


        /// <summary>
        /// Get list contact history
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<ContactHistoryDto>> GetListAsync(FilterContactHistoryDto input);

        
    }
}
