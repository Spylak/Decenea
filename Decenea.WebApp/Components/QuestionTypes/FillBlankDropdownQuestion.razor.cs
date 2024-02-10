using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class FillBlankDropdownQuestion
{
    [Parameter]
    public QuestionBaseModel<FillBlankDropdown> FillBlankDropdownQuestionModel { get; set; } =
        new QuestionBaseModel<FillBlankDropdown>(new FillBlankDropdown());
    [Parameter] public EventCallback<QuestionBaseModel<FillBlankDropdown>> FillBlankDropdownQuestionModelChanged { get; set; }
    private string SpecialChars { get; set; } = "_____";
    private List<string> QuestionText { get; set; } = new List<string>();
    private string DynamicQuestion { get; set; } = "";
    private int NumberOfSpaces { get; set; } = 0;
    private class Field
    {
        public string Input { get; set; } = "";
        public FillBlankDropdown.SpaceOption Option { get; set; } = new FillBlankDropdown.SpaceOption();
    }
    private List<Field> Fields { get; set; } = new List<Field>();

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        UpdateOptions(FillBlankDropdownQuestionModel.Description);
        PopulateDynamicQuestion();
    }

    private void AddOption(Field field,string input)
    {
        if (!field.Option.Choices.Select(i => i.Text).Contains(input))
        {
            field.Option.Choices.Add(new FillBlankDropdown.Choice()
            {
                Text = input
            });
            field.Input = "";
        }
    }
    
    private void UpdateOptions(string question)
    {
        FillBlankDropdownQuestionModel.Description = question;
        NumberOfSpaces= question.Split(SpecialChars).Length - 1;
        var options = new List<FillBlankDropdown.SpaceOption>();
        QuestionText = FillBlankDropdownQuestionModel
            .Description
            .Split(SpecialChars)
            .ToList();
        for (int i = 0; i < NumberOfSpaces; i++)
        {
            if (Fields.Count <= i)
            {
                Fields.Add(new Field()
                {
                    Input = "",
                    Option = FillBlankDropdownQuestionModel.QuestionContent.Options.ElementAtOrDefault(i) is not null ? FillBlankDropdownQuestionModel.QuestionContent.Options[i] : new FillBlankDropdown.SpaceOption()
                    {
                        SpaceNo = i,
                        Choices = new List<FillBlankDropdown.Choice>()
                    }
                });
            }
            options.Add(Fields[i].Option);
        }
        FillBlankDropdownQuestionModel.QuestionContent.Options = options;
    }
    
    private void AddNewField()
    {
        Fields.Add(new Field()
        {
            Input = "",
            Option = new FillBlankDropdown.SpaceOption()
            {
                SpaceNo = Fields.Count + 1,
                Choices = new List<FillBlankDropdown.Choice>()
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
        FillBlankDropdownQuestionModel = SampleHelper.GetFillBlankDropdownQuestionSample();
        UpdateOptions(FillBlankDropdownQuestionModel.Description);
        PopulateDynamicQuestion();
        await FillBlankDropdownQuestionModelChanged.InvokeAsync(FillBlankDropdownQuestionModel);
    }
    
    private async Task Reset()
    {
        FillBlankDropdownQuestionModel = new QuestionBaseModel<FillBlankDropdown>(new FillBlankDropdown());
        UpdateOptions(FillBlankDropdownQuestionModel.Description);
        PopulateDynamicQuestion();
        Fields = new List<Field>();
        await FillBlankDropdownQuestionModelChanged.InvokeAsync(FillBlankDropdownQuestionModel);
    }
    
    private void PopulateDynamicQuestion()
    {
        DynamicQuestion = "";
        QuestionText = new List<string>();
        QuestionText = FillBlankDropdownQuestionModel.Description.Split(SpecialChars).ToList();
        for (var i=0;i<QuestionText.Count-1;i++)
        {
            var choice = FillBlankDropdownQuestionModel
                .QuestionContent
                .Options[i];
            var value =choice.Choices.FirstOrDefault(e => e.Checked);
            var answer = value is not null? value.Text : $"_____{i+1}";
            DynamicQuestion += QuestionText[i]+$"\u27A1\u27A1 {answer } \u2B05\u2B05";
        }
        DynamicQuestion += QuestionText[^1];
    }
    
    private void OnValueChange(string args,int j)
    {
        var answers= Fields.Select(i => i.Option)
            .FirstOrDefault(i => i.SpaceNo == j);
        foreach (var itm in answers?.Choices ?? new List<FillBlankDropdown.Choice>())
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