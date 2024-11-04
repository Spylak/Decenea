using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class FillBlankDropdownQuestion
{
    [Parameter]
    public GenericQuestionModel? FillBlankDropdownQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> FillBlankDropdownQuestionBaseModelChanged { get; set; }

    private GenericQuestionModel<FillBlankDropdown> FillBlankDropdownQuestionModel { get; set; } = InitializeGenericQuestionModel();
    private string SpecialChars { get; set; } = "_____";
    private List<string> QuestionText { get; set; } = [];
    private string DynamicQuestion { get; set; } = "";
    private int NumberOfSpaces { get; set; } = 0;
    private class Field
    {
        public string Input { get; set; } = "";
        public FillBlankDropdown.SpaceOption Option { get; set; } = new();
    }
    private List<Field> Fields { get; set; } = [];

    protected override void OnParametersSet()
    {
        FillBlankDropdownQuestionModel = GenericQuestionModel.ConvertToGenericModel<FillBlankDropdown>(FillBlankDropdownQuestionBaseModel ?? InitializeGenericQuestionModel());
        UpdateOptions(FillBlankDropdownQuestionModel.Description);
        PopulateDynamicQuestion();
    }

    private void AddOption(Field field, string input)
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
                        Choices = []
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
                Choices = []
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
        await FillBlankDropdownQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(FillBlankDropdownQuestionModel));
    }

    private static GenericQuestionModel<FillBlankDropdown> InitializeGenericQuestionModel()
    {
        return new GenericQuestionModel<FillBlankDropdown>(new FillBlankDropdown())
        {
            QuestionType = QuestionType.FillBlankDropdown
        };
    }
    
    private void Reset()
    {
        FillBlankDropdownQuestionModel = InitializeGenericQuestionModel();
        UpdateOptions(FillBlankDropdownQuestionModel.Description);
        PopulateDynamicQuestion();
        Fields = [];
    }
    
    private void PopulateDynamicQuestion()
    {
        DynamicQuestion = "";
        QuestionText = [];
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
    
    private async Task OnValueChange(string args,int j)
    {
        var answers= Fields.Select(i => i.Option)
            .FirstOrDefault(i => i.SpaceNo == j);
        
        foreach (var itm in answers?.Choices ?? [])
        {
            if(itm.Text==args)
            {
                itm.Checked = true;
            }
            else
            {
                itm.Checked = false;
            }
        }
        PopulateDynamicQuestion();
        await FillBlankDropdownQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(FillBlankDropdownQuestionModel));
    }
}