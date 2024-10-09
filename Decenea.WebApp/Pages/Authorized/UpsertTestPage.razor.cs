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

            TestContainer.UpsertTestModel = test ?? new TestModel(){ Title = "Test Name"};
            
            if (test is null)
            {
                var upsertTests = await IndexedDb.UpsertTest.GetAllAsync();
                var upsertTest = upsertTests.Data?.FirstOrDefault();
                if (upsertTest is not null)
                {
                    TestContainer.UpsertTestModel = upsertTest;
                }
            }

            if (!keys.Data?.Contains("upserttest") ?? false)
            {
                if (TestContainer.UpsertTestModel.Id == TestId)
                {
                    var dropTable = await IndexedDb.UpsertTest.DropTableAsync();
                }

                var result = await IndexedDb.UpsertTest.AddAsync(TestContainer.UpsertTestModel);
            }
        }
        await base.OnInitializedAsync();
    }

    private async Task NewTest()
    {
        TestContainer.UpsertTestModel = new TestModel()
        {
            Title = "New Test"
        };
        TestContainer.UpsertTestModel.Description = $"Test with id: {TestContainer.UpsertTestModel.Id}";
        await IndexedDb.UpsertTest.DropTableAsync();
        await IndexedDb.UpsertTest.AddAsync(TestContainer.UpsertTestModel);
        NavigationManager.NavigateTo($"/{Routes.UpsertTest}/{TestContainer.UpsertTestModel.Id}");
    }

    private async Task SampleTest()
    {
        TestContainer.UpsertTestModel = new TestModel()
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
        
        TestContainer.UpsertTestModel.Description = $"Test with id: {TestContainer.UpsertTestModel.Id}";
        await IndexedDb.UpsertTest.DropTableAsync();
        await IndexedDb.UpsertTest.AddAsync(TestContainer.UpsertTestModel);
        
        NavigationManager.NavigateTo($"/{Routes.UpsertTest}/{TestContainer.UpsertTestModel.Id}");
    }

    private async Task Update(GenericQuestionModel entity)
    {
    }

    private async Task<ApiResponseResult<TestDto>> RemoteSave()
    {
        if (string.IsNullOrWhiteSpace(TestContainer.UpsertTestModel.Version))
        {
            return await TestApi.Create(new CreateTestRequest()
            {
                Title = TestContainer.UpsertTestModel.Title,
                Description = TestContainer.UpsertTestModel.Description,
                MinutesToComplete = TestContainer.UpsertTestModel.MinutesToComplete,
                Questions = TestContainer
                    .UpsertTestModel
                    .GenericQuestionModels
                    .Select(i => i.ToDto())
                    .ToList()
            });
        }
        
        return await TestApi.Update(new UpdateTestRequest()
        {
            Id = TestContainer.UpsertTestModel.Id,
            Title = TestContainer.UpsertTestModel.Title,
            Description = TestContainer.UpsertTestModel.Description,
            Version = TestContainer.UpsertTestModel.Version,
            MinutesToComplete = TestContainer.UpsertTestModel.MinutesToComplete,
            Questions = TestContainer
                .UpsertTestModel
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
                TestContainer.UpsertTestModel
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
            ["TestModel"] = TestContainer.UpsertTestModel
        };
        var dialogOptions = new DialogOptions()
        {
            BackdropClick = false,
            CloseOnEscapeKey = true
        };
        if (questionType is not null && questionId is not null)
            parameters.Add("GenericQuestion",TestContainer.UpsertTestModel.GenericQuestionModels.FirstOrDefault(i => i.Id == questionId));
        var dialog = await DialogService.ShowAsync<QuestionTypesDialog>(null, parameters, dialogOptions);
        var result = await dialog.Result;
        await IndexedDb.UpsertTest.UpdateAsync(TestContainer.UpsertTestModel);
    }
}