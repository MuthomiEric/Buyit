using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce_Web_API.Installers
{
    public interface IInstaller
    {
        void RegisterServices(IServiceCollection services, IConfiguration Configuration);
    }
}
