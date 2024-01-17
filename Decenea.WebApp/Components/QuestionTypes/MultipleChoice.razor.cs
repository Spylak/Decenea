using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleChoice
{
    [Parameter] public MultipleChoiceQuestionModel MultipleChoiceQuestionModel { get; set; } = new MultipleChoiceQuestionModel();
    [Parameter] public EventCallback<MultipleChoiceQuestionModel> MultipleChoiceQuestionModelChanged { get; set; }
    
    private class Field
    {
        public string Input { get; set; } = "";
        public IEnumerable<string> SelectedChoices { get; set; } = new List<string>();
        public MultipleChoiceQuestionModel.SubQuestion SubQuestion { get; set; } = new MultipleChoiceQuestionModel.SubQuestion();
    }
    
    private List<Field> Fields { get; set; } = new List<Field>();

    protected override void OnInitialized()
    {
        Fields = MultipleChoiceQuestionModel.SubQuestions.Select( i => new Field()
        {
            Input = "",
            SubQuestion = i
        }).ToList();
    }

    private void AddNewField()
    {
        Fields.Add(new Field()
        {
            Input = "",
            SubQuestion = new MultipleChoiceQuestionModel.SubQuestion()
            {
                Text = "New",
                Choices = new List<MultipleChoiceQuestionModel.Choice>()
            }
        });
    }
    
    private async Task CreateSample()
    {
        MultipleChoiceQuestionModel = SampleService.GetMultipleChoiceQuestionSample();
        PopulateFields();
        await MultipleChoiceQuestionModelChanged.InvokeAsync(MultipleChoiceQuestionModel);
    }
    
    private async Task Reset()
    {
        MultipleChoiceQuestionModel = new MultipleChoiceQuestionModel();
        PopulateFields();
        await MultipleChoiceQuestionModelChanged.InvokeAsync(MultipleChoiceQuestionModel);
    }
    
    private void RemoveChoices(Field item)
    {
        var remaining = item
            .SubQuestion
            .Choices
            .Where(i => item.SelectedChoices.Contains(i.Text));
        item.SubQuestion.Choices = remaining.ToList();
        item.SelectedChoices = new List<string>();
    }

    private void PopulateFields()
    {
        Fields = MultipleChoiceQuestionModel.SubQuestions.Select( i => new Field()
        {
            Input = "",
            SubQuestion = i
        }).ToList();
    }
    
    private void RemoveField(Field field)
    {
        Fields.Remove(field);
    }
    
    private void AddChoice(Field field,string input)
    {
        if (!field.SubQuestion.Choices.Select(i => i.Text).Contains(input))
        {
            field.SubQuestion.Choices.Add(new MultipleChoiceQuestionModel.Choice()
            {
                Text = input
            });
            field.Input = "";
        }
    }
}