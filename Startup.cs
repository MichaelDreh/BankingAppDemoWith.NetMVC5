using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BankingSystem.Startup))]

namespace BankingSystem
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
