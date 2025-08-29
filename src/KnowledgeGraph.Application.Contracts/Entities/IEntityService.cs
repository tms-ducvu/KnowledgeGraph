using KnowledgeGraph.Common;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KnowledgeGraph.Entities
{
    public interface IEntityService : IApplicationService
    {
        /// <summary>
        /// Get entity info by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<EntityDto> GetAsync(Guid id);

        /// <summary>
        /// Get list entity
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<EntityDto>> GetListAsync(FilterEntityDto input);

        /// <summary>
        /// Create entity info by Guid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<BaseResponseDto<EntityDto>> CreateAsync(CreateEntityDto input);

        /// <summary>
        /// Update entity info by Guid
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BaseResponseDto<EntityDto>> UpdateAsync(Guid id, UpdateEntityDto input);

        /// <summary>
        /// Soft Delete entity info by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BaseResponseModel> DeleteAsync(Guid id);

        /// <summary>
        /// Toggle entity active status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BaseResponseModel> ToggleActiveAsync(Guid id);
    }
}
