using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skclusive.Core.Component;
using Microsoft.JSInterop;

namespace Skclusive.Script.DomHelpers
{
    public class DetectThemeHelper : IAsyncDisposable
    #if NETSTANDARD2_0
        , IDisposable
    #endif
    {
        public DetectThemeHelper(IScriptService scriptService)
        {
            ScriptService = scriptService;
        }

        private object Id;

        private IScriptService ScriptService { get; }

        public event EventHandler<Theme> OnChange;

        private static IDictionary<string, Theme> THEME_MAPPING = new Dictionary<string, Theme>
        {
            { "Dark", Theme.Dark },
            { "Light", Theme.Light },
            { "None", Theme.None }
        };

        private readonly static EventArgs EVENT_ARGS = new EventArgs();

        [JSInvokable]
        public ValueTask OnChangeAsync(string theme)
        {
            OnChange?.Invoke(EVENT_ARGS, THEME_MAPPING[theme]);

            return default;
        }

        public async ValueTask InitAsync()
        {
            Id = await ScriptService.InvokeAsync<object>("Skclusive.Script.DomHelpers.ThemeDetector.construct", DotNetObjectReference.Create(this));
        }

        public async ValueTask DisposeAsync()
        {
            OnChange = null;

            if (Id != null)
            {
                await ScriptService.InvokeVoidAsync("Skclusive.Script.DomHelpers.ThemeDetector.dispose", Id);
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
