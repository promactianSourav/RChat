using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using RChat.Authorization.Roles;
using RChat.Authorization.Users;
using RChat.MultiTenancy;
using RChat.UserPerRelations;
using RChat.Messages;

namespace RChat.EntityFrameworkCore
{
    public class RChatDbContext : AbpZeroDbContext<Tenant, Role, User, RChatDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public DbSet<UserPerRelation> UserPerRelations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public RChatDbContext(DbContextOptions<RChatDbContext> options)
            : base(options)
        {
        }
    }
}
