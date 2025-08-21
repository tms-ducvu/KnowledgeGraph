using KnowledgeGraph.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

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
    }
}
