using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DropdownQuestion
{
    [Parameter]
    public QuestionBaseModel? DropdownQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<QuestionBaseModel> DropdownQuestionBaseModelChanged { get; set; }
    private QuestionBaseModel<Dropdown>? DropdownQuestionModel { get; set; }
    protected override void OnParametersSet()
    {
        if (DropdownQuestionBaseModel is not null)
        {
            DropdownQuestionModel = QuestionBaseModel.ConvertToGenericBaseModel<Dropdown>(DropdownQuestionBaseModel);
            PopulateFields();
        }
    }

    private class Field
    {
        public string Input { get; set; } = "";
        public Dropdown.SubQuestion SubQuestion { get; set; } = new Dropdown.SubQuestion();
    }
    
    private List<Field> Fields { get; set; } = new List<Field>();

    private async Task CreateSample()
    {
        DropdownQuestionModel = SampleHelper.GetDropdownQuestionSample();
        PopulateFields();
        await DropdownQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGenericBaseModel(DropdownQuestionModel));
    }
    
    private void Reset()
    {
        DropdownQuestionModel = new QuestionBaseModel<Dropdown>(new Dropdown());
        DropdownQuestionModel.Id = DropdownQuestionBaseModel?.Id ?? Guid.NewGuid().ToString();
        ClearFields();
    }

    private void ClearFields()
    {
        Fields = new List<Field>();
    }

    private void PopulateFields()
    {
        if(DropdownQuestionModel?.QuestionContent is null)
            return;
        
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

        if (DropdownQuestionModel is not null)
        {
            DropdownQuestionModel.QuestionContent = new Dropdown() { SubQuestions = subQuestions };
            await DropdownQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGenericBaseModel(DropdownQuestionModel));
        }
    }
}