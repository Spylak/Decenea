using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.WebApp.Database;
using Decenea.WebApp.State;

namespace Decenea.WebApp.Pages.Shared;

public partial class MainLayout
{
    [Inject] private IndexedDb IndexedDb { get; set; }
    [Inject] private AuthStateProvider AuthStateProvider { get; set; }
    MudTheme currentTheme = new MudTheme();
    List<string> list = new List<string>()
    {
        "/login", "/register", "/profile", "/recipeform"
    };
    bool _drawerRightOpen = false;
    
    bool _drawerLeftOpen = true;
    private EventCallback drawerRightOpen => EventCallback.Factory.Create(this, DrawerRightToggle);

    protected override async Task OnInitializedAsync()
    {
        var lsTheme = await UserService.GetThemeLocalStorage();
        {
            
            switch (lsTheme)
            {
                case SD.DarkTheme:
                    currentTheme = Themes.DarkTheme;
                    await UserService.SetThemeLocalStorage(lsTheme);
                    break;
                case SD.DefaultTheme:
                    currentTheme = Themes.DefaultTheme;
                    await UserService.SetThemeLocalStorage(lsTheme);
                    break;
                default:
                    currentTheme = Themes.DarkTheme;
                    await UserService.SetThemeLocalStorage(SD.DarkTheme);
                    break;
            }
        }
        var onGoingTests = await IndexedDb.OngoingTest.GetAllAsync();
        if (onGoingTests.Data is not null)
        {
            var onGoingTest = onGoingTests.Data.FirstOrDefault();
            if (onGoingTest is not null)
            {
                if (onGoingTest.FinishTime <= DateTime.Now)
                {
                    var result = await IndexedDb.CompletedTests.AddAsync(onGoingTest);
                    if (result.IsSuccess)
                    {
                        await IndexedDb.OngoingTest.DropTableAsync();
                    }
                }
            }
        }
        StateHasChanged();
    }


    private async Task OnClick(string buttonName)
    {
        NavigationManager.NavigateTo($"{buttonName}");
    }
   

    void DrawerLeftToggle()
    {
        _drawerLeftOpen = !_drawerLeftOpen;
    }
    void DrawerRightToggle()
    {
        _drawerRightOpen = !_drawerRightOpen;
    }

    void DarkMode()
    {
        if (currentTheme==Themes.DefaultTheme)
        {
            currentTheme = Themes.DarkTheme;
            UserService.SetThemeLocalStorage("darkTheme");
            StateHasChanged();
        }
        else
        {
            currentTheme = Themes.DefaultTheme;
            UserService.SetThemeLocalStorage("defaultTheme");
            StateHasChanged();
        }
    }
}