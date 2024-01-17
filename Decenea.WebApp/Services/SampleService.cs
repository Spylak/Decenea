using Decenea.WebApp.Models.QuestionTypes;
using Decenea.WebApp.Services.IService;

namespace Decenea.WebApp.Services;

public class SampleService : ISampleService
{
    private readonly IGlobalFunctionService _globalFunctionService;
    public SampleService(IGlobalFunctionService globalFunctionService)
    {
        _globalFunctionService = globalFunctionService;
    }
    public DragAndDropQuestionModel GetDragAndDropQuestionSample()
    {
        return new DragAndDropQuestionModel(dropzones:new List<DragAndDropQuestionModel.DropZone>()
        {
            new DragAndDropQuestionModel.DropZone(){Identifier = "1",Name = "Field 1"},
            new DragAndDropQuestionModel.DropZone(){Identifier = "2",Name = "Field 2"},
            new DragAndDropQuestionModel.DropZone(){Identifier = "3",Name = "Field 3"},
            new DragAndDropQuestionModel.DropZone(){Identifier = "4",Name = "Field 4"},
        })
        {
            Question = "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text.",
            Choices = new()
            {
                new DragAndDropQuestionModel.DropItem() {Name = "Item 1", Selector = "0"},
                new DragAndDropQuestionModel.DropItem() {Name = "Item 2", Selector = "0"},
                new DragAndDropQuestionModel.DropItem() {Name = "Item 3", Selector = "0"},
                new DragAndDropQuestionModel.DropItem() {Name = "Item 4", Selector = "0"},
                new DragAndDropQuestionModel.DropItem() {Name = "Item 5", Selector = "0"},
            },
        };
    }

    public DropdownQuestionModel GetDropdownQuestionSample()
    {
        return new DropdownQuestionModel()
    {
        SubQuestions = new List<DropdownQuestionModel.SubQuestion>()
        {
            new DropdownQuestionModel.SubQuestion()
            {
                Text = "First one here",
                Choices = new List<DropdownQuestionModel.Choice>()
                {
                    new DropdownQuestionModel.Choice() {Text = "One", Checked = false},
                    new DropdownQuestionModel.Choice() {Text = "Two!", Checked = false},
                    new DropdownQuestionModel.Choice() {Text = "Three!", Checked = false},
                    new DropdownQuestionModel.Choice() {Text = "Four!", Checked = false}
                }
            },
            new DropdownQuestionModel.SubQuestion()
            {
                Text = "Second one here",
                Choices = new List<DropdownQuestionModel.Choice>()
                {
                    new DropdownQuestionModel.Choice() {Text = "One", Checked = false},
                    new DropdownQuestionModel.Choice() {Text = "Two", Checked = false},
                    new DropdownQuestionModel.Choice() {Text = "Three", Checked = false}
                }
            }
        },
        Question = "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text."
    };
    }

    public OrderingQuestionModel GetOrderingQuestionSample()
    {
        return  new OrderingQuestionModel()
        {
            Choices = new List<OrderingQuestionModel.Choice>()
            {
                new OrderingQuestionModel.Choice() {Text = "First one", Order = 0, Active = false},
                new OrderingQuestionModel.Choice() {Text = "Second one", Order = 1, Active = false},
                new OrderingQuestionModel.Choice() {Text = "Third one", Order = 2, Active = false},
                new OrderingQuestionModel.Choice() {Text = "Forth one", Order = 3, Active = false},
                new OrderingQuestionModel.Choice() {Text = "Fifth one", Order = 4, Active = false}
            },
            Question = "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text."
        };
    }

    public FillBlankDropdownQuestionModel GetFillBlankDropdownQuestionSample()
    {
        var question = new FillBlankDropdownQuestionModel();
        question.Options = new List<FillBlankDropdownQuestionModel.SpaceOption>();
        question.Options.Add( new FillBlankDropdownQuestionModel.SpaceOption()
        {
            SpaceNo = 0,
            Choices = new List<FillBlankDropdownQuestionModel.Choice>()
            {
                new FillBlankDropdownQuestionModel.Choice() { Text = "One", Checked = false },
                new FillBlankDropdownQuestionModel.Choice() { Text = "Two!", Checked = false },
                new FillBlankDropdownQuestionModel.Choice() { Text = "Three!", Checked = false },
                new FillBlankDropdownQuestionModel.Choice() { Text = "Four!", Checked = false }
            }
        });
        question.Options.Add(new FillBlankDropdownQuestionModel.SpaceOption()
        {
            SpaceNo = 1,
            Choices = new List<FillBlankDropdownQuestionModel.Choice>()
            {
                new FillBlankDropdownQuestionModel.Choice() { Text = "One", Checked = false },
                new FillBlankDropdownQuestionModel.Choice() { Text = "Two", Checked = false },
                new FillBlankDropdownQuestionModel.Choice() { Text = "Three", Checked = false }
            }
        });
        question.Question ="What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text.";
        return question;
    }

