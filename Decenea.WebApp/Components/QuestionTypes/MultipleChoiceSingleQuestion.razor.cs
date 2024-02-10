using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleChoiceSingleQuestion
{
    [Parameter]
    public QuestionBaseModel<MultipleChoiceSingle> MultipleChoiceSingleQuestionModel { get; set; } =
        new QuestionBaseModel<MultipleChoiceSingle>(new MultipleChoiceSingle());
    [Parameter] public EventCallback<QuestionBaseModel<MultipleChoiceSingle>> MultipleChoiceSingleQuestionModelChanged { get; set; }
    
    private class Field
    {
        public string Input { get; set; } = "";
        public IEnumerable<string> SelectedChoices { get; set; } = Enumerable.Empty<string>();

        public MultipleChoiceSingle.SubQuestion SubQuestion { get; set; } = new MultipleChoiceSingle.SubQuestion();
    }

    private List<Field> Fields { get; set; } = new List<Field>();

    protected override void OnInitialized()
    {
        Fields = MultipleChoiceSingleQuestionModel.QuestionContent.SubQuestions.Select(i => new Field()
        {
            Input = "",
            SubQuestion = i
        }).ToList();
    }
    
    private async Task CreateSample()
    {
        MultipleChoiceSingleQuestionModel = SampleHelper.GetMultipleChoiceSingleQuestionSample();
        PopulateFields();
        await MultipleChoiceSingleQuestionModelChanged.InvokeAsync(MultipleChoiceSingleQuestionModel);
    }
    
    private async Task Reset()
    {
        MultipleChoiceSingleQuestionModel = new QuestionBaseModel<MultipleChoiceSingle>(new MultipleChoiceSingle());
        PopulateFields();
        await MultipleChoiceSingleQuestionModelChanged.InvokeAsync(MultipleChoiceSingleQuestionModel);
    }

    private void RemoveChoices(Field item)
    {
        var remaining = item.SubQuestion.Choices.Except(item.SelectedChoices);
        item.SubQuestion.Choices = remaining.ToList();
        item.SelectedChoices = new List<string>();
    }

    private void PopulateFields()
    {
        Fields = MultipleChoiceSingleQuestionModel.QuestionContent.SubQuestions.Select(i => new Field()
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
            SubQuestion = new MultipleChoiceSingle.SubQuestion()
            {
                Text = "New",
                Picked = "",
                Choices = new List<string>()
            }
        });
    }

    private void RemoveField(Field field)
    {
        Fields.Remove(field);
    }

    private void AddChoice(Field field, string input)
    {
        if (!field.SubQuestion.Choices.Contains(input))
        {
            field.SubQuestion.Choices.Add(input);
            field.Input = "";
        }
    }
}