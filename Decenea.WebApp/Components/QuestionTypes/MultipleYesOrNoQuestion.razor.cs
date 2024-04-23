using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleYesOrNoQuestion
{
    [Parameter]
    public QuestionBaseModel? MultipleYesOrNoQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<QuestionBaseModel> MultipleYesOrNoQuestionBaseModelChanged { get; set; }
    private QuestionBaseModel<MultipleYesOrNo>? MultipleYesOrNoQuestionModel { get; set; }
    protected override void OnParametersSet()
    {  
        if (MultipleYesOrNoQuestionBaseModel is not null)
        {
            MultipleYesOrNoQuestionModel = QuestionBaseModel.ConvertToGenericBaseModel<MultipleYesOrNo>(MultipleYesOrNoQuestionBaseModel);
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
        await MultipleYesOrNoQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGenericBaseModel(MultipleYesOrNoQuestionModel));
    }
    
    private void Reset()
    {
        MultipleYesOrNoQuestionModel = new QuestionBaseModel<MultipleYesOrNo>(new MultipleYesOrNo());
        MultipleYesOrNoQuestionModel.Id = MultipleYesOrNoQuestionBaseModel?.Id ?? Guid.NewGuid().ToString();
        PopulateFields();
    }

    private async Task ChoiceChanged()
    {
        await MultipleYesOrNoQuestionBaseModelChanged.InvokeAsync(QuestionBaseModel.ConvertToNonGenericBaseModel(MultipleYesOrNoQuestionModel));
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