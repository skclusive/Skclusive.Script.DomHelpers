﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Skclusive.Core.Component;

namespace Skclusive.Script.DomHelpers
{
    public static class DomHelpersExtension
    {
        public static void TryAddDomHelpersServices(this IServiceCollection services, ICoreConfig config)
        {
            services.TryAddCoreServices(config);
            services.TryAddScoped<DomHelpers>();
            services.TryAddScriptTypeProvider<DomHelpersScriptProvider>();
        }
    }
}