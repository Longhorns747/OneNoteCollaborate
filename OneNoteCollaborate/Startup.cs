using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OneNoteCollaborate.Startup))]
namespace OneNoteCollaborate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
