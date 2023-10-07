using Decenea.WebAppAdmin.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebAppAdmin.Shared.MainLayout;

public partial class MainLayout : IBrowserViewportObserver, IAsyncDisposable
{
    [Inject]
    private AppState AppState { get; set; } = null!;    
    [Inject]
    private IBrowserViewportService BrowserViewportService{ get; set; } = null!; 
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    public Guid Id { get; } = Guid.NewGuid();
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            AppState.StateHasChanged += StateHasChanged;
            await BrowserViewportService.SubscribeAsync(this, fireImmediately: true);
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public async ValueTask DisposeAsync()
    {
        AppState.StateHasChanged -= StateHasChanged;
        await BrowserViewportService.UnsubscribeAsync(this);
    }

    public Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs)
    {
        AppState.CurrentBreakpoint = browserViewportEventArgs.Breakpoint;
        return InvokeAsync(StateHasChanged);
    }
}