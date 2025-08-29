using AutoMapper;
using KnowledgeGraph.ContactHistories;
using KnowledgeGraph.Contacts;
using KnowledgeGraph.Entities;
using KnowledgeGraph.Reviews;

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
        CreateMap<Entity, EntityDto>();
        CreateMap<Review, ReviewDto>();
    }
}
