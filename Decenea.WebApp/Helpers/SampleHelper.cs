using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Helpers;

public static class SampleHelper
{
    public static GenericQuestionModel<DragAndDrop> GetDragAndDropQuestionSample()
    {
        return new GenericQuestionModel<DragAndDrop>(new DragAndDrop())
        {
            Description =
                "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text.",

            QuestionContent = new DragAndDrop(new List<DragAndDrop.DropZone>()
            {
                new DragAndDrop.DropZone() { Identifier = "1", Name = "Field 1" },
                new DragAndDrop.DropZone() { Identifier = "2", Name = "Field 2" },
                new DragAndDrop.DropZone() { Identifier = "3", Name = "Field 3" },
                new DragAndDrop.DropZone() { Identifier = "4", Name = "Field 4" },
            })
            {
                Choices = new List<DragAndDrop.DropItem>()
                {
                    new DragAndDrop.DropItem() { Name = "Item 1", Selector = "0" },
                    new DragAndDrop.DropItem() { Name = "Item 2", Selector = "0" },
                    new DragAndDrop.DropItem() { Name = "Item 3", Selector = "0" },
                    new DragAndDrop.DropItem() { Name = "Item 4", Selector = "0" },
                    new DragAndDrop.DropItem() { Name = "Item 5", Selector = "0" },
                },
            }
        };
    }

    public static GenericQuestionModel<Dropdown> GetDropdownQuestionSample()
    {
        return new GenericQuestionModel<Dropdown>(new Dropdown())
        {
            Description  = "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text.",
            QuestionContent = new Dropdown
            {
                SubQuestions = new List<Dropdown.SubQuestion>()
                {
                    new Dropdown.SubQuestion()
                    {
                        Text = "First one here",
                        Choices = new List<Dropdown.Choice>()
                        {
                            new Dropdown.Choice() { Text = "One", Checked = false },
                            new Dropdown.Choice() { Text = "Two!", Checked = false },
                            new Dropdown.Choice() { Text = "Three!", Checked = false },
                            new Dropdown.Choice() { Text = "Four!", Checked = false }
                        }
                    },
                    new Dropdown.SubQuestion()
                    {
                        Text = "Second one here",
                        Choices = new List<Dropdown.Choice>()
                        {
                            new Dropdown.Choice() { Text = "One", Checked = false },
                            new Dropdown.Choice() { Text = "Two", Checked = false },
                            new Dropdown.Choice() { Text = "Three", Checked = false }
                        }
                    }
                }
            }
        };
    }

    public static GenericQuestionModel<Ordering> GetOrderingQuestionSample()
    {
        return new GenericQuestionModel<Ordering>(new Ordering())
        {
            Description = "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text.",
            QuestionContent = new Ordering()
            {
                Choices = new List<Ordering.Choice>()
                {
                    new Ordering.Choice() { Text = "First one", Order = 0, Active = false },
                    new Ordering.Choice() { Text = "Second one", Order = 1, Active = false },
                    new Ordering.Choice() { Text = "Third one", Order = 2, Active = false },
                    new Ordering.Choice() { Text = "Forth one", Order = 3, Active = false },
                    new Ordering.Choice() { Text = "Fifth one", Order = 4, Active = false }
                },
            }
        };
    }

