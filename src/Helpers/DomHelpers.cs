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

        public async ValueTask<bool> HasClassAsync(ElementReference? element, string className)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<bool>("Skclusive.Script.DomHelpers.hasClass", element, className);
            }

            return false;
        }

        public ValueTask AddClassAsync(ElementReference? element, string clazz, bool trigger = false)
        {
            return AddClassesAsync(element, new List<string>(clazz.Split(' ')), trigger);
        }

        public async ValueTask AddClassesAsync(ElementReference? element, List<string> clazzes, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.addClasses", element, clazzes, trigger);
            }
        }

        public async ValueTask ToggleClassAsync(ElementReference? element, string className)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.toggleClass", element, className);
            }
        }

        public ValueTask RemoveClassAsync(ElementReference? element, string clazz)
        {
            return RemoveClassesAsync(element, new List<string>(clazz.Split(' ')));
        }

        public async ValueTask RemoveClassesAsync(ElementReference? element, List<string> clazzes)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.removeClasses", element, clazzes);
            }
        }

        public async ValueTask UpdateClassesAsync(ElementReference? element, List<string> removes, List<string> adds, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.updateClasses", element, removes, adds, trigger);
            }
        }

        public async ValueTask SetStyleAsync(ElementReference? element, IDictionary<string, object> styles, bool trigger = false)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.setStyle", element, styles, trigger);
            }
        }

        public async ValueTask<object> GetStyleAsync(ElementReference? element, string style)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<object>("Skclusive.Script.DomHelpers.getStyle", element, style);
            }

            return null;
        }

        public async ValueTask<string> GetInputValueAsync(ElementReference? input)
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

        public async ValueTask<ElementReference> GetActiveElementAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                var id = await JSRuntime.InvokeAsync<string>("Skclusive.Script.DomHelpers.activeElement", element);

                return new ElementReference(id);
            }

            return default(ElementReference);
        }

        public async ValueTask FocusAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.focus", element);
            }
        }

        public async ValueTask BlurAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.blur", element);
            }
        }

        public async ValueTask MoveContentAsync(ElementReference? source, ElementReference? target, ElementReference? targetBody)
        {
            if (source.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.moveContent", source, target, targetBody);
            }
        }

        public async ValueTask CopyContentAsync(ElementReference? source, ElementReference? target)
        {
            if (source.HasValue && target.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.copyContent", source, target);
            }
        }

        public async ValueTask ClearContentAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.clearContent", element);
            }
        }

        public async ValueTask<ElementReference> FindClosetAsync(ElementReference? element, string selector, ElementReference? stopAt)
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

        public async ValueTask<string> GetComputedStyleAsync(ElementReference? element, string psuedoElement)
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

        public async ValueTask ScrollToAsync(ElementReference? element, int value)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.scrollTo", element, value);
            }
        }

        public async ValueTask ScrollLeftAsync(ElementReference? element, int value)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.scrollLeft", element, value);
            }
        }

        public async ValueTask ScrollTopAsync(ElementReference? element, int value)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.scrollTop", element, value);
            }
        }

        public async ValueTask<Boundry> GetOffsetAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<Boundry>("Skclusive.Script.DomHelpers.offset", element);
            }

            return null;
        }

        public async ValueTask<double> GetHeightAsync(ElementReference? element, bool client)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<double>("Skclusive.Script.DomHelpers.height", element, client);
            }

            return -1;
        }

        public async ValueTask<double> GetWidthAsync(ElementReference? element, bool client)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<double>("Skclusive.Script.DomHelpers.width", element, client);
            }

            return -1;
        }

        public async ValueTask<Point> GetPositionAsync(ElementReference? element, ElementReference? offsetParent)
        {
            if (element.HasValue)
            {
                return await JSRuntime.InvokeAsync<Point>("Skclusive.Script.DomHelpers.position", element, offsetParent);
            }

            return null;
        }

        public async ValueTask<ElementReference> GetScrollParentAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                var id = await JSRuntime.InvokeAsync<string>("Skclusive.Script.DomHelpers.scrollParent", element);

                return new ElementReference(id);
            }

            return default(ElementReference);
        }

        public async ValueTask<Boundry> GetBoundryAsync(ElementReference? element)
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

        public async ValueTask<double> GetScrollParentAsync(ElementReference? parent, ElementReference? child)
        {
            if (parent.HasValue && child.HasValue)
            {
                return await JSRuntime.InvokeAsync<double>("Skclusive.Script.DomHelpers.getScrollParent", parent, child);
            }

            return default(double);
        }

        public async ValueTask<Offset> GetElementOffsetAsync(ElementReference? element)
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

        public ValueTask<Offset> GetWindowOffsetAsync(ElementReference? element)
        {
            return JSRuntime.InvokeAsync<Offset>("Skclusive.Script.DomHelpers.getWindowOffset", element);
        }

        public ValueTask RemoveNodeAsync(ElementReference? element)
        {
            return JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.removeNode", element);
        }

        public async ValueTask ResetHeightAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.resetHeight", element);
            }
        }

        public async ValueTask ToggleHeightAsync(ElementReference? element)
        {
            if (element.HasValue)
            {
                await JSRuntime.InvokeVoidAsync("Skclusive.Script.DomHelpers.toggleHeight", element);
            }
        }
    }
}
