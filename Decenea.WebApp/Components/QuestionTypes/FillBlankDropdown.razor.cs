using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class FillBlankDropdown
{
    [Parameter] public FillBlankDropdownQuestionModel FillBlankDropdownQuestionModel { get; set; } = new FillBlankDropdownQuestionModel();
    [Parameter] public EventCallback<FillBlankDropdownQuestionModel> FillBlankDropdownQuestionModelChanged { get; set; }
    private string SpecialChars { get; set; } = "_____";
    private List<string> QuestionText { get; set; } = new List<string>();
    private string DynamicQuestion { get; set; } = "";
    private int NumberOfSpaces { get; set; } = 0;
    private class Field
    {
        public string Input { get; set; } = "";
        public FillBlankDropdownQuestionModel.SpaceOption Option { get; set; } = new FillBlankDropdownQuestionModel.SpaceOption();
    }
    private List<Field> Fields { get; set; } = new List<Field>();

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        UpdateOptions(FillBlankDropdownQuestionModel.Question);
        PopulateDynamicQuestion();
    }

    private void AddOption(Field field,string input)
    {
        if (!field.Option.Choices.Select(i => i.Text).Contains(input))
        {
            field.Option.Choices.Add(new FillBlankDropdownQuestionModel.Choice()
            {
                Text = input
            });
            field.Input = "";
        }
    }
    
    private void UpdateOptions(string question)
    {
        FillBlankDropdownQuestionModel.Question = question;
        NumberOfSpaces= question.Split(SpecialChars).Length - 1;
        var options = new List<FillBlankDropdownQuestionModel.SpaceOption>();
        QuestionText = FillBlankDropdownQuestionModel
            .Question
            .Split(SpecialChars)
            .ToList();
        for (int i = 0; i < NumberOfSpaces; i++)
        {
            if (Fields.Count <= i)
            {
                Fields.Add(new Field()
                {
                    Input = "",
                    Option = FillBlankDropdownQuestionModel.Options.ElementAtOrDefault(i) is not null ? FillBlankDropdownQuestionModel.Options[i] : new FillBlankDropdownQuestionModel.SpaceOption()
                    {
                        SpaceNo = i,
                        Choices = new List<FillBlankDropdownQuestionModel.Choice>()
                    }
                });
            }
            options.Add(Fields[i].Option);
        }
        FillBlankDropdownQuestionModel.Options = options;
    }
    
    private void AddNewField()
    {
        Fields.Add(new Field()
        {
            Input = "",
            Option = new FillBlankDropdownQuestionModel.SpaceOption()
            {
                SpaceNo = Fields.Count + 1,
                Choices = new List<FillBlankDropdownQuestionModel.Choice>()
            }
        });
    }
    
    private void RemoveField(Field field)
    {
        if (Fields.Count > QuestionText.Count() - 1)
        {
            Fields.Remove(field);
        }
    }
    
    private async Task CreateSample()
    {
        FillBlankDropdownQuestionModel = SampleService.GetFillBlankDropdownQuestionSample();
        UpdateOptions(FillBlankDropdownQuestionModel.Question);
        PopulateDynamicQuestion();
        await FillBlankDropdownQuestionModelChanged.InvokeAsync(FillBlankDropdownQuestionModel);
    }
    
    private async Task Reset()
    {
        FillBlankDropdownQuestionModel = new FillBlankDropdownQuestionModel();
        UpdateOptions(FillBlankDropdownQuestionModel.Question);
        PopulateDynamicQuestion();
        Fields = new List<Field>();
        await FillBlankDropdownQuestionModelChanged.InvokeAsync(FillBlankDropdownQuestionModel);
    }
    
    private void PopulateDynamicQuestion()
    {
        DynamicQuestion = "";
        QuestionText = new List<string>();
        QuestionText = FillBlankDropdownQuestionModel.Question.Split(SpecialChars).ToList();
        for (var i=0;i<QuestionText.Count-1;i++)
        {
            var choice = FillBlankDropdownQuestionModel
                .Options[i];
            var value =choice?.Choices.FirstOrDefault(e => e.Checked);
            var answer = value is not null? value.Text : $"_____{i+1}";
            DynamicQuestion += QuestionText[i]+$"\u27A1\u27A1 {answer } \u2B05\u2B05";
        }
        DynamicQuestion += QuestionText[^1];
    }
    
    private void OnValueChange(string args,int j)
    {
        var answers= Fields.Select(i => i.Option)
            .FirstOrDefault(i => i.SpaceNo == j);
        foreach (var itm in answers?.Choices ?? new List<FillBlankDropdownQuestionModel.Choice>())
        {
            if (itm.Text==args)
            {
                itm.Checked = true;
            }
            else
            {
                itm.Checked = false;
            }
        }
        PopulateDynamicQuestion();
    }
}