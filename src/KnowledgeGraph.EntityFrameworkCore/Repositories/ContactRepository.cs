using KnowledgeGraph.Contacts;
using KnowledgeGraph.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace KnowledgeGraph.Repositories
{
    public class ContactRepository : EfCoreRepository<KnowledgeGraphDbContext, Contact, Guid>, IContactRepository
    {
        public ContactRepository(IDbContextProvider<KnowledgeGraphDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}