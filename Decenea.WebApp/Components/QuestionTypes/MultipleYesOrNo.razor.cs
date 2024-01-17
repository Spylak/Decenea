using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;

public partial class MultipleYesOrNo
{
    [Parameter] public MultipleYesOrNoQuestionModel MultipleYesOrNoQuestionModel { get; set; } = new MultipleYesOrNoQuestionModel();
    [Parameter] public EventCallback<MultipleYesOrNoQuestionModel> MultipleYesOrNoQuestionModelChanged { get; set; }
    
    private class Field
    {
        public string Input { get; set; } = "";

        public MultipleYesOrNoQuestionModel.SubQuestion SubQuestion { get; set; } =
            new MultipleYesOrNoQuestionModel.SubQuestion();
    }

    private List<Field> Fields { get; set; } = new List<Field>();

    protected override void OnInitialized()
    {
        Fields = MultipleYesOrNoQuestionModel.SubQuestions.Select(i => new Field()
        {
            Input = "",
            SubQuestion = i
        }).ToList();
    }
    private async Task CreateSample()
    {
        MultipleYesOrNoQuestionModel = SampleService.GetMultipleYesOrNoQuestionSample();
        PopulateFields();
        await MultipleYesOrNoQuestionModelChanged.InvokeAsync(MultipleYesOrNoQuestionModel);
    }
    
    private async Task Reset()
    {
        MultipleYesOrNoQuestionModel = new MultipleYesOrNoQuestionModel();
        PopulateFields();
        await MultipleYesOrNoQuestionModelChanged.InvokeAsync(MultipleYesOrNoQuestionModel);
    }

    private void PopulateFields()
    {
        Fields = MultipleYesOrNoQuestionModel.SubQuestions.Select(i => new Field()
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
            SubQuestion = new MultipleYesOrNoQuestionModel.SubQuestion()
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