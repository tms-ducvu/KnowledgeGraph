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
using Volo.Abp.Domain.Entities;
using KnowledgeGraph.Common;
using KnowledgeGraph.Entities;

namespace KnowledgeGraph.Entities
{
    public class EntityAppService: ApplicationService, IEntityService
    {
        private readonly IEntityRepository _entityRepository;

        private readonly IDataFilter _dataFilter;

        public EntityAppService(IEntityRepository entityRepository, IDataFilter dataFilter)
        {
            _entityRepository = entityRepository;
            _dataFilter = dataFilter;
        }

        public async Task<BaseResponseDto<EntityDto>> CreateAsync(CreateEntityDto input)
        {
            var result = new BaseResponseDto<EntityDto>();

            try
            {
                // Check if EntityCode already exists
                var existingEntity = await _entityRepository.FirstOrDefaultAsync(e => e.EntityCode == input.EntityCode);
                if (existingEntity != null)
                {
                    result.message = "EntityCode already exists!";
                    result.success = false;
                    return result;
                }

                var entity = new Entity()
                {
                    EntityName = input.EntityName,
                    EntityCode = input.EntityCode,
                    EntityBusinessType = input.EntityBusinessType,
                    EntityAddress = input.EntityAddress,
                    EntityPhone = input.EntityPhone,
                    EntityEmail = input.EntityEmail,
                    EntityWebsite = input.EntityWebsite,
                    EntityIsActive = input.EntityIsActive,
                };

                var res = await _entityRepository.InsertAsync(entity);

                result.data = ObjectMapper.Map<Entity, EntityDto>(entity);

                return result;
            }
            catch
            {
                result.message = "Fail to add entity";
                result.success = false;

                return result;
            }
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var result = new BaseResponseModel();

            try
            {
                var entity = await _entityRepository.FirstOrDefaultAsync(d => d.Id.Equals(id));

                if (entity == null)
                {
                    result.message = "This Entity not exist!";
                    result.success = false;
                    return result;
                }

                entity.DeletionTime = DateTime.Now;

                await _entityRepository.DeleteAsync(id);

                return result;
            }
            catch (Exception)
            {
                result.message = "Deletion Entity failed!";
                result.success = false;

                return result;
            }
        }

        public async Task<PagedResultDto<EntityDto>> GetListAsync(FilterEntityDto input)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var entityQuery = await _entityRepository.GetQueryableAsync();

                entityQuery = entityQuery
                    .WhereIf(!string.IsNullOrEmpty(input.EntityName), x => !string.IsNullOrEmpty(x.EntityName) && x.EntityName.ToLower().Contains(input.EntityName!.ToLower()))
                    .WhereIf(!string.IsNullOrEmpty(input.EntityCode), x => !string.IsNullOrEmpty(x.EntityCode) && x.EntityCode.ToLower().Contains(input.EntityCode!.ToLower()))
                    .WhereIf(!string.IsNullOrEmpty(input.EntityBusinessType), x => !string.IsNullOrEmpty(x.EntityBusinessType) && x.EntityBusinessType.ToLower().Contains(input.EntityBusinessType!.ToLower()))
                    .Where(x => x.IsDeleted == false) // Chỉ lấy entity chưa bị xóa
                    .OrderBy(input.Sorting ?? "CreationTime DESC");

                var totalCount = await AsyncExecuter.CountAsync(entityQuery);
                var list = await AsyncExecuter.ToListAsync(entityQuery.PageBy(input.SkipCount, input.MaxResultCount));
                var entitylist = ObjectMapper.Map<List<Entity>, List<EntityDto>>(list);

                return new PagedResultDto<EntityDto>(totalCount, entitylist);
            }
        }

        public async Task<EntityDto> GetAsync(Guid id)
        {
            var result = new EntityDto();

            var entity = await _entityRepository.FirstOrDefaultAsync(d => d.Id == id);
            if (entity != null)
            {
                result = ObjectMapper.Map<Entity, EntityDto>(entity);
            }

            return result;
        }

        public async Task<BaseResponseDto<EntityDto>> UpdateAsync(Guid id, UpdateEntityDto input)
        {
            var result = new BaseResponseDto<EntityDto>();

            try
            {
                var entity = await _entityRepository.FirstOrDefaultAsync(d => d.Id.Equals(id));

                if (entity == null)
                {
                    result.message = "Entity not found!";
                    result.success = false;
                    return result;
                }

                // Check if EntityCode already exists (excluding current entity)
                if (input.EntityCode != entity.EntityCode)
                {
                    var existingEntity = await _entityRepository.FirstOrDefaultAsync(e => e.EntityCode == input.EntityCode);
                    if (existingEntity != null)
                    {
                        result.message = "EntityCode already exists!";
                        result.success = false;
                        return result;
                    }
                }

                entity.EntityName = input.EntityName;
                entity.EntityCode = input.EntityCode;
                entity.EntityBusinessType = input.EntityBusinessType;
                entity.EntityAddress = input.EntityAddress;
                entity.EntityPhone = input.EntityPhone;
                entity.EntityEmail = input.EntityEmail;
                entity.EntityWebsite = input.EntityWebsite;
                entity.EntityIsActive = input.EntityIsActive;

                var res = await _entityRepository.UpdateAsync(entity);

                result.data = ObjectMapper.Map<Entity, EntityDto>(res);

                return result;
            }
            catch
            {
                result.message = "Fail to update entity";
                result.success = false;

                return result;
            }
        }

        public async Task<BaseResponseModel> ToggleActiveAsync(Guid id)
        {
            var result = new BaseResponseModel();

            try
            {
                var entity = await _entityRepository.FirstOrDefaultAsync(d => d.Id.Equals(id));

                if (entity == null)
                {
                    result.message = "Entity not found!";
                    result.success = false;
                    return result;
                }

                entity.EntityIsActive = !entity.EntityIsActive;

                await _entityRepository.UpdateAsync(entity);

                result.message = $"Entity {(entity.EntityIsActive ? "activated" : "deactivated")} successfully!";
                return result;
            }
            catch (Exception)
            {
                result.message = "Toggle Entity active status failed!";
                result.success = false;

                return result;
            }
        }
    }
}
