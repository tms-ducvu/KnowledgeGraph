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
using KnowledgeGraph.ContactHistories;

namespace KnowledgeGraph.Contacts
{
    public class ContactAppService: ApplicationService, IContactService
    {
        private readonly IContactRepository _contactRepository;

        private readonly IContactHistoryRepository _contactHistoryRepository;

        private readonly IDataFilter _dataFilter;

        public ContactAppService(IContactRepository contactRepository, IContactHistoryRepository contactHistoryRepository, IDataFilter dataFilter)
        {
            _contactRepository = contactRepository;
            _contactHistoryRepository = contactHistoryRepository;
            _dataFilter = dataFilter;
        }

        public async Task<BaseResponseDto<ContactDto>> CreateAsync(CreateContactDto input)
        {
            var result = new BaseResponseDto<ContactDto>();

            try
            {
                var entity = new Contact()
                {
                    Name = input.Name,
                    Email = input.Email,
                    PhoneCode = input.PhoneCode,
                    PhoneNumber = input.PhoneNumber,
                    AddressStreet = input.AddressStreet,
                    AddressCity = input.AddressCity,
                    AddressCountry = input.AddressCountry,
                    AddressZipCode = input.AddressZipCode,
                };


                var res = await _contactRepository.InsertAsync(entity);

                var history = new ContactHistory
                {
                    ContactId = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    PhoneCode = entity.PhoneCode,
                    PhoneNumber = entity.PhoneNumber,
                    AddressStreet = entity.AddressStreet,
                    AddressCity = entity.AddressCity,
                    AddressCountry = entity.AddressCountry,
                    AddressZipCode = entity.AddressZipCode,
                };

                await _contactHistoryRepository.InsertAsync(history);


                result.data = ObjectMapper.Map<Contact, ContactDto>(entity);

                return result;
            }
            catch
            {
                result.message = "Fail to add contact";
                result.success = false;

                return result;
            }
        }

        public async Task<BaseResponseModel> DeleteAsync(Guid id)
        {
            var result = new BaseResponseModel();

            try
            {
                var entity = await _contactRepository.FirstOrDefaultAsync(d => d.Id.Equals(id));

                if (entity == null)
                {
                    result.message = "This Contact not exist!";
                    result.success = false;
                    return result;
                }

                entity.DeletionTime = DateTime.Now;

                await _contactRepository.DeleteAsync(id);

                return result;
            }
            catch (Exception)
            {
                result.message = "Deletion Contact failed!";
                result.success = false;

                return result;
            }
        }

        public async Task<PagedResultDto<ContactDto>> GetListAsync(FilterContactDto input)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            {
                var contactQuery = await _contactRepository.GetQueryableAsync();

                contactQuery = contactQuery
                    .WhereIf(!string.IsNullOrEmpty(input.Name), x => !string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(input.Name!.ToLower()))
                    .WhereIf(!string.IsNullOrEmpty(input.Email), x => !string.IsNullOrEmpty(x.Email) && x.Email.ToLower().Contains(input.Email!.ToLower()))
                    .WhereIf(!string.IsNullOrEmpty(input.PhoneNumber), x => !string.IsNullOrEmpty(x.PhoneNumber) && x.PhoneNumber.ToLower().Contains(input.PhoneNumber!.ToLower()))
                    .Where(x => x.IsDeleted == false) // Chỉ lấy contact chưa bị xóa
                    .OrderBy(input.Sorting ?? "CreationTime DESC");

                var totalCount = await AsyncExecuter.CountAsync(contactQuery);
                var list = await AsyncExecuter.ToListAsync(contactQuery.PageBy(input.SkipCount, input.MaxResultCount));
                var contactlist = ObjectMapper.Map<List<Contact>, List<ContactDto>>(list);

                return new PagedResultDto<ContactDto>(totalCount, contactlist);
            }
        }

        public async Task<ContactDto> GetAsync(Guid id)
        {
            var result = new ContactDto();

            var contact = await _contactRepository.FirstOrDefaultAsync(d => d.Id == id);
            if (contact != null)
            {
                result = ObjectMapper.Map<Contact, ContactDto>(contact);
            }

            return result;
        }

        public async Task<BaseResponseDto<ContactDto>> UpdateAsync(Guid id, UpdateContactDto input)
        {
            var result = new BaseResponseDto<ContactDto>();

            try
            {
                var contact = await _contactRepository.FirstOrDefaultAsync(d => d.Id.Equals(id));

                if (contact == null)
                {
                    result.message = "Contact not found!";
                    result.success = false;
                    return result;
                }
                

                contact.Name = input.Name;
                contact.Email = input.Email;
                contact.PhoneCode = input.PhoneCode;
                contact.PhoneNumber = input.PhoneNumber;
                contact.AddressStreet = input.AddressStreet;
                contact.AddressCity = input.AddressCity;
                contact.AddressCountry = input.AddressCountry;
                contact.AddressZipCode = input.AddressZipCode;

                var res = await _contactRepository.UpdateAsync(contact);

                var history = new ContactHistory
                {
                    ContactId = contact.Id,
                    Name = contact.Name,
                    Email = contact.Email,
                    PhoneCode = contact.PhoneCode,
                    PhoneNumber = contact.PhoneNumber,
                    AddressStreet = contact.AddressStreet,
                    AddressCity = contact.AddressCity,
                    AddressCountry = contact.AddressCountry,
                    AddressZipCode = contact.AddressZipCode,
                };

                await _contactHistoryRepository.InsertAsync(history);

                result.data = ObjectMapper.Map<Contact, ContactDto>(res);

                return result;
            }
            catch
            {
                result.message = "Fail to update contact";
                result.success = false;

                return result;
            }
        }
    }
}
