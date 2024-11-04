using Decenea.Common.Apis;
using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Requests.Answer;
using Decenea.Common.Requests.Test;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Constants;
using Decenea.WebApp.Database;
using Decenea.WebApp.Mappers;
using Decenea.WebApp.State;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebApp.Pages.Authorized;

public partial class OnGoingTestPage
{
    [Inject] private TestContainer TestContainer { get; set; }
    [Inject] private IndexedDb IndexedDb { get; set; }
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    [Inject] private ITestApi TestApi { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Parameter] public string? TestId { get; set; }
    private MudDialog MudDialog { get; set; }
    private int ActiveQuestionIndex = 0;
    private bool IsServerConnected = true;
    private bool SubmittedTest = false;
    private readonly Dictionary<int, GenericQuestionModel> _genericQuestionModels = new();

    private async Task StartTest()
    {
        if (TestContainer.OngoingTest is not null)
        {
            TestContainer.OngoingTest.StartingTime = DateTime.Now;
        }

        await IndexedDb.OngoingTest.AddAsync(TestContainer.OngoingTest);
        StateHasChanged();
    }

    private async Task UpdateTestToIdb()
    {
        TestContainer.OngoingTest!.GenericQuestionModels = TestContainer.OngoingTest.GenericQuestionModels.OrderBy(i => i.Order).ToList();
        await IndexedDb.OngoingTest.UpdateAsync(TestContainer.OngoingTest);
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);

        if (TestId is not null)
        {
            var testApiResponseResult =
                await TestApi.Get(new GetActiveTestRequest() { Id = TestId });

            TestContainer.OngoingTest = testApiResponseResult.Data?.ToTestModel();
        }

        var keys = await IndexedDb.GetKeysAsync();
        if (keys.IsSuccess)
        {
            var onGoingTests = await IndexedDb
                .OngoingTest
                .GetAllAsync();

            if (onGoingTests.Data is not null)
            {
                var onGoingTest = onGoingTests.Data.FirstOrDefault();
                if (onGoingTest is not null)
                {
                    if (onGoingTest.Id == TestContainer.OngoingTest?.Id)
                    {
                        TestContainer.OngoingTest = onGoingTest;
                    }
                }
            }
            else
            {
                await IndexedDb.OngoingTest.AddAsync(TestContainer.OngoingTest);
            }

            if (TestContainer.OngoingTest is null)
                return;
        }

        for (int i = 0; i < TestContainer.OngoingTest?.GenericQuestionModels.Count; i++)
        {
            TestContainer.OngoingTest.GenericQuestionModels[i].Order = i;
            _genericQuestionModels[i] = TestContainer.OngoingTest.GenericQuestionModels[i];
        }

        StateHasChanged();
    }

    private void NextQuestion()
    {
        if (ActiveQuestionIndex + 1 < TestContainer.OngoingTest?.GenericQuestionModels.Count)
        {
            ActiveQuestionIndex++;
        }
    }

    private async Task Submit()
    {
        var result = await TestApi.UpsertTestAnswers(new UpsertAnswersRequest
        {
            TestId = TestContainer.OngoingTest!.Id,
            Answers = TestContainer
                .OngoingTest
                .GenericQuestionModels
                .Select(i => i.ToAnswerDto())
                .ToList()
        });

        if (!result.IsError)
        {
            await IndexedDb.OngoingTest.DropTableAsync();
            await MudDialog.CloseAsync(DialogResult.Ok(result));
            Snackbar.Add(Messages.AnswerSaved, Severity.Success);
            SubmittedTest = true;
            StateHasChanged();
        }
    }

    private void PreviousQuestion()
    {
        if (ActiveQuestionIndex - 1 >= 0)
        {
            ActiveQuestionIndex--;
        }
    }
}