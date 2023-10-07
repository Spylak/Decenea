using MudBlazor;

namespace Decenea.WebAppAdmin.State;

public class AppState
{
    public bool IsLoading { get; set; }
    public bool IsLoggedIn { get; set; }
    public Breakpoint CurrentBreakpoint { get; set; }
    public bool IsLeftDrawerOpen { get; set; }
    public bool IsRightDrawerOpen { get; set; }
    public void ToggleLeftDrawer()
    {
        IsLeftDrawerOpen = !IsLeftDrawerOpen;
    }
    public event Action? StateHasChanged;

    public void InvokeStateChanged() => StateHasChanged?.Invoke();
}