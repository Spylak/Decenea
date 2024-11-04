using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Decenea.WebApp.Helpers;

namespace Decenea.WebApp.State;

public class QuestionTypesContainer
{
    private GenericQuestionModel _dragAndDrop;
    private GenericQuestionModel _orderingDnDGenericQuestionModel;
    private GenericQuestionModel _fillBlank;
    private GenericQuestionModel _orderingGenericQuestionModel;
    private GenericQuestionModel _multipleYesOrNo;
    private GenericQuestionModel _multipleChoice;
    private GenericQuestionModel _dropdown;
    private GenericQuestionModel _multipleChoiceSingle;
    private GenericQuestionModel _fillBlankDropdownGenericQuestion;

    public QuestionTypesContainer()
    {
        _dragAndDrop = GenericQuestionModel.ConvertToGenericModel<DragAndDrop>(SampleHelper.GetDragAndDropQuestionSample());
        _orderingDnDGenericQuestionModel = GenericQuestionModel.ConvertToGenericModel<OrderingDragAndDrop>(SampleHelper.GetOrderingDnDQuestionSample());
        _fillBlank = GenericQuestionModel.ConvertToGenericModel<FillBlank>(SampleHelper.GetFillBlankQuestionSample());
        _orderingGenericQuestionModel = GenericQuestionModel.ConvertToGenericModel<Ordering>(SampleHelper.GetOrderingQuestionSample());
        _multipleYesOrNo = GenericQuestionModel.ConvertToGenericModel<MultipleYesOrNo>(SampleHelper.GetMultipleYesOrNoQuestionSample());
        _multipleChoice = GenericQuestionModel.ConvertToGenericModel<MultipleChoice>(SampleHelper.GetMultipleChoiceQuestionSample());
        _dropdown = GenericQuestionModel.ConvertToGenericModel<Dropdown>(SampleHelper.GetDropdownQuestionSample());
        _multipleChoiceSingle = GenericQuestionModel.ConvertToGenericModel<MultipleChoiceSingle>(SampleHelper.GetMultipleChoiceSingleQuestionSample());
        _fillBlankDropdownGenericQuestion = GenericQuestionModel.ConvertToGenericModel<FillBlankDropdown>(SampleHelper.GetFillBlankDropdownQuestionSample());
    }

    public GenericQuestionModel DragAndDrop
    {
        get => _dragAndDrop;
        set => _dragAndDrop = value;
    }

    public GenericQuestionModel OrderingDnDGenericQuestionModel
    {
        get => _orderingDnDGenericQuestionModel;
        set => _orderingDnDGenericQuestionModel = value;
    }

    public GenericQuestionModel FillBlank
    {
        get => _fillBlank;
        set => _fillBlank = value;
    }

    public GenericQuestionModel OrderingGenericQuestionModel
    {
        get => _orderingGenericQuestionModel;
        set => _orderingGenericQuestionModel = value;
    }

    public GenericQuestionModel MultipleYesOrNo
    {
        get => _multipleYesOrNo;
        set => _multipleYesOrNo = value;
    }

    public GenericQuestionModel MultipleChoice
    {
        get => _multipleChoice;
        set => _multipleChoice = value;
    }

    public GenericQuestionModel Dropdown
    {
        get => _dropdown;
        set => _dropdown = value;
    }

    public GenericQuestionModel MultipleChoiceSingle
    {
        get => _multipleChoiceSingle;
        set => _multipleChoiceSingle = value;
    }

    public GenericQuestionModel FillBlankDropdownGenericQuestion
    {
        get => _fillBlankDropdownGenericQuestion;
        set => _fillBlankDropdownGenericQuestion = value;
    }
}