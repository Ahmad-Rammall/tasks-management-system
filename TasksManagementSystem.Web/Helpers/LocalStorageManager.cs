using Microsoft.JSInterop;

namespace TasksManagementSystem.Web.Helpers
{
    public static class LocalStorageManager
    {
        public static async Task SaveToLocalStorage(IJSRuntime jsRuntime, string key, string value)
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }
    }
}
