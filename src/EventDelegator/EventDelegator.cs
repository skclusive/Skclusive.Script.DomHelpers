using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;
using Microsoft.JSInterop;

namespace Skclusive.Script.DomHelpers
{
    public class EventDelegator : IAsyncDisposable
    #if NETSTANDARD2_0
        , IDisposable
    #endif
    {
        public EventDelegator(IScriptService scriptService)
        {
            ScriptService = scriptService;
        }

        private object Id;

        private IScriptService ScriptService { get; }

        public event EventHandler<string> OnEvent;

        private readonly static EventArgs EVENT_ARGS = new EventArgs();

        [JSInvokable]
        public ValueTask OnEventAsync(string json)
        {
            OnEvent?.Invoke(EVENT_ARGS, json);

            return default;
        }

        public async ValueTask InitAsync(ElementReference reference, string name, int delay)
        {
            Id = await ScriptService.InvokeAsync<object>("Skclusive.Script.DomHelpers.EventDelegator.construct", reference, name, DotNetObjectReference.Create(this), delay);
        }

        public async ValueTask DisposeAsync()
        {
            OnEvent = null;

            if (Id != null)
            {
                await ScriptService.InvokeVoidAsync("Skclusive.Script.DomHelpers.EventDelegator.dispose", Id);
            }
         }

        #if NETSTANDARD2_0

        void IDisposable.Dispose()
        {
            if (this is IAsyncDisposable disposable)
            {
                _ = disposable.DisposeAsync();
            }
        }

        #endif
    }
}
