using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleYesOrNoQuestion
{
    [Parameter]
    public QuestionBaseModel<MultipleYesOrNo> MultipleYesOrNoQuestionModel { get; set; } =
        new QuestionBaseModel<MultipleYesOrNo>(new MultipleYesOrNo());
    [Parameter] public EventCallback<QuestionBaseModel<MultipleYesOrNo>> MultipleYesOrNoQuestionModelChanged { get; set; }
    
    private class Field
    {
        public string Input { get; set; } = "";

        public MultipleYesOrNo.SubQuestion SubQuestion { get; set; } = new MultipleYesOrNo.SubQuestion();
    }

    private List<Field> Fields { get; set; } = new List<Field>();

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
        await MultipleYesOrNoQuestionModelChanged.InvokeAsync(MultipleYesOrNoQuestionModel);
    }
    
    private async Task Reset()
    {
        MultipleYesOrNoQuestionModel = new QuestionBaseModel<MultipleYesOrNo>(new MultipleYesOrNo());
        PopulateFields();
        await MultipleYesOrNoQuestionModelChanged.InvokeAsync(MultipleYesOrNoQuestionModel);
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