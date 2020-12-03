using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace RChat.EntityFrameworkCore
{
    public static class RChatDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RChatDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RChatDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
