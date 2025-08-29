using KnowledgeGraph.ContactHistories;
using KnowledgeGraph.Contacts;
using KnowledgeGraph.Entities;
using KnowledgeGraph.Reviews;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace KnowledgeGraph.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ConnectionStringName("Default")]
public class KnowledgeGraphDbContext :
    AbpDbContext<KnowledgeGraphDbContext>,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext 
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext .
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    #endregion

    public DbSet<ContactHistory> ContactHistories { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public KnowledgeGraphDbContext(DbContextOptions<KnowledgeGraphDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureBlobStoring();

        builder.Entity<Contact>(b =>
        {
            b.ToTable(KnowledgeGraphConsts.DbTablePrefix + "Contacts",
                KnowledgeGraphConsts.DbSchema);
            b.TryConfigureConcurrencyStamp();
            b.TryConfigureExtraProperties();
            b.TryConfigureObjectExtensions();
            b.TryConfigureMayHaveCreator();
            b.TryConfigureMustHaveCreator();
            b.TryConfigureSoftDelete();
            b.TryConfigureCreationTime();
            b.TryConfigureMultiTenant();
            b.Property(x => x.Name).HasMaxLength(255);
            b.Property(x => x.Email).HasMaxLength(255);
            b.Property(x => x.PhoneCode).HasMaxLength(255);
            b.Property(x => x.PhoneNumber).HasMaxLength(255);
            b.Property(x => x.AddressStreet).HasMaxLength(255);
            b.Property(x => x.AddressCity).HasMaxLength(255);
            b.Property(x => x.AddressCountry).HasMaxLength(255);
            b.Property(x => x.AddressZipCode).HasMaxLength(255);
        });

        builder.Entity<ContactHistory>(b =>
        {
            b.ToTable(KnowledgeGraphConsts.DbTablePrefix + "ContactHistories",
                KnowledgeGraphConsts.DbSchema);
            b.TryConfigureConcurrencyStamp();
            b.TryConfigureExtraProperties();
            b.TryConfigureObjectExtensions();
            b.TryConfigureMayHaveCreator();
            b.TryConfigureMustHaveCreator();
            b.TryConfigureSoftDelete();
            b.TryConfigureCreationTime();
            b.TryConfigureMultiTenant();
            b.Property(x => x.ContactId).IsRequired();
            b.Property(x => x.Name).HasMaxLength(255);
            b.Property(x => x.Email).HasMaxLength(255);
            b.Property(x => x.PhoneCode).HasMaxLength(255);
            b.Property(x => x.PhoneNumber).HasMaxLength(255);
            b.Property(x => x.AddressStreet).HasMaxLength(255);
            b.Property(x => x.AddressCity).HasMaxLength(255);
            b.Property(x => x.AddressCountry).HasMaxLength(255);
            b.Property(x => x.AddressZipCode).HasMaxLength(255);
            b.Property(x => x.SyncStatus).HasMaxLength(50);
            b.Property(x => x.SyncLog).HasColumnType("nvarchar(max)");
        });

        builder.Entity<Entity>(b =>
        {
            b.ToTable(KnowledgeGraphConsts.DbTablePrefix + "Entities",
                KnowledgeGraphConsts.DbSchema);
            b.TryConfigureConcurrencyStamp();
            b.TryConfigureExtraProperties();
            b.TryConfigureObjectExtensions();
            b.TryConfigureMayHaveCreator();
            b.TryConfigureMustHaveCreator();
            b.TryConfigureSoftDelete();
            b.TryConfigureCreationTime();
            b.TryConfigureMultiTenant();
            b.Property(x => x.EntityName).HasMaxLength(255);
            b.Property(x => x.EntityCode).HasMaxLength(50);
            b.Property(x => x.EntityBusinessType).HasMaxLength(100);
            b.Property(x => x.EntityPhone).HasMaxLength(20);
            b.Property(x => x.EntityEmail).HasMaxLength(255);
            b.Property(x => x.EntityWebsite).HasMaxLength(255);
            b.Property(x => x.EntityIsActive).HasDefaultValue(false);
        });



        builder.Entity<Review>(b =>
        {
            b.ToTable(KnowledgeGraphConsts.DbTablePrefix + "Reviews",
                KnowledgeGraphConsts.DbSchema);
            b.TryConfigureConcurrencyStamp();
            b.TryConfigureExtraProperties();
            b.TryConfigureObjectExtensions();
            b.TryConfigureMayHaveCreator();
            b.TryConfigureMustHaveCreator();
            b.TryConfigureSoftDelete();
            b.TryConfigureCreationTime();
            b.TryConfigureMultiTenant();
            b.Property(x => x.ReviewEntityId).IsRequired();
            b.Property(x => x.ReviewerName).HasMaxLength(255).IsRequired();
            b.Property(x => x.ReviewPlatformId).IsRequired();
            b.Property(x => x.ReviewReviewDate).IsRequired();
            b.Property(x => x.ReviewRating).IsRequired();
            b.Property(x => x.ReviewRating).HasMaxLength(5);
            b.Property(x => x.ReviewContent).HasColumnType("nvarchar(max)").IsRequired(false);
            b.Property(x => x.ReviewSyncTime).IsRequired(false);
        });



    }
}
