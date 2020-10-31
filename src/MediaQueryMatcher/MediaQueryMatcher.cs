using System;
using System.Threading.Tasks;
using Skclusive.Core.Component;
using Microsoft.JSInterop;

namespace Skclusive.Script.DomHelpers
{
    public class MediaQueryMatcher : IAsyncDisposable
    {
        public MediaQueryMatcher(IScriptService scriptService)
        {
            ScriptService = scriptService;
        }

        private object Id;

        private IScriptService ScriptService { get; }

        public event EventHandler<bool> OnChange;

        private readonly static EventArgs EVENT_ARGS = new EventArgs();

        [JSInvokable]
        public ValueTask OnChangeAsync(bool match)
        {
            OnChange?.Invoke(EVENT_ARGS, match);

            return default;
        }

        public async ValueTask InitAsync(string media)
        {
            Id = await ScriptService.InvokeAsync<object>("Skclusive.Script.DomHelpers.MediaQueryMatcher.construct", media, DotNetObjectReference.Create(this));
        }

        public async ValueTask DisposeAsync()
        {
            OnChange = null;

            if (Id != null)
            {
                await ScriptService.InvokeVoidAsync("Skclusive.Script.DomHelpers.MediaQueryMatcher.dispose", Id);
            }
        }
    }
}
