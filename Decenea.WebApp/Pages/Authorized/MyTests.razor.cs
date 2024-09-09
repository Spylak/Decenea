using Blazored.LocalStorage;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Database;
using Decenea.WebApp.Models;
using Decenea.WebApp.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebApp.Pages.Authorized;

public partial class MyTests
{
    [Inject] private TestContainer TestContainer { get; set; }
    [Inject] private IndexedDb IndexedDb { get; set; }
    [Inject] private ILocalStorageService LocalStorageService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    private List<Test> Tests { get; set; } = new List<Test>();

    protected override async Task OnInitializedAsync()
    {
        var tests = await IndexedDb.Tests.GetAllAsync();
        if (tests.IsSuccess)
        {
            Tests = tests.Data?.ToList() ?? new List<Test>();
        }
        await base.OnInitializedAsync();
    }

    private void TakeTest(string testId)
    {
        NavigationManager.NavigateTo($"/ongoingtest/{testId}");
    }

    private async Task RemoveTest(string testId)
    {
        var test = Tests.FirstOrDefault(i => i.Id == testId);
        if(test is null)
            return;
        var remResult = await IndexedDb.Tests.RemoveAsync(test);
        if (remResult.IsSuccess)
        {
            Tests.Remove(test);
        }
    }
}