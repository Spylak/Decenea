using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleChoiceQuestion
{
    [Parameter]
    public QuestionBaseModel<MultipleChoice> MultipleChoiceQuestionModel { get; set; } =
        new QuestionBaseModel<MultipleChoice>(new MultipleChoice());
    [Parameter] public EventCallback<QuestionBaseModel<MultipleChoice>> MultipleChoiceQuestionModelChanged { get; set; }
    
    private class Field
    {
        public string Input { get; set; } = "";
        public IEnumerable<string> SelectedChoices { get; set; } = Enumerable.Empty<string>();
        public MultipleChoice.SubQuestion SubQuestion { get; set; } = new MultipleChoice.SubQuestion();
    }
    
    private List<Field> Fields { get; set; } = new List<Field>();

    protected override void OnInitialized()
    {
        Fields = MultipleChoiceQuestionModel.QuestionContent.SubQuestions.Select( i => new Field()
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
            SubQuestion = new MultipleChoice.SubQuestion()
            {
                Text = "New",
                Choices = new List<MultipleChoice.Choice>()
            }
        });
    }
    
    private async Task CreateSample()
    {
        MultipleChoiceQuestionModel = SampleHelper.GetMultipleChoiceQuestionSample();
        PopulateFields();
        await MultipleChoiceQuestionModelChanged.InvokeAsync(MultipleChoiceQuestionModel);
    }
    
    private async Task Reset()
    {
        MultipleChoiceQuestionModel = new QuestionBaseModel<MultipleChoice>(new MultipleChoice());
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
        Fields = MultipleChoiceQuestionModel.QuestionContent.SubQuestions.Select( i => new Field()
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
            field.SubQuestion.Choices.Add(new MultipleChoice.Choice()
            {
                Text = input
            });
            field.Input = "";
        }
    }
}