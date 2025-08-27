using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace KnowledgeGraph.ContactHistories
{
    public class ContactHistoryAppService : ApplicationService, IContactHistoryService
    {
        private readonly IContactHistoryRepository _contactHistoryRepository;


        public ContactHistoryAppService(IContactHistoryRepository contactHistoryRepository)
        {
            _contactHistoryRepository = contactHistoryRepository;
        }

        public async Task<PagedResultDto<ContactHistoryDto>> GetListAsync(FilterContactHistoryDto input)
        {
            var contactHistoryQuery = await _contactHistoryRepository.GetQueryableAsync();

            contactHistoryQuery = contactHistoryQuery
                .WhereIf(!string.IsNullOrEmpty(input.Email), x => !string.IsNullOrEmpty(x.Email) && x.Email.ToLower().Contains(input.Email!.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(input.PhoneNumber), x => !string.IsNullOrEmpty(x.PhoneNumber) && x.PhoneNumber.ToLower().Contains(input.PhoneNumber!.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(input.Name), x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(input.Name!.ToLower()))
                .OrderBy(input.Sorting ?? "CreationTime DESC");

            var totalCount = await AsyncExecuter.CountAsync(contactHistoryQuery);
            var list = await AsyncExecuter.ToListAsync(contactHistoryQuery.PageBy(input.SkipCount, input.MaxResultCount));
            var contacHistorytlist = ObjectMapper.Map<List<ContactHistory>, List<ContactHistoryDto>>(list);

            return new PagedResultDto<ContactHistoryDto>(totalCount, contacHistorytlist);
        }

        public async Task<ContactHistoryDto> GetAsync(Guid id)
        {
            var result = new ContactHistoryDto();

            var contactHistory = await _contactHistoryRepository.FirstOrDefaultAsync(d => d.Id == id);
            if (contactHistory != null)
            {
                result = ObjectMapper.Map<ContactHistory, ContactHistoryDto>(contactHistory);
            }

            return result;
        }

        public async Task<ContactHistoryDto> SyncAsync(Guid id)
        {
            var result = new ContactHistoryDto();

            try
            {
                // TODO: Implement sync logic here
                // For now, just return a success message
                result.SyncStatus = "Synced";
                result.LastSyncTime = DateTime.Now;
                result.SyncLog = null;
                
                // Get the actual contact history to return complete data
                var contactHistory = await _contactHistoryRepository.FirstOrDefaultAsync(d => d.Id == id);
                if (contactHistory != null)
                {
                    result = ObjectMapper.Map<ContactHistory, ContactHistoryDto>(contactHistory);
                    result.SyncStatus = "Synced";
                    result.LastSyncTime = DateTime.Now;
                    result.SyncLog = null;
                }
            }
            catch (Exception)
            {
                result.SyncStatus = "Failed";
                result.SyncLog = "Sync operation failed";
            }

            return result;
        }


    }
}
