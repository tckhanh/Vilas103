using Data;
using Data.Mappings;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartup(typeof(vilas103.Startup))]

// Files related to ASP.NET Identity duplicate the Microsoft ASP.NET Identity file structure and contain initial Microsoft comments.

namespace vilas103
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            AutoMapperConfiguration.Configure();
        }
    }
}