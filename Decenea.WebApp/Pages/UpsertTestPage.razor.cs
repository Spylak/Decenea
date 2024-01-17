using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Decenea.WebApp.Components;
using Decenea.WebApp.Constants;
using Decenea.WebApp.Database;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;
using Decenea.WebApp.State;

namespace Decenea.WebApp.Pages;

public partial class UpsertTestPage
{
    [Inject] private TestContainer TestContainer { get; set; }
    [Inject] private ISampleService SampleService { get; set; }
    [Inject] private IndexedDb IndexedDb { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ILocalStorageService LocalStorageService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    [Parameter] public string? TestId { get; set; }
    [CascadingParameter] public EventCallback DrawerRightOpen { get; set; }
    private bool TestNameEdit { get; set; } = false;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        base.SetParametersAsync(parameters);
    }

    protected override async Task OnInitializedAsync()
    {
        var keys = await IndexedDb.GetKeysAsync();
        if (keys.IsSuccess)
        {
            var tests = await IndexedDb
                .Tests
                .GetAllAsync();

            var test = tests.Data?
                .FirstOrDefault(i => i.Id == TestId);

            TestContainer.UpsertTest = test ?? new Test(){ Name = "TestName"};
            
            if (test is null)
            {
                var upsertTests = await IndexedDb.UpsertTest.GetAllAsync();
                var upsertTest = upsertTests.Data?.FirstOrDefault();
                if (upsertTest is not null)
                {
                    TestContainer.UpsertTest = upsertTest;
                }
            }

            if (!keys.Data?.Contains("upserttest") ?? false)
            {
                if (TestContainer.UpsertTest.Id == TestId)
                {
                    var dropTable = await IndexedDb.UpsertTest.DropTableAsync();
                }

                var result = await IndexedDb.UpsertTest.AddAsync(TestContainer.UpsertTest);
            }
        }
        await base.OnInitializedAsync();
    }

    private async Task NewTest()
    {
        TestContainer.UpsertTest = new Test()
        {
            Name = "New Test"
        };
        TestContainer.UpsertTest.Description = $"Test with id: {TestContainer.UpsertTest.Id}";
        await IndexedDb.UpsertTest.DropTableAsync();
        await IndexedDb.UpsertTest.AddAsync(TestContainer.UpsertTest);
        NavigationManager.NavigateTo($"/{Routes.UpsertTest}/{TestContainer.UpsertTest.Id}");
    }

    private async Task Update(QuestionBaseModel entity)
    {
    }

    async Task CommittedItemChanges(QuestionBaseModel item)
    {
        await Update(item);
    }

    private async Task RemoveRange(ICollection<QuestionBaseModel> questions)
    {
        if (questions is null)
            return;
        foreach (var question in questions)
        {
            var obj =
                TestContainer.UpsertTest
                    .GetType()
                    .GetProperty(question.QuestionType);
            obj
                .GetType()
                .GetMethod("Remove")
                .Invoke(obj, new[] { question });
        }
    }

    private async Task QuestionDialog(string? questionType = null, string? questionId = null)
    {
        var parameters = new DialogParameters
        {
            ["ButtonText"] = "Done",
            ["Color"] = Color.Success,
            ["Test"] = TestContainer.UpsertTest
        };
        var dialogOptions = new DialogOptions()
        {
            DisableBackdropClick = true,
            CloseOnEscapeKey = true
        };
        if (questionType is not null && questionId is not null)
            parameters.Add("Question", QuestionHelper.GetQuestion(TestContainer.UpsertTest, questionType, questionId));
        var dialog = DialogService.Show<QuestionTypesDialog>(null, parameters, dialogOptions);
        var result = await dialog.Result;
        await IndexedDb.UpsertTest.UpdateAsync(TestContainer.UpsertTest);
    }
}