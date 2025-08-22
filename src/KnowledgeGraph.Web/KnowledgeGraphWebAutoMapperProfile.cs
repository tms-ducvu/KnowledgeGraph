using AutoMapper;
using KnowledgeGraph.Contacts;

namespace KnowledgeGraph.Web;

public class KnowledgeGraphWebAutoMapperProfile : Profile
{
    public KnowledgeGraphWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project
        CreateMap<ContactDto, CreateContactDto>();
        CreateMap<ContactDto, UpdateContactDto>();
    }
}
