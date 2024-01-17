using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class Dropdown
{
    [Parameter] public DropdownQuestionModel DropdownQuestionModel { get; set; } = new DropdownQuestionModel();
    [Parameter] public EventCallback<DropdownQuestionModel> DropdownQuestionModelChanged { get; set; }
    
    private class Field
    {
        public string Input { get; set; } = "";
        public DropdownQuestionModel.SubQuestion SubQuestion { get; set; } = new DropdownQuestionModel.SubQuestion();
    }
    
    private List<Field> Fields { get; set; } = new List<Field>();

    private async Task CreateSample()
    {
        DropdownQuestionModel = SampleService.GetDropdownQuestionSample();
        PopulateFields();
        await DropdownQuestionModelChanged.InvokeAsync(DropdownQuestionModel);
    }
    
    private async Task Reset()
    {
        DropdownQuestionModel = new DropdownQuestionModel();
        PopulateFields();
        await DropdownQuestionModelChanged.InvokeAsync(DropdownQuestionModel);
    }

    private void PopulateFields()
    {
        Fields = DropdownQuestionModel.SubQuestions.Select( i => new Field()
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
            SubQuestion = new DropdownQuestionModel.SubQuestion()
            {
                Text = "New",
                Choices = new List<DropdownQuestionModel.Choice>()
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
            field.SubQuestion.Choices.Add(new DropdownQuestionModel.Choice()
            {
                Text = input
            });
            field.Input = "";
        }
    }
    
    private void OnValueChanged(string choice,string subQuestion)
    {
        var subQ = Fields.Select(i => i.SubQuestion).FirstOrDefault(i => i.Text == subQuestion);
        foreach (var item in subQ?.Choices ?? new List<DropdownQuestionModel.Choice>())
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