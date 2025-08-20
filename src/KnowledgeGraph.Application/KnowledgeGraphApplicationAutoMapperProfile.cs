using AutoMapper;
using KnowledgeGraph.Contacts;
using KnowledgeGraph.ContactHistories;

namespace KnowledgeGraph;

public class KnowledgeGraphApplicationAutoMapperProfile : Profile
{
    public KnowledgeGraphApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Contact, ContactDto>();
        CreateMap<ContactHistory, ContactHistoryDto>();
    }
}
