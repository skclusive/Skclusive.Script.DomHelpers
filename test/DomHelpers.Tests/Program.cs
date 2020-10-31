﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Skclusive.Script.DomHelpers;
using Skclusive.Core.Component;

namespace Skclusive.Blazor.DomHelpers.Tests
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.TryAddDomHelpersServices(new CoreConfigBuilder().Build());

            // builder.Services.AddSingleton<DomHelpersTests>();

            await builder.Build().RunAsync();
        }
    }
}
