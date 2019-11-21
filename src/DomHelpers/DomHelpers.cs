using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Skclusive.Script.DomHelpers
{
    public class DomHelpers
    {
        private IJSRuntime JSRuntime { get; }

        public DomHelpers(IJSRuntime jsruntime)
        {
            JSRuntime = jsruntime;
        }

        public Task AddClass(ElementReference? element, string clazz, bool trigger = false)
        {
            return AddClasses(element, new List<string>(clazz.Split(' ')), trigger);
        }

        public async Task AddClasses(ElementReference? element, List<string> clazzes, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.addClasses", element, clazzes, trigger);
            }
        }

        public Task RemoveClass(ElementReference? element, string clazz)
        {
            return RemoveClasses(element, new List<string>(clazz.Split(' ')));
        }

        public async Task RemoveClasses(ElementReference? element, List<string> clazzes)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.removeClasses", element, clazzes);
            }
        }

        public async Task UpdateClasses(ElementReference? element, List<string> removes, List<string> adds, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.updateClasses", element, removes, adds, trigger);
            }
        }

        public async Task SetStyle(ElementReference? element, IDictionary<string, object> styles, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.setStyle", element, styles, trigger);
            }
        }

        public async Task<object> GetStyle(ElementReference? element, string style)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<object>("Skclusive.Script.DomHelpers.getStyle", element, style);
            }

            return null;
        }

        public async Task Focus(ElementReference? element)
        {
            if(element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.focus", element);
            }
        }

        public async Task MoveContent(ElementReference? source, ElementReference? target)
        {
            if (source.HasValue && target.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.moveContent", source, target);
            }
        }

        public async Task ClearContent(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.clearContent", element);
            }
        }
    }
}
