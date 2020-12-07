using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Dependency;
using RChat.Authorization.Users;
using RChat.MultiTenancy;

namespace RChat.Authorization
{
    public class MyExternalAuthSource : DefaultExternalAuthenticationSource<Tenant, User>, ITransientDependency
    {
       
        public override string Name
        {
            get { return "MyCustomSource"; }
        }

        public override Task<bool> TryAuthenticateAsync(string name, string plainPassword, Tenant tenant)
        {
            //TODO: authenticate user and return true or false
            return Task.FromResult(true);
        }
    }
}
