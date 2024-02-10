using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class DropdownQuestion
{
    [Parameter]
    public QuestionBaseModel<Dropdown> DropdownQuestionModel { get; set; } = new QuestionBaseModel<Dropdown>(new Dropdown());
    [Parameter] public EventCallback<QuestionBaseModel<Dropdown>> DropdownQuestionModelChanged { get; set; }
    
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
        await DropdownQuestionModelChanged.InvokeAsync(DropdownQuestionModel);
    }
    
    private async Task Reset()
    {
        DropdownQuestionModel = new QuestionBaseModel<Dropdown>(new Dropdown());
        PopulateFields();
        await DropdownQuestionModelChanged.InvokeAsync(DropdownQuestionModel);
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
    
    private void OnValueChanged(string choice,string subQuestion)
    {
        var subQ = Fields.Select(i => i.SubQuestion).FirstOrDefault(i => i.Text == subQuestion);
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
    }
}