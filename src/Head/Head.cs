using System.Threading.Tasks;

namespace Skclusive.Script.DomHelpers
{
    public class Head : Portal
    {
        protected override async Task OnAfterRenderAsync()
        {
            TargetHeadRef = SourceRef;

            await base.OnAfterRenderAsync();
        }
    }
}
