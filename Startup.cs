using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(wc_Blog.Startup))]
namespace wc_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
