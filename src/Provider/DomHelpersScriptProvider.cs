using Skclusive.Core.Component;

namespace Skclusive.Script.DomHelpers
{
    public class DomHelpersScriptProvider : ScriptTypeProvider
    {
        public DomHelpersScriptProvider() : base(priority: 1, typeof(DomHelpersScript))
        {
        }
    }
}
