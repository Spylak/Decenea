using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Database;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.State;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Pages.Authorized;

public partial class OnGoingTestPage
{
    [Inject] private TestContainer TestContainer { get; set; }
    [Inject] private IndexedDb IndexedDb { get; set; }
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    [Parameter] public string? TestId { get; set; }
    private int ActiveQuestionIndex = 0;
    private Dictionary<int, GenericQuestionModel> _genericQuestionModels = new Dictionary<int, GenericQuestionModel>();
    private async Task StartTest()
    {
        TestContainer.OngoingTest.StartingTime = DateTime.Now;
        await IndexedDb.OngoingTest.AddAsync(TestContainer.OngoingTest);
        StateHasChanged();
    }
    
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        var keys = await IndexedDb.GetKeysAsync();
        if (keys.IsSuccess)
        {
            var tests = await IndexedDb
                .Tests
                .GetAllAsync();
            
            var onGoingTests = await IndexedDb
                .OngoingTest
                .GetAllAsync();
            
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
                            return;
                        }
                    }
                    else
                    {
                        if (onGoingTest.Id == TestId || TestId is null )
                        {
                            TestContainer.OngoingTest = onGoingTest;
                            StateHasChanged();
                            return;
                        }
                    }
                }
            }

            var test = tests.Data?
                .FirstOrDefault(i => i.Id == TestId);
            
            if (test is null)
                return;

            TestContainer.OngoingTest = test;
            if (!keys.Data?.Contains("ongoingtest") ?? false)
            {
                if (TestContainer.OngoingTest.Id != TestId)
                {
                    var result = await IndexedDb.OngoingTest.AddAsync(TestContainer.UpsertTest);
                    // var dropTable = await IndexedDb.OngoingTest.DropTable();
                }
            }

            for (int i = 0; i < TestContainer.OngoingTest.GenericQuestionModels.Count; i++)
            {
                _genericQuestionModels[i] = TestContainer.OngoingTest.GenericQuestionModels[i];
            }
            StateHasChanged();
        }
    }

    private void NextQuestion()
    {
        if (ActiveQuestionIndex + 1 < TestContainer.OngoingTest.GenericQuestionModels.Count)
        {
            ActiveQuestionIndex++;
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