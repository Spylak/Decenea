using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Database;
using Decenea.WebApp.Extensions;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;
using Decenea.WebApp.State;

namespace Decenea.WebApp.Pages;

public partial class OnGoingTestPage : IDisposable
{
    [Inject] private TestContainer TestContainer { get; set; }
    [Inject] private IndexedDb IndexedDb { get; set; }
    [Inject] private IGlobalFunctionService GlobalFunctionService { get; set; }
    [Parameter] public string? TestId { get; set; }
    private Timer? Timer { get; set; }
    private List<GenericQuestionModel> QuestionBaseModels { get; set; } = new List<GenericQuestionModel>();
    private int ActiveQuestionIndex = 0;
    private GenericQuestionModel ActiveGenericQuestion { get; set; }
    private int TimeRemaining => TestContainer.OngoingTest.MinutesToComplete*60 - ElapsedTime;
    private int ElapsedTime { get; set; }
    TimeSpan TimeSpan => TimeSpan.FromSeconds(TimeRemaining);
    string FormattedTimeRemaining => string.Format("{0:D2}:{1:D2}", (int)TimeSpan.TotalMinutes, TimeSpan.Seconds);

    private void StartTest()
    {
        TestContainer.OngoingTest.StartingTime = DateTime.Now; 
        StateHasChanged();
        // Timer = new Timer(OnTimerElapsed, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }
    private async void OnTimerElapsed(object state)
    {
        if (TimeRemaining <= 0)
        {
            Timer?.Dispose();
        }
        else
        {
            ElapsedTime++;
            await InvokeAsync(StateHasChanged);
        }
    }
    
    public void Dispose()
    {
        Timer?.Dispose();
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

            QuestionBaseModels = TestContainer.OngoingTest.GenericQuestionModels;
            ActiveGenericQuestion = QuestionBaseModels[ActiveQuestionIndex];
            StateHasChanged();
        }
    }

    private void UpdateActiveQuestion(GenericQuestionModel activeGenericQuestion)
    {
        ActiveGenericQuestion = activeGenericQuestion;
        QuestionBaseModels[ActiveQuestionIndex] = activeGenericQuestion;
    }

    private void NextQuestion()
    {
        if (ActiveQuestionIndex + 1 < QuestionBaseModels.Count)
        {
            ActiveQuestionIndex++;
            ActiveGenericQuestion = QuestionBaseModels[ActiveQuestionIndex];
        }
    }

    private void PreviousQuestion()
    {
        if (ActiveQuestionIndex - 1 >= 0)
        {
            ActiveQuestionIndex--;
            ActiveGenericQuestion = QuestionBaseModels[ActiveQuestionIndex];
        }
    }
}