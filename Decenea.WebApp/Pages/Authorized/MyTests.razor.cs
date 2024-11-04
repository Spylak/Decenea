using Blazored.LocalStorage;
using Decenea.Common.Apis;
using Decenea.Common.Requests.Test;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Database;
using Decenea.WebApp.Mappers;
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
    [Inject] private ITestApi TestApi { get; set; }
    private List<TestModel> Tests { get; set; } = new List<TestModel>();

    protected override async Task OnInitializedAsync()
    {
        var tests = await IndexedDb.Tests.GetAllAsync();
        if (tests.IsSuccess)
        {
            Tests = tests.Data?.ToList() ?? [];
        }

        var remoteTests = await TestApi.Get(new GetManyTestsRequest());
        if (!remoteTests.IsError)
        {
            Tests.AddRange(remoteTests.Data?.Select(i => i.ToTestModel()) ?? []);
        }
        await base.OnInitializedAsync();
    }

    private void TakeTest(string testId)
    {
        TestContainer.OngoingTest = Tests.FirstOrDefault(t => t.Id == testId);
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