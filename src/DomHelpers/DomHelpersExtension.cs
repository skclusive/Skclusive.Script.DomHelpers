using Microsoft.Extensions.DependencyInjection;

namespace Skclusive.Script.DomHelpers
{
    public static class DomHelpersExtension
    {
        public static void AddDomHelpers(this IServiceCollection services)
        {
            services.AddSingleton<DomHelpers>();
        }
    }
}
