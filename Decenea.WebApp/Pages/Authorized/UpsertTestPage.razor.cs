using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Enums;
using Decenea.Common.Requests.Test;
using Decenea.WebApp.Apis;
using Decenea.WebApp.Components;
using Decenea.WebApp.Constants;
using Decenea.WebApp.Helpers;
using Decenea.WebApp.Mappers;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebApp.Pages.Authorized;

public partial class UpsertTestPage
{
    [Inject] private TestContainer TestContainer { get; set; }
    [Inject] private ITestApi TestApi { get; set; }
    [Parameter] public string? TestId { get; set; }
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

            TestContainer.UpsertTest = test ?? new Test(){ Title = "TestName"};
            
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
            Title = "New Test"
        };
        TestContainer.UpsertTest.Description = $"Test with id: {TestContainer.UpsertTest.Id}";
        await IndexedDb.UpsertTest.DropTableAsync();
        await IndexedDb.UpsertTest.AddAsync(TestContainer.UpsertTest);
        NavigationManager.NavigateTo($"/{Routes.UpsertTest}/{TestContainer.UpsertTest.Id}");
    }

    private async Task SampleTest()
    {
        TestContainer.UpsertTest = new Test()
        {
            Title = "Sample Test",
            GenericQuestionModels = new List<GenericQuestionModel>()
            {
                SampleHelper.GetOrderingDnDQuestionSample(),
                SampleHelper.GetDropdownQuestionSample(),
                SampleHelper.GetOrderingQuestionSample(),
                SampleHelper.GetDragAndDropQuestionSample(),
                SampleHelper.GetFillBlankQuestionSample(),
                SampleHelper.GetFillBlankDropdownQuestionSample(),
                SampleHelper.GetMultipleChoiceSingleQuestionSample(),
                SampleHelper.GetMultipleChoiceQuestionSample(),
                SampleHelper.GetMultipleYesOrNoQuestionSample(),
            },
        };
        
        TestContainer.UpsertTest.Description = $"Test with id: {TestContainer.UpsertTest.Id}";
        await GlobalFunctionService.ConsoleLogAsync(TestContainer.UpsertTest);
        await IndexedDb.UpsertTest.DropTableAsync();
        await IndexedDb.UpsertTest.AddAsync(TestContainer.UpsertTest);
        
        NavigationManager.NavigateTo($"/{Routes.UpsertTest}/{TestContainer.UpsertTest.Id}");
    }

    private async Task Update(GenericQuestionModel entity)
    {
    }

    private async Task<ApiResponseResult<TestDto>> RemoteSave()
    {
        if (string.IsNullOrWhiteSpace(TestContainer.UpsertTest.Version))
        {
            return await TestApi.Create(new CreateTestRequest()
            {
                Title = TestContainer.UpsertTest.Title,
                Description = TestContainer.UpsertTest.Description,
                Questions = TestContainer
                    .UpsertTest
                    .GenericQuestionModels
                    .Select(i => i.ToDto())
                    .ToList()
            });
        }
        
        return await TestApi.Update(new UpdateTestRequest()
        {
            Id = TestContainer.UpsertTest.Id,
            Title = TestContainer.UpsertTest.Title,
            Description = TestContainer.UpsertTest.Description,
            Version = TestContainer.UpsertTest.Version,
            Questions = TestContainer
                .UpsertTest
                .GenericQuestionModels
                .Select(i => i.ToDto())
                .ToList()
        });
    }

    async Task CommittedItemChanges(GenericQuestionModel item)
    {
        await Update(item);
    }

    private async Task RemoveRange(ICollection<GenericQuestionModel> questions)
    {
        if (questions is null)
            return;
        foreach (var question in questions)
        {
            var obj =
                TestContainer.UpsertTest
                    .GetType()
                    .GetProperty(nameof(question.QuestionType));
            obj
                .GetType()
                .GetMethod("Remove")
                .Invoke(obj, new[] { question });
        }
    }

    private async Task QuestionDialog(QuestionType? questionType = null, string? questionId = null)
    {
        var parameters = new DialogParameters
        {
            ["ButtonText"] = "Done",
            ["Color"] = Color.Success,
            ["Test"] = TestContainer.UpsertTest
        };
        var dialogOptions = new DialogOptions()
        {
            BackdropClick = false,
            CloseOnEscapeKey = true
        };
        if (questionType is not null && questionId is not null)
            parameters.Add("GenericQuestion",TestContainer.UpsertTest.GenericQuestionModels.FirstOrDefault(i => i.Id == questionId));
        var dialog = await DialogService.ShowAsync<QuestionTypesDialog>(null, parameters, dialogOptions);
        var result = await dialog.Result;
        await IndexedDb.UpsertTest.UpdateAsync(TestContainer.UpsertTest);
    }
}