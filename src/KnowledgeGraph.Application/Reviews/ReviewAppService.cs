using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using KnowledgeGraph.Common;
using KnowledgeGraph.Reviews;
using KnowledgeGraph.Entities;

namespace KnowledgeGraph.Reviews
{
    public class ReviewAppService: ApplicationService, IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        private readonly IDataFilter _dataFilter;
        
        private readonly IEntityService _entityService;

        public ReviewAppService(IReviewRepository reviewRepository, IDataFilter dataFilter, IEntityService entityService)
        {
            _reviewRepository = reviewRepository;
            _dataFilter = dataFilter;
            _entityService = entityService;
        }

        public async Task<BaseResponseDto<ReviewDto>> CreateAsync(CreateReviewDto input)
        {
            var result = new BaseResponseDto<ReviewDto>();

            try
            {
                var entity = new Review()
                {
                    ReviewEntityId = input.ReviewEntityId,
                    ReviewerName = input.ReviewerName,
                    ReviewPlatformId = input.ReviewPlatformId,
                    ReviewReviewDate = input.ReviewReviewDate,
                    ReviewRating = input.ReviewRating,
                    ReviewContent = input.ReviewContent,
                    ReviewSyncTime = input.ReviewSyncTime,
                };


                var res = await _reviewRepository.InsertAsync(entity);


                result.data = ObjectMapper.Map<Review, ReviewDto>(entity);

                return result;
            }
            catch
            {
                result.message = "Fail to add review";
                result.success = false;

                return result;
            }
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var result = new BaseResponseModel();

            try
            {
                var entity = await _reviewRepository.FirstOrDefaultAsync(d => d.Id.Equals(id));

                if (entity == null)
                {
                    result.message = "This Review not exist!";
                    result.success = false;
                    return result;
                }

                entity.DeletionTime = DateTime.Now;

                await _reviewRepository.DeleteAsync(id);

                return result;
            }
            catch (Exception)
            {
                result.message = "Deletion Review failed!";
                result.success = false;

                return result;
            }
        }

        public async Task<PagedResultDto<ReviewDto>> GetListAsync(FilterReviewDto input)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var reviewQuery = await _reviewRepository.GetQueryableAsync();

                reviewQuery = reviewQuery
                    .WhereIf(input.ReviewEntityId.HasValue, x => x.ReviewEntityId == input.ReviewEntityId.Value)
                    .WhereIf(!string.IsNullOrEmpty(input.ReviewerName), x => !string.IsNullOrEmpty(x.ReviewerName) && x.ReviewerName.ToLower().Contains(input.ReviewerName!.ToLower()))
                    .WhereIf(input.ReviewPlatformId.HasValue, x => x.ReviewPlatformId == input.ReviewPlatformId.Value)
                    .WhereIf(input.ReviewRating.HasValue, x => x.ReviewRating == input.ReviewRating.Value)
                    .WhereIf(input.ReviewReviewDateFrom.HasValue, x => x.ReviewReviewDate >= input.ReviewReviewDateFrom.Value)
                    .WhereIf(input.ReviewReviewDateTo.HasValue, x => x.ReviewReviewDate <= input.ReviewReviewDateTo.Value)
                    .Where(x => x.IsDeleted == false) // Chỉ lấy review chưa bị xóa
                    .OrderBy(input.Sorting ?? "CreationTime DESC");

                var totalCount = await AsyncExecuter.CountAsync(reviewQuery);
                var list = await AsyncExecuter.ToListAsync(reviewQuery.PageBy(input.SkipCount, input.MaxResultCount));
                
                // Map và populate EntityName
                var reviewlist = new List<ReviewDto>();
                foreach (var review in list)
                {
                    var reviewDto = ObjectMapper.Map<Review, ReviewDto>(review);
                    
                    // Lấy tên doanh nghiệp từ Entity
                    if (review.ReviewEntityId != Guid.Empty)
                    {
                        var entity = await _entityService.GetAsync(review.ReviewEntityId);
                        reviewDto.EntityName = entity?.EntityName;
                    }
                    
                    reviewlist.Add(reviewDto);
                }

                return new PagedResultDto<ReviewDto>(totalCount, reviewlist);
            }
        }

        public async Task<ReviewDto> GetAsync(Guid id)
        {
            var result = new ReviewDto();

            var review = await _reviewRepository.FirstOrDefaultAsync(d => d.Id == id);
            if (review != null)
            {
                result = ObjectMapper.Map<Review, ReviewDto>(review);
            }

            return result;
        }

        public async Task<BaseResponseDto<ReviewDto>> UpdateAsync(Guid id, UpdateReviewDto input)
        {
            var result = new BaseResponseDto<ReviewDto>();

            try
            {
                var review = await _reviewRepository.FirstOrDefaultAsync(d => d.Id.Equals(id));

                if (review == null)
                {
                    result.message = "Review not found!";
                    result.success = false;
                    return result;
                }
                

                review.ReviewEntityId = input.ReviewEntityId;
                review.ReviewerName = input.ReviewerName;
                review.ReviewPlatformId = input.ReviewPlatformId;
                review.ReviewReviewDate = input.ReviewReviewDate;
                review.ReviewRating = input.ReviewRating;
                review.ReviewContent = input.ReviewContent;
                review.ReviewSyncTime = input.ReviewSyncTime;

                var res = await _reviewRepository.UpdateAsync(review);

                result.data = ObjectMapper.Map<Review, ReviewDto>(res);

                return result;
            }
            catch
            {
                result.message = "Fail to update review";
                result.success = false;

                return result;
            }
        }
    }
}
