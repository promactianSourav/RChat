using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RChat.Authorization;

namespace RChat
{
    [DependsOn(
        typeof(RChatCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class RChatApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<RChatAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(RChatApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
