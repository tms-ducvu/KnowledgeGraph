using KnowledgeGraph.Common;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KnowledgeGraph.Contacts
{
    public interface IContactService : IApplicationService
    {
        /// <summary>
        /// Get contact info by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ContactDto> GetAsync(Guid id);

        /// <summary>
        /// Get list contact
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<ContactDto>> GetListAsync(FilterContactDto input);

        /// <summary>
        /// Create contact info by Guid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<BaseResponseDto<ContactDto>> CreateAsync(CreateContactDto input);

        /// <summary>
        /// Update contact info by Guid
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BaseResponseDto<ContactDto>> UpdateAsync(Guid id, UpdateContactDto input);

        /// <summary>
        /// Soft Delete contact info by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BaseResponseModel> DeleteAsync(Guid id);
    }
}
