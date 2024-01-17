using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services;

namespace Decenea.WebApp.Components.QuestionTypes;


public partial class FillBlank
{

    [Parameter] public FillBlankQuestionModel FillBlankQuestionModel { get; set; } = new FillBlankQuestionModel();
    [Parameter] public EventCallback<FillBlankQuestionModel> FillBlankQuestionModelChanged { get; set; }
    private string DynamicQuestion { get; set; } = "";
    private List<FillBlankQuestionModel.SpaceOption> Fields { get; set; } = new List<FillBlankQuestionModel.SpaceOption>();
    private string SpecialChars { get; set; } = "_____";
    private List<string> QuestionText { get; set; } = new List<string>();
    private int ActiveFieldNumber { get; set; } = 0;
    
    private void UpdateOptions(string question)
    {
        FillBlankQuestionModel.Question = question;
        FillBlankQuestionModel.Options = new List<FillBlankQuestionModel.SpaceOption>();
        QuestionText = new List<string>();
        QuestionText = FillBlankQuestionModel.Question.Split(SpecialChars).ToList();
        for (int i = 0; i < QuestionText.Count() - 1; i++)
        {
            if (Fields.Count <= i)
            {
                Fields.Add(new FillBlankQuestionModel.SpaceOption()
                {
                    SpaceNo = i,
                    Text = ""
                });
            }
            FillBlankQuestionModel.Options.Add(Fields[i]);
        }
        StateHasChanged();
    }

    private void RemoveField(FillBlankQuestionModel.SpaceOption field)
    {
        if (Fields.Count > QuestionText.Count() - 1)
        {
            Fields.Remove(field);
        }
    }
    
    private void AddNewField()
    {
        var max = Fields.Any() ? Fields.Max(i => i.SpaceNo) : 0;
        Fields.Add(new FillBlankQuestionModel.SpaceOption() { SpaceNo = max + 1, Text = "" });
    }
    
    private async Task CreateSample()
    {
        FillBlankQuestionModel = SampleService.GetFillBlankQuestionSample();
        Fields = FillBlankQuestionModel.Options;
        UpdateOptions(FillBlankQuestionModel.Question);
        PopulateDynamicQuestion();
        await FillBlankQuestionModelChanged.InvokeAsync(FillBlankQuestionModel);
    }
    
    private async Task Reset()
    {
        FillBlankQuestionModel = new FillBlankQuestionModel();
        Fields = FillBlankQuestionModel.Options;
        UpdateOptions(FillBlankQuestionModel.Question);
        PopulateDynamicQuestion();
        await FillBlankQuestionModelChanged.InvokeAsync(FillBlankQuestionModel);
    }
    
    protected override void OnInitialized()
    {
        Fields = FillBlankQuestionModel.Options;
        UpdateOptions(FillBlankQuestionModel.Question);
        PopulateDynamicQuestion();
    }

    private void PopulateDynamicQuestion()
    {
        DynamicQuestion = "";
        QuestionText = new List<string>();
        QuestionText = FillBlankQuestionModel.Question.Split(SpecialChars).ToList();
        for (var i=0;i<QuestionText.Count - 1;i++)
        {
            var choice = FillBlankQuestionModel
                .Options?
                .FirstOrDefault(e => e.SpaceNo == i)?
                .Text;
            choice = choice !="" ? choice : $"_____{i+1}";
            DynamicQuestion += (QuestionText[i]+$"\u27A1\u27A1 {choice } \u2B05\u2B05");
        }
        DynamicQuestion += QuestionText[QuestionText.Count - 1];
    }
    
    private void OnValueChange(string args,int j)
    {
        var option = FillBlankQuestionModel.Options.FirstOrDefault(i => i.SpaceNo == j);
        if (option is not null)
            option.Text = args;
        var field =Fields.FirstOrDefault(i => i.SpaceNo == j);
        if (field is null)
            return;
        field.Text = args;
        PopulateDynamicQuestion();
    }
}