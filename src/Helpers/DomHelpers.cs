using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Skclusive.Core.Component;

namespace Skclusive.Script.DomHelpers
{
    public class DomHelpers
    {
        private IJSRuntime JSRuntime { get; }

        public DomHelpers(IJSRuntime jsruntime)
        {
            JSRuntime = jsruntime;
        }

        public async Task<bool> HasClassAsync(ElementReference? element, string className)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<bool>("Skclusive.Script.DomHelpers.hasClass", element, className);
            }

            return false;
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

        public async Task ToggleClassAsync(ElementReference? element, string className)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.toggleClass", element, className);
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

        public async Task<string> GetInputValueAsync(ElementReference? input)
        {
            if (input.HasValue)
            {
                return await JSRuntime.InvokeAsync<string>("Skclusive.Script.DomHelpers.getInputValue", input);
            }

            return null;
        }

        // public async Task<ElementReference> GetOwnerDocumentAsync(ElementReference? element)
        // {
        //     if (element.HasValue)
        //     {
        //         return await JSRuntime.InvokeAsync<ElementReference>("Skclusive.Script.DomHelpers.ownerDocument", element);
        //     }

        //     return default(ElementReference);
        // }

        public async Task<ElementReference> GetActiveElementAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                var id = await JSRuntime.InvokeAsync<string>("Skclusive.Script.DomHelpers.activeElement", element);

                return new ElementReference(id);
            }

            return default(ElementReference);
        }

        public async Task FocusAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.focus", element);
            }
        }

        public async Task BlurAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.blur", element);
            }
        }

        public async Task MoveContentAsync(ElementReference? source, ElementReference? target, ElementReference? targetBody)
        {
            if (source.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.moveContent", source, target, targetBody);
            }
        }

        public async Task CopyContentAsync(ElementReference? source, ElementReference? target)
        {
            if (source.HasValue && target.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.copyContent", source, target);
            }
        }

        public async Task ClearContentAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.clearContent", element);
            }
        }

        public async Task<ElementReference> FindClosetAsync(ElementReference? element, string selector, ElementReference? stopAt)
        {
            if (element.HasValue)
            {
                var id = await JSRuntime.InvokeAsync<string>("Skclusive.Script.DomHelpers.closest", element, selector, stopAt);

                return new ElementReference(id);
            }

            return default(ElementReference);
        }

        // public async Task<ElementReference> GetOwnerWindowAsync(ElementReference? element)
        // {
        //     if (element.HasValue)
        //     {
        //         return await JSRuntime.InvokeAsync<ElementReference>("Skclusive.Script.DomHelpers.ownerWindow", element);
        //     }

        //     return default(ElementReference);
        // }

        public async Task<string> GetComputedStyleAsync(ElementReference? element, string psuedoElement)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<string>("Skclusive.Script.DomHelpers.getComputedStyle", element, psuedoElement);
            }

            return null;
        }

        // public async Task<bool> IsDocumentAsync(ElementReference? element)
        // {
        //     if (element.HasValue)
        //     {
        //         return await JSRuntime.InvokeAsync<bool>("Skclusive.Script.DomHelpers.isDocument", element);
        //     }

        //     return false;
        // }

        // public async Task<bool> IsWindowAsync(ElementReference? element)
        // {
        //     if (element.HasValue)
        //     {
        //         return await JSRuntime.InvokeAsync<bool>("Skclusive.Script.DomHelpers.isWindow", element);
        //     }

        //     return false;
        // }

        public async Task ScrollToAsync(ElementReference? element, int value)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.scrollTo", element, value);
            }
        }

        public async Task ScrollLeftAsync(ElementReference? element, int value)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.scrollLeft", element, value);
            }
        }

        public async Task ScrollTopAsync(ElementReference? element, int value)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.scrollTop", element, value);
            }
        }

        public async Task<Boundry> GetOffsetAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<Boundry>("Skclusive.Script.DomHelpers.offset", element);
            }

            return null;
        }

        public async Task<double> GetHeightAsync(ElementReference? element, bool client)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<double>("Skclusive.Script.DomHelpers.height", element, client);
            }

            return -1;
        }

        public async Task<double> GetWidthAsync(ElementReference? element, bool client)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<double>("Skclusive.Script.DomHelpers.width", element, client);
            }

            return -1;
        }

        public async Task<Point> GetPositionAsync(ElementReference? element, ElementReference? offsetParent)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<Point>("Skclusive.Script.DomHelpers.position", element, offsetParent);
            }

            return null;
        }

        public async Task<ElementReference> GetScrollParentAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                var id = await JSRuntime.InvokeAsync<string>("Skclusive.Script.DomHelpers.scrollParent", element);

                return new ElementReference(id);
            }

            return default(ElementReference);
        }

        public async Task<Boundry> GetBoundryAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<Boundry>("Skclusive.Script.DomHelpers.getBoundry", element);
            }

            return new Boundry
            {
                Top = 0,

                Left = 0,

                Width = 0,

                Height = 0
            };
        }

        public async Task<double> GetScrollParentAsync(ElementReference? parent, ElementReference? child)
        {
            if (parent.HasValue && child.HasValue)
            {
                return await JSRuntime.InvokeAsync<double>("Skclusive.Script.DomHelpers.getScrollParent", parent, child);
            }

            return default(double);
        }

        public async Task<Offset> GetElementOffsetAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<Offset>("Skclusive.Script.DomHelpers.getElementOffset", element);
            }

            return new Offset
            {
                Width = 0,

                Height = 0
            };
        }

        public async Task<Offset> GetWindowOffsetAsync(ElementReference? element)
        {
            return await JSRuntime.InvokeAsync<Offset>("Skclusive.Script.DomHelpers.getWindowOffset", element);
        }

        public async Task RemoveNodeAsync(ElementReference? element)
        {
            await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.removeNode", element);
        }

        public async Task ResetHeightAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.resetHeight", element);
            }
        }

        public async Task ToggleHeightAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.toggleHeight", element);
            }
        }
    }
}