    public static GenericQuestionModel<FillBlankDropdown> GetFillBlankDropdownQuestionSample()
    {
        var question = new GenericQuestionModel<FillBlankDropdown>(new FillBlankDropdown())
        {
            Description = "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text.",
            QuestionContent = new FillBlankDropdown()
            {
                Options = new List<FillBlankDropdown.SpaceOption>
                {
                    new FillBlankDropdown.SpaceOption()
                    {
                        SpaceNo = 0,
                        Choices = new List<FillBlankDropdown.Choice>()
                        {
                            new FillBlankDropdown.Choice() { Text = "One", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Two!", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Three!", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Four!", Checked = false }
                        }
                    },
                    new FillBlankDropdown.SpaceOption()
                    {
                        SpaceNo = 1,
                        Choices = new List<FillBlankDropdown.Choice>()
                        {
                            new FillBlankDropdown.Choice() { Text = "One", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Two", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Three", Checked = false }
                        }
                    }
                }
            }
        };
        return question;
    }

    public static GenericQuestionModel<FillBlank> GetFillBlankQuestionSample()
    {
        return new GenericQuestionModel<FillBlank>(new FillBlank())
        {
            Description = 
                "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of(The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum,  comes from a line in section 1.10.32.The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested. Sections 1.10.32 and 1.10.33 fromby Cicero are also reproduced in their exact original form, accompanied by English versions from the 1914 translation by H. Rackham."
        };
    }

    public static GenericQuestionModel<MultipleChoiceSingle> GetMultipleChoiceSingleQuestionSample()
    {
        return new GenericQuestionModel<MultipleChoiceSingle>(new MultipleChoiceSingle())
        {
            QuestionContent = new MultipleChoiceSingle()
            {
                SubQuestions = new List<MultipleChoiceSingle.SubQuestion>()
                {
                    new MultipleChoiceSingle.SubQuestion()
                    {
                        Text = "First one here",
                        Picked = "One!",
                        Choices = new List<string>()
                        {
                            "One!", "Two!", "Three!", "Four!"
                        }
                    },
                    new MultipleChoiceSingle.SubQuestion()
                    {
                        Text = "First one here",
                        Picked = "One",
                        Choices = new List<string>()
                        {
                            "One", "Two", "Three"
                        }
                    }
                },
            },
            Description = 
                "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text."
        };
    }

    public static GenericQuestionModel<MultipleYesOrNo> GetMultipleYesOrNoQuestionSample()
    {
        return new GenericQuestionModel<MultipleYesOrNo>(new MultipleYesOrNo())
        {
            QuestionContent = new MultipleYesOrNo()
            {
                SubQuestions = new List<MultipleYesOrNo.SubQuestion>()
                {
                    new MultipleYesOrNo.SubQuestion() { Text = "First one", Checked = false },
                    new MultipleYesOrNo.SubQuestion() { Text = "Second one", Checked = false },
                },
            },
            Description = 
                "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text."
        };
    }

    public static GenericQuestionModel<MultipleChoice> GetMultipleChoiceQuestionSample()
    {
        return new GenericQuestionModel<MultipleChoice>(new MultipleChoice())
        {
            QuestionContent = new MultipleChoice()
            {
                SubQuestions = new List<MultipleChoice.SubQuestion>()
                {
                    new MultipleChoice.SubQuestion()
                    {
                        Text = "Subquestion One",
                        Choices = new List<MultipleChoice.Choice>()
                        {
                            new MultipleChoice.Choice() { Text = "First Choice", Checked = false },
                            new MultipleChoice.Choice() { Text = "Second Choice", Checked = false },
                            new MultipleChoice.Choice() { Text = "Third Choice", Checked = false },
                            new MultipleChoice.Choice() { Text = "Forth Choice", Checked = false }
                        }
                    },
                    new MultipleChoice.SubQuestion()
                    {
                        Text = "Subquestion Two",
                        Choices = new List<MultipleChoice.Choice>()
                        {
                            new MultipleChoice.Choice() { Text = "First Choice", Checked = false },
                            new MultipleChoice.Choice() { Text = "Second Choice", Checked = false },
                            new MultipleChoice.Choice() { Text = "Third Choice", Checked = false }
                        }
                    },
                },
            },
            Description = "What is your _____ , how old are you _____ ?Contrary to popular belief, Lorem Ipsum is not simply random text."
        };
    }

    public static GenericQuestionModel<OrderingDragAndDrop> GetOrderingDnDQuestionSample()
    {
        return new GenericQuestionModel<OrderingDragAndDrop>(new OrderingDragAndDrop())
        {
            QuestionContent = new OrderingDragAndDrop()
            {
                Choices = new List<OrderingDragAndDrop.DropItem>()
                {
                    new OrderingDragAndDrop.DropItem(1) { Name = "Item 1", Selector = "0" },
                    new OrderingDragAndDrop.DropItem(2) { Name = "Item 2", Selector = "0" },
                    new OrderingDragAndDrop.DropItem(3) { Name = "Item 3", Selector = "0" },
                    new OrderingDragAndDrop.DropItem(4) { Name = "Item 4", Selector = "0" },
                    new OrderingDragAndDrop.DropItem(5) { Name = "Item 5", Selector = "0" },
                },
                AnswerZone = new OrderingDragAndDrop.DropZone()
                {
                    Identifier = "1", Name = "Answer"
                },
                OptionZone = new OrderingDragAndDrop.DropZone()
                {
                    Identifier = "0", Name = "Choices"
                }
            },
            Description = "What is your _____ , how old are you _____ ?Contrary to popular belief _____, Lorem Ipsum is not simply random text.",
        };
    }
}