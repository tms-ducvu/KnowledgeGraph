using AutoMapper;
using KnowledgeGraph.Contacts;
using KnowledgeGraph.Entities;
using KnowledgeGraph.Reviews;

namespace KnowledgeGraph.Web;

public class KnowledgeGraphWebAutoMapperProfile : Profile
{
    public KnowledgeGraphWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project
        CreateMap<ContactDto, CreateContactDto>();
        CreateMap<ContactDto, UpdateContactDto>();

        CreateMap<EntityDto, CreateEntityDto>();
        CreateMap<EntityDto, UpdateEntityDto>();

        CreateMap<ReviewDto, CreateReviewDto>();
        CreateMap<ReviewDto, UpdateReviewDto>();
    }
}
