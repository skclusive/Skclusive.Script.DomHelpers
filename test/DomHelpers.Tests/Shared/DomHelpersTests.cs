using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skclusive.Core.Component;

namespace Skclusive.Blazor.DomHelpers.Tests
{
    public class DomHelpersTests
    {
        private IJSRuntime JSRuntime { get; }

        public DomHelpersTests(IJSRuntime jsruntime)
        {
            JSRuntime = jsruntime;
        }

        // public async Task<string> GetDocumentTitleAsync(ElementReference? document)
        // {
        //     if (document.HasValue)
        //     {
        //         return await JSRuntime.InvokeAsync<string>("DomHelpersTests.getDocumentTitle", document);
        //     }

        //     return null;
        // }
    }
}
