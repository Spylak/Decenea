using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleYesOrNoQuestion
{
    [Parameter] public GenericQuestionModel? MultipleYesOrNoQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> MultipleYesOrNoQuestionBaseModelChanged { get; set; }

    private GenericQuestionModel<MultipleYesOrNo> MultipleYesOrNoQuestionModel { get; set; } = InitializeGenericQuestionModel();
    
    protected override void OnParametersSet()
    {
        MultipleYesOrNoQuestionModel = GenericQuestionModel
            .ConvertToGenericModel<MultipleYesOrNo>(MultipleYesOrNoQuestionBaseModel ?? InitializeGenericQuestionModel());
        PopulateFields();
    }

    private class Field
    {
        public string Input { get; set; } = "";

        public MultipleYesOrNo.SubQuestion SubQuestion { get; set; } = new();
    }

    private List<Field> Fields { get; set; } = [];

    protected override void OnInitialized()
    {
        Fields = MultipleYesOrNoQuestionModel.QuestionContent.SubQuestions.Select(i => new Field()
        {
            Input = "",
            SubQuestion = i
        }).ToList();
    }

    private async Task CreateSample()
    {
        MultipleYesOrNoQuestionModel = SampleHelper.GetMultipleYesOrNoQuestionSample();
        PopulateFields();
        await MultipleYesOrNoQuestionBaseModelChanged.InvokeAsync(
            GenericQuestionModel.ConvertToNonGenericModel(MultipleYesOrNoQuestionModel));
    }

    private static GenericQuestionModel<MultipleYesOrNo> InitializeGenericQuestionModel()
    {
        return new GenericQuestionModel<MultipleYesOrNo>(new MultipleYesOrNo())
        {
            QuestionType = QuestionType.MultipleYesOrNo
        };
    }
    
    private void Reset()
    {
        MultipleYesOrNoQuestionModel = InitializeGenericQuestionModel();
        PopulateFields();
    }

    private async Task ChoiceChanged()
    {
        await MultipleYesOrNoQuestionBaseModelChanged.InvokeAsync(
            GenericQuestionModel.ConvertToNonGenericModel(MultipleYesOrNoQuestionModel));
    }

    private void PopulateFields()
    {
        Fields = MultipleYesOrNoQuestionModel.QuestionContent.SubQuestions.Select(i => new Field()
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
            SubQuestion = new MultipleYesOrNo.SubQuestion()
            {
                Text = "New",
                Checked = false
            }
        });
    }

    private void RemoveField(Field field)
    {
        Fields.Remove(field);
    }
}