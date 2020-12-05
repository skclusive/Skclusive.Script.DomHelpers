using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Skclusive.Core.Component;

namespace Skclusive.Script.DomHelpers
{
    public class HistoryBackHelper : IAsyncDisposable
    #if NETSTANDARD2_0
        , IDisposable
    #endif
    {
        public HistoryBackHelper(IScriptService scriptService)
        {
            ScriptService = scriptService;
        }

        private object Id;

        private IScriptService ScriptService { get; }

        public async ValueTask InitAsync(ElementReference reference, string name, int delay = 0)
        {
            Id = await ScriptService.InvokeAsync<object>("Skclusive.Script.DomHelpers.HistoryBackHelper.construct", reference, name, delay);
        }

        public async ValueTask DisposeAsync()
        {
            if (Id != null)
            {
                await ScriptService.InvokeVoidAsync("Skclusive.Script.DomHelpers.HistoryBackHelper.dispose", Id);
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
