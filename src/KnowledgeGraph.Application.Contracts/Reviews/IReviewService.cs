using KnowledgeGraph.Common;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace KnowledgeGraph.Reviews
{
    public interface IReviewService : IApplicationService
    {
        /// <summary>
        /// Get review info by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ReviewDto> GetAsync(Guid id);

        /// <summary>
        /// Get list review
        /// </summary>
        /// <returns></returns>
        public Task<PagedResultDto<ReviewDto>> GetListAsync(FilterReviewDto input);

        /// <summary>
        /// Create review info by Guid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<BaseResponseDto<ReviewDto>> CreateAsync(CreateReviewDto input);

        /// <summary>
        /// Update review info by Guid
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BaseResponseDto<ReviewDto>> UpdateAsync(Guid id, UpdateReviewDto input);

        /// <summary>
        /// Soft Delete review info by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BaseResponseModel> DeleteAsync(Guid id);
    }
}
