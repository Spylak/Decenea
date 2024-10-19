using Decenea.Common.Enums;
using Decenea.WebApp.Helpers;
using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Components.QuestionTypes;


public partial class FillBlankQuestion
{

    [Parameter]
    public GenericQuestionModel? FillBlankQuestionBaseModel { get; set; }
    [Parameter] public EventCallback<GenericQuestionModel> FillBlankQuestionBaseModelChanged { get; set; }
    private GenericQuestionModel<FillBlank> FillBlankQuestionModel { get; set; } = InitializeGenericQuestionModel();
    private string DynamicQuestion { get; set; } = "";
    private List<FillBlank.SpaceOption> Fields { get; set; } = [];
    private string SpecialChars { get; set; } = "_____";
    private List<string> QuestionText { get; set; } = [];
    private int ActiveFieldNumber { get; set; } = 0;
    
    protected override void OnParametersSet()
    {
        FillBlankQuestionModel = GenericQuestionModel.ConvertToGenericModel<FillBlank>(FillBlankQuestionBaseModel ?? InitializeGenericQuestionModel());
        Fields = FillBlankQuestionModel.QuestionContent.Options;
        UpdateOptions(FillBlankQuestionModel.Description);
        PopulateDynamicQuestion();
    }
    
    private void UpdateOptions(string question)
    {
        FillBlankQuestionModel.Description = question;
        FillBlankQuestionModel.QuestionContent.Options = new List<FillBlank.SpaceOption>();
        QuestionText = [];
        QuestionText = FillBlankQuestionModel.Description.Split(SpecialChars).ToList();
        for (int i = 0; i < QuestionText.Count() - 1; i++)
        {
            if (Fields.Count <= i)
            {
                Fields.Add(new FillBlank.SpaceOption()
                {
                    SpaceNo = i,
                    Text = ""
                });
            }
            FillBlankQuestionModel.QuestionContent.Options.Add(Fields[i]);
        }
        StateHasChanged();
    }

    private void RemoveField(FillBlank.SpaceOption field)
    {
        if (Fields.Count > QuestionText.Count() - 1)
        {
            Fields.Remove(field);
        }
    }
    
    private void AddNewField()
    {
        var max = Fields.Count != 0 ? Fields.Max(i => i.SpaceNo) : 0;
        Fields.Add( new FillBlank.SpaceOption() { SpaceNo = max + 1, Text = "" });
    }
    
    private async Task CreateSample()
    {
        FillBlankQuestionModel = SampleHelper.GetFillBlankQuestionSample();
        Fields = FillBlankQuestionModel.QuestionContent.Options;
        UpdateOptions(FillBlankQuestionModel.Description);
        PopulateDynamicQuestion();
        await FillBlankQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(FillBlankQuestionModel));
    }

    private static GenericQuestionModel<FillBlank> InitializeGenericQuestionModel()
    {
        return new GenericQuestionModel<FillBlank>(new FillBlank())
        {
            QuestionType = QuestionType.FillBlank
        };
    }
    
    private void Reset()
    {
        FillBlankQuestionModel = InitializeGenericQuestionModel();
        Fields = FillBlankQuestionModel.QuestionContent.Options;
        UpdateOptions(FillBlankQuestionModel.Description);
        PopulateDynamicQuestion();
    }

    private void PopulateDynamicQuestion()
    {
        DynamicQuestion = "";
        QuestionText = new List<string>();
        QuestionText = FillBlankQuestionModel.Description.Split(SpecialChars).ToList();
        for (var i=0;i<QuestionText.Count - 1;i++)
        {
            var choice = FillBlankQuestionModel
                .QuestionContent
                .Options?
                .FirstOrDefault(e => e.SpaceNo == i)?
                .Text;
            choice = choice !="" ? choice : $"_____{i+1}";
            DynamicQuestion += (QuestionText[i]+$"\u27A1\u27A1 {choice } \u2B05\u2B05");
        }
        DynamicQuestion += QuestionText[^1];
    }
    
    private async Task OnValueChange(string args,int j)
    {
        var option = FillBlankQuestionModel.QuestionContent.Options.FirstOrDefault(i => i.SpaceNo == j);
        if (option is not null)
            option.Text = args;
        var field =Fields.FirstOrDefault(i => i.SpaceNo == j);
        if (field is null)
            return;
        field.Text = args;
        PopulateDynamicQuestion();
        await FillBlankQuestionBaseModelChanged.InvokeAsync(GenericQuestionModel.ConvertToNonGenericModel(FillBlankQuestionModel));
    }
}