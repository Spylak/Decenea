using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleChoiceSingle
{
    [Parameter]
    public MultipleChoiceSingleQuestionModel MultipleChoiceSingleQuestionModel { get; set; } = new MultipleChoiceSingleQuestionModel();
    [Parameter] public EventCallback<MultipleChoiceSingleQuestionModel> MultipleChoiceSingleQuestionModelChanged { get; set; }
    
    private class Field
    {
        public string Input { get; set; } = "";
        public IEnumerable<string> SelectedChoices { get; set; } = new List<string>();

        public MultipleChoiceSingleQuestionModel.SubQuestion SubQuestion { get; set; } =
            new MultipleChoiceSingleQuestionModel.SubQuestion();
    }

    private List<Field> Fields { get; set; } = new List<MultipleChoiceSingle.Field>();

    protected override void OnInitialized()
    {
        Fields = MultipleChoiceSingleQuestionModel.SubQuestions.Select(i => new Field()
        {
            Input = "",
            SubQuestion = i
        }).ToList();
    }
    
    private async Task CreateSample()
    {
        MultipleChoiceSingleQuestionModel = SampleService.GetMultipleChoiceSingleQuestionSample();
        PopulateFields();
        await MultipleChoiceSingleQuestionModelChanged.InvokeAsync(MultipleChoiceSingleQuestionModel);
    }
    
    private async Task Reset()
    {
        MultipleChoiceSingleQuestionModel = new MultipleChoiceSingleQuestionModel();
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
        Fields = MultipleChoiceSingleQuestionModel.SubQuestions.Select(i => new Field()
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
            SubQuestion = new MultipleChoiceSingleQuestionModel.SubQuestion()
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