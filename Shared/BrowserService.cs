using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace FMV_Standard.Shared
{
    public class BrowserService
    {
        private readonly IJSRuntime _js;

        public BrowserService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<BrowserDimension> getDimensions()
        {
            return await _js.InvokeAsync<BrowserDimension>("getDimensions");
        }

    }

    public class BrowserDimension
    {
        public int width { get; set; }
        public int height { get; set; }
    }
}