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

        public Task AddClassAsync(ElementReference? element, string clazz, bool trigger = false)
        {
            return AddClassesAsync(element, new List<string>(clazz.Split(' ')), trigger);
        }

        public async Task AddClassesAsync(ElementReference? element, List<string> clazzes, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.addClasses", element, clazzes, trigger);
            }
        }

        public Task RemoveClassAsync(ElementReference? element, string clazz)
        {
            return RemoveClassesAsync(element, new List<string>(clazz.Split(' ')));
        }

        public async Task RemoveClassesAsync(ElementReference? element, List<string> clazzes)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.removeClasses", element, clazzes);
            }
        }

        public async Task UpdateClassesAsync(ElementReference? element, List<string> removes, List<string> adds, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.updateClasses", element, removes, adds, trigger);
            }
        }

        public async Task SetStyleAsync(ElementReference? element, IDictionary<string, object> styles, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.setStyle", element, styles, trigger);
            }
        }

        public async Task<object> GetStyleAsync(ElementReference? element, string style)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<object>("Skclusive.Script.DomHelpers.getStyle", element, style);
            }

            return null;
        }

        public async Task FocusAsync(ElementReference? element)
        {
            if(element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.focus", element);
            }
        }

        public async Task BlurAsync(ElementReference? element)
        {
            if(element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.blur", element);
            }
        }

        public async Task MoveContentAsync(ElementReference? source, ElementReference? target)
        {
            if (source.HasValue && target.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.moveContent", source, target);
            }
        }

        public async Task ClearContentAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.clearContent", element);
            }
        }
    }
}
