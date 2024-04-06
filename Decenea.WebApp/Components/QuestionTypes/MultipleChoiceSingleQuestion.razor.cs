using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleChoiceSingleQuestion
{
    [Parameter]
    public QuestionBaseModel? MultipleChoiceSingleQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<QuestionBaseModel> MultipleChoiceSingleQuestionBaseModelChanged { get; set; }
    private QuestionBaseModel<MultipleChoiceSingle>? MultipleChoiceSingleQuestionModel { get; set; }
    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        if (MultipleChoiceSingleQuestionBaseModel is not null)
        {
            MultipleChoiceSingleQuestionModel = QuestionBaseModel.ConvertToGeneric<MultipleChoiceSingle>(MultipleChoiceSingleQuestionBaseModel);
            Fields = MultipleChoiceSingleQuestionModel.QuestionContent?.SubQuestions.Select(i => new Field()
            {
                Input = "",
                SubQuestion = i
            }).ToList() ?? new List<Field>();
        }
    }
    private class Field
    {
        public string Input { get; set; } = "";
        public IEnumerable<string> SelectedChoices { get; set; } = Enumerable.Empty<string>();

        public MultipleChoiceSingle.SubQuestion SubQuestion { get; set; } = new MultipleChoiceSingle.SubQuestion();
    }

    private List<Field> Fields { get; set; } = new List<Field>();
    
    private async Task CreateSample()
    {
        MultipleChoiceSingleQuestionModel = SampleHelper.GetMultipleChoiceSingleQuestionSample();
        PopulateFields();
        await MultipleChoiceSingleQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGeneric(MultipleChoiceSingleQuestionModel));
    }
    
    private async Task Reset()
    {
        MultipleChoiceSingleQuestionModel = new QuestionBaseModel<MultipleChoiceSingle>(new MultipleChoiceSingle());
        PopulateFields();
        await MultipleChoiceSingleQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGeneric(MultipleChoiceSingleQuestionModel));
    }

    private void RemoveChoices(Field item)
    {
        var remaining = item.SubQuestion.Choices.Except(item.SelectedChoices);
        item.SubQuestion.Choices = remaining.ToList();
        item.SelectedChoices = new List<string>();
    }

    private void PopulateFields()
    {
        if(MultipleChoiceSingleQuestionModel?.QuestionContent is null)
            return;
        
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