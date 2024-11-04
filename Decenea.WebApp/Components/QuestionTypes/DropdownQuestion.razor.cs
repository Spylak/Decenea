using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DropdownQuestion
{
    [Parameter]
    public GenericQuestionModel? DropdownQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> DropdownQuestionBaseModelChanged { get; set; }
    private GenericQuestionModel<Dropdown> DropdownQuestionModel { get; set; } = InitializeGenericQuestionModel();
    
    protected override void OnParametersSet()
    {
        DropdownQuestionModel = GenericQuestionModel.ConvertToGenericModel<Dropdown>(DropdownQuestionBaseModel ?? InitializeGenericQuestionModel());
        PopulateFields();
    }

    private class Field
    {
        public string Input { get; set; } = "";
        public Dropdown.SubQuestion SubQuestion { get; set; } = new();
    }
    
    private List<Field> Fields { get; set; } = [];

    private async Task CreateSample()
    {
        DropdownQuestionModel = SampleHelper.GetDropdownQuestionSample();
        PopulateFields();
        await DropdownQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(DropdownQuestionModel));
    }

    private static GenericQuestionModel<Dropdown> InitializeGenericQuestionModel()
    {
        return new GenericQuestionModel<Dropdown>(new Dropdown())
        {
            QuestionType = QuestionType.Dropdown,
        };
    }
    
    private void Reset()
    {
        DropdownQuestionModel = InitializeGenericQuestionModel();
        ClearFields();
    }

    private void ClearFields()
    {
        Fields = new List<Field>();
    }

    private void PopulateFields()
    {
        Fields = DropdownQuestionModel.QuestionContent.SubQuestions.Select( i => new Field()
        {
            Input = "",
            SubQuestion = i
        }).ToList();
    }
    
    protected override void OnInitialized()
    {
        PopulateFields();
    }

    private void AddNewField()
    {
        Fields.Add(new Field()
        {
            Input = "",
            SubQuestion = new Dropdown.SubQuestion()
            {
                Text = "New",
                Choices = new List<Dropdown.Choice>()
            }
        });
    }
    
    private void RemoveField(Field field)
    {
        Fields.Remove(field);
    }
    
    private void AddChoice(Field field,string input)
    {
        if (!field.SubQuestion.Choices.Select(i => i.Text).Contains(input))
        {
            field.SubQuestion.Choices.Add(new Dropdown.Choice()
            {
                Text = input
            });
            field.Input = "";
        }
    }
    
    private async Task OnValueChanged(string choice,string subQuestion)
    {
        var subQuestions = Fields.Select(i => i.SubQuestion).ToList();
        var subQ = subQuestions.FirstOrDefault(i => i.Text == subQuestion);
        foreach (var item in subQ?.Choices ?? new List<Dropdown.Choice>())
        {
            if (item.Text==choice)
            {
                item.Checked = true;
            }
            else
            {
                item.Checked = false;
            }
        }

        DropdownQuestionModel.QuestionContent = new Dropdown() { SubQuestions = subQuestions };
        await DropdownQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(DropdownQuestionModel));
    }
}