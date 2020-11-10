using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Skclusive.Core.Component;

namespace Skclusive.Script.DomHelpers
{
    public class Portal : EventComponentBase
    {
        /// <summary>
        /// Reference attached to the root element of the component.
        /// </summary>
        [Parameter]
        public IReference RootRef { get; set; } = new Reference();

         /// <summary>
        /// ChildContent of the current component which gets component <see cref="IComponentContext" />.
        /// </summary>
        [Parameter]
        public RenderFragment<IComponentContext> ChildContent { get; set; }

        /// <summary>
        /// Reference attached to the child element of the component.
        /// </summary>
        [Parameter]
        public IReference ChildRef { get; set; } = new Reference("ChildContextRef");


        [Inject]
        public DomHelpers DomHelpers { set; get; }

        /// <summary>
        /// html component tag to be used as container.
        /// </summary>
        [Parameter]
        public string Component { get; set; } = "div";

        /// <summary>
        /// Reference attached to the target.
        /// </summary>
        [Parameter]
        public IReference TargetRef { get; set; }

        /// <summary>
        /// Reference attached to the target body.
        /// </summary>
        [Parameter]
        public IReference TargetBodyRef { get; set; }

        /// <summary>
        /// Reference attached to the target head.
        /// </summary>
        [Parameter]
        public IReference TargetHeadRef { get; set; }

        /// <summary>
        /// Disable the portal behavior.
        /// The children stay within it's parent DOM hierarchy.
        /// </summary>
        [Parameter]
        public bool Disable { get; set; } = false;

        public Portal(): base(string.Empty)
        {
        }

        public bool HasContent => ChildContent != null;

        protected virtual IComponentContext Context => new ComponentContextBuilder()
           .WithClass(Class)
           .WithStyle(Style)
           .WithRefBack(ChildRef)
           .WithDisabled(Disabled)
           .Build();

        protected IReference SourceRef { get; set; } = new Reference();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            if (!Disable)
            {
                builder.OpenElement(0, Component);
                builder.AddAttribute(1, "style", "display: none;");
                builder.AddContent(2, ChildContent.Invoke(Context));
                builder.AddElementReferenceCapture(3, (refx) =>
                {
                    SourceRef.Current = refx;
                });
                builder.CloseElement();
            }
            else
            {
                builder.AddContent(4, ChildContent.Invoke(Context));
            }
        }

        protected override async Task OnAfterRenderAsync()
        {
            await base.OnAfterRenderAsync();

            if (!Disable)
            {
                await DomHelpers.MoveContentAsync(SourceRef.Current, TargetRef?.Current, TargetBodyRef?.Current, TargetHeadRef?.Current);
            }
        }

        protected override async ValueTask DisposeAsync()
        {
            if (!Disable)
            {
                if (ChildRef.Current != null)
                {
                    await DomHelpers.RemoveNodeAsync(ChildRef.Current);
                }
            }
        }
    }
}
