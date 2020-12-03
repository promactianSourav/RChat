using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RChat.EntityFrameworkCore;
using RChat.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace RChat.Web.Tests
{
    [DependsOn(
        typeof(RChatWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class RChatWebTestModule : AbpModule
    {
        public RChatWebTestModule(RChatEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RChatWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(RChatWebMvcModule).Assembly);
        }
    }
}