    public FillBlankQuestionModel GetFillBlankQuestionSample()
    {
        return new FillBlankQuestionModel()
        {
            Question =
                "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of(The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum,  comes from a line in section 1.10.32.The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 fromby Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham."
        };
    }

    public MultipleChoiceSingleQuestionModel GetMultipleChoiceSingleQuestionSample()
    {
        return new MultipleChoiceSingleQuestionModel()
        {
            SubQuestions = new List<MultipleChoiceSingleQuestionModel.SubQuestion>()
            {
                new MultipleChoiceSingleQuestionModel.SubQuestion()
                {
                    Text = "First one here",
                    Picked = "One!",
                    Choices = new List<string>()
                    {
                        "One!","Two!","Three!","Four!"
                    }
                },
                new MultipleChoiceSingleQuestionModel.SubQuestion()
                {
                    Text = "First one here",
                    Picked = "One",
                    Choices = new List<string>()
                    {
                        "One","Two","Three"
                    }
                }
            },
            Question = "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text."
        };
    }

    public MultipleYesOrNoQuestionModel GetMultipleYesOrNoQuestionSample()
    {
        return new MultipleYesOrNoQuestionModel()
        {
            SubQuestions = new List<MultipleYesOrNoQuestionModel.SubQuestion>()
            {
                new MultipleYesOrNoQuestionModel.SubQuestion() {Text = "First one", Checked = false},
                new MultipleYesOrNoQuestionModel.SubQuestion() {Text = "Second one", Checked = false},
            },
            Question = "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text."
        };
    }

    public MultipleChoiceQuestionModel GetMultipleChoiceQuestionSample()
    {
        return new MultipleChoiceQuestionModel()
    {
        SubQuestions = new List<MultipleChoiceQuestionModel.SubQuestion>()
        {
            new MultipleChoiceQuestionModel.SubQuestion()
            {
                Text = "Subquestion One",
                Choices = new List<MultipleChoiceQuestionModel.Choice>()
                {
                    new MultipleChoiceQuestionModel.Choice() {Text = "First Choice", Checked = false},
                    new MultipleChoiceQuestionModel.Choice() {Text = "Second Choice", Checked = false},
                    new MultipleChoiceQuestionModel.Choice() {Text = "Third Choice", Checked = false},
                    new MultipleChoiceQuestionModel.Choice() {Text = "Forth Choice", Checked = false}
                }
            },
            new MultipleChoiceQuestionModel.SubQuestion()
            {
                Text = "Subquestion Two",
                Choices = new List<MultipleChoiceQuestionModel.Choice>()
                {
                    new MultipleChoiceQuestionModel.Choice() {Text = "First Choice", Checked = false},
                    new MultipleChoiceQuestionModel.Choice() {Text = "Second Choice", Checked = false},
                    new MultipleChoiceQuestionModel.Choice() {Text = "Third Choice", Checked = false}
                }
            },
        },
        Question = "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text."
    };
    }

    public OrderingDnDQuestionModel GetOrderingDnDQuestionSample()
    {
        return new OrderingDnDQuestionModel()
        {
            Question = "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text.",
            Choices = new()
            {
                new OrderingDnDQuestionModel.DropItem(1) {Name = "Item 1", Selector = "0"},
                new OrderingDnDQuestionModel.DropItem(2) {Name = "Item 2", Selector = "0"},
                new OrderingDnDQuestionModel.DropItem(3) {Name = "Item 3", Selector = "0"},
                new OrderingDnDQuestionModel.DropItem(4) {Name = "Item 4", Selector = "0"},
                new OrderingDnDQuestionModel.DropItem(5) {Name = "Item 5", Selector = "0"},
            },
            AnswerZone = new OrderingDnDQuestionModel.DropZone()
            {
                Identifier = "1",Name = "Answer"
            },
            OptionZone = new OrderingDnDQuestionModel.DropZone()
            {
                Identifier = "0",Name = "Choices"
            }
        };
    }
}