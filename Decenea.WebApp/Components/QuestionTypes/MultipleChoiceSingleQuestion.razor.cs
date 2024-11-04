using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleChoiceSingleQuestion
{
    [Parameter]
    public GenericQuestionModel? MultipleChoiceSingleQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> MultipleChoiceSingleQuestionBaseModelChanged { get; set; }

    private GenericQuestionModel<MultipleChoiceSingle> MultipleChoiceSingleQuestionModel { get; set; } = InitializeGenericQuestionModel();
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        MultipleChoiceSingleQuestionModel = GenericQuestionModel.ConvertToGenericModel<MultipleChoiceSingle>(MultipleChoiceSingleQuestionBaseModel ?? InitializeGenericQuestionModel());
        PopulateFields();
    }

    private class Field
    {
        public string Input { get; set; } = "";
        public IEnumerable<string> SelectedChoices { get; set; } = [];

        public MultipleChoiceSingle.SubQuestion SubQuestion { get; set; } = new ();
    }

    private List<Field> Fields { get; set; } = [];
    
    private async Task CreateSample()
    {
        MultipleChoiceSingleQuestionModel = SampleHelper.GetMultipleChoiceSingleQuestionSample();
        PopulateFields();
        await MultipleChoiceSingleQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(MultipleChoiceSingleQuestionModel));
    }

    private static GenericQuestionModel<MultipleChoiceSingle> InitializeGenericQuestionModel()
    {
        return new GenericQuestionModel<MultipleChoiceSingle>(new MultipleChoiceSingle())
        {
            QuestionType = QuestionType.MultipleChoiceSingle
        };
    }
    
    private void Reset()
    {
        MultipleChoiceSingleQuestionModel = InitializeGenericQuestionModel();
        PopulateFields();
    }

    private async Task RemoveChoices(Field item)
    {
        var remaining = item.SubQuestion.Choices.Except(item.SelectedChoices);
        item.SubQuestion.Choices = remaining.ToList();
        item.SelectedChoices = new List<string>();
        await MultipleChoiceSingleQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(MultipleChoiceSingleQuestionModel));
    }

    private async Task ChoiceChanged()
    {
        MultipleChoiceSingleQuestionModel.QuestionContent.SubQuestions = Fields.Select(i => i.SubQuestion).ToList();
        await MultipleChoiceSingleQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(MultipleChoiceSingleQuestionModel));
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