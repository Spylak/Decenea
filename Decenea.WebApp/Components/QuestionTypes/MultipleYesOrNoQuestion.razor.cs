using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleYesOrNoQuestion
{
    [Parameter]
    public GenericQuestionModel? MultipleYesOrNoQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> MultipleYesOrNoQuestionBaseModelChanged { get; set; }
    private GenericQuestionModel<MultipleYesOrNo>? MultipleYesOrNoQuestionModel { get; set; }
    protected override void OnParametersSet()
    {  
        if (MultipleYesOrNoQuestionBaseModel is not null)
        {
            MultipleYesOrNoQuestionModel = GenericQuestionModel.ConvertToGenericModel<MultipleYesOrNo>(MultipleYesOrNoQuestionBaseModel);
            PopulateFields();
        }
    }

    private class Field
    {
        public string Input { get; set; } = "";

        public MultipleYesOrNo.SubQuestion SubQuestion { get; set; } = new MultipleYesOrNo.SubQuestion();
    }

    private List<Field> Fields { get; set; } = new List<Field>();

    protected override void OnInitialized()
    {
        if(MultipleYesOrNoQuestionModel?.QuestionContent is null)
            return;
        
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
        await MultipleYesOrNoQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(MultipleYesOrNoQuestionModel));
    }
    
    private void Reset()
    {
        MultipleYesOrNoQuestionModel = new GenericQuestionModel<MultipleYesOrNo>(new MultipleYesOrNo())
        {
            Id = MultipleYesOrNoQuestionBaseModel?.Id ?? Ulid.NewUlid().ToString(),
            QuestionType = QuestionType.MultipleYesOrNo
        };
        PopulateFields();
    }

    private async Task ChoiceChanged()
    {
        await MultipleYesOrNoQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(MultipleYesOrNoQuestionModel));
    }
    
    private void PopulateFields()
    {
        if(MultipleYesOrNoQuestionModel?.QuestionContent is null)
            return;
        
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