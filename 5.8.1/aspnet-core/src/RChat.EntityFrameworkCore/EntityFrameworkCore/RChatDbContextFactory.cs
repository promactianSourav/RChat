using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RChat.Configuration;
using RChat.Web;

namespace RChat.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class RChatDbContextFactory : IDesignTimeDbContextFactory<RChatDbContext>
    {
        public RChatDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<RChatDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            RChatDbContextConfigurer.Configure(builder, configuration.GetConnectionString(RChatConsts.ConnectionStringName));

            return new RChatDbContext(builder.Options);
        }
    }
}
