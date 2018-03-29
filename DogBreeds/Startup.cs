using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DogBreeds.Startup))]
namespace DogBreeds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
