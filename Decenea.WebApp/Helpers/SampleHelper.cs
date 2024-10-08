using Decenea.Common.Enums;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Helpers;

public static class SampleHelper
{
    public static GenericQuestionModel<DragAndDrop> GetDragAndDropQuestionSample()
    {
        return new GenericQuestionModel<DragAndDrop>(new DragAndDrop())
        {
            QuestionType = QuestionType.DragAndDrop,
            Description = "Match the historical events with their corresponding dates.",
            QuestionContent = new DragAndDrop()
            {
                DropZones = new Dictionary<int, string>()
                {
                    {1,"1776"},
                    {2,"1492"},
                    {3,"1969"},
                    {4,"1914"},
                },
                Choices =
                [
                    new DragAndDrop.DropItem() { Name = "American Declaration of Independence", Selector = "0" },
                    new DragAndDrop.DropItem() { Name = "Columbus Discovers America", Selector = "0" },
                    new DragAndDrop.DropItem() { Name = "First Moon Landing", Selector = "0" },
                    new DragAndDrop.DropItem() { Name = "Start of World War I", Selector = "0" }
                ],
            }
        };
    }

    public static GenericQuestionModel<Dropdown> GetDropdownQuestionSample()
    {
        return new GenericQuestionModel<Dropdown>(new Dropdown())
        {
            QuestionType = QuestionType.Dropdown,
            Description = "Select the correct answers from the dropdown menus.",
            QuestionContent = new Dropdown
            {
                SubQuestions = new List<Dropdown.SubQuestion>()
                {
                    new Dropdown.SubQuestion()
                    {
                        Text = "Which planet is known as the Red Planet?",
                        Choices = new List<Dropdown.Choice>()
                        {
                            new Dropdown.Choice() { Text = "Earth", Checked = false },
                            new Dropdown.Choice() { Text = "Mars", Checked = false },
                            new Dropdown.Choice() { Text = "Jupiter", Checked = false },
                            new Dropdown.Choice() { Text = "Saturn", Checked = false }
                        }
                    },
                    new Dropdown.SubQuestion()
                    {
                        Text = "Which element has the chemical symbol 'O'?",
                        Choices = new List<Dropdown.Choice>()
                        {
                            new Dropdown.Choice() { Text = "Oxygen", Checked = false },
                            new Dropdown.Choice() { Text = "Gold", Checked = false },
                            new Dropdown.Choice() { Text = "Silver", Checked = false }
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
            QuestionType = QuestionType.Ordering,
            Description = "Arrange the stages of the water cycle in the correct order.",
            QuestionContent = new Ordering()
            {
                Choices = new List<Ordering.Choice>()
                {
                    new Ordering.Choice() { Text = "Evaporation", Order = 0, Active = false },
                    new Ordering.Choice() { Text = "Condensation", Order = 1, Active = false },
                    new Ordering.Choice() { Text = "Precipitation", Order = 2, Active = false },
                    new Ordering.Choice() { Text = "Collection", Order = 3, Active = false },
                },
            }
        };
    }

    public static GenericQuestionModel<FillBlankDropdown> GetFillBlankDropdownQuestionSample()
    {
        var question = new GenericQuestionModel<FillBlankDropdown>(new FillBlankDropdown())
        {
            QuestionType = QuestionType.FillBlankDropdown,
            Description = "What is your given and last _____, what is the _____ of your birth?",
            QuestionContent = new FillBlankDropdown()
            {
                Options = new List<FillBlankDropdown.SpaceOption>
                {
                    new FillBlankDropdown.SpaceOption()
                    {
                        SpaceNo = 0,
                        Choices = new List<FillBlankDropdown.Choice>()
                        {
                            new FillBlankDropdown.Choice() { Text = "Friend", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "School", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Title", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Meal", Checked = false }
                        }
                    },
                    new FillBlankDropdown.SpaceOption()
                    {
                        SpaceNo = 1,
                        Choices = new List<FillBlankDropdown.Choice>()
                        {
                            new FillBlankDropdown.Choice() { Text = "Car", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Year", Checked = false },
                            new FillBlankDropdown.Choice() { Text = "Animal", Checked = false }
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
            QuestionType = QuestionType.FillBlank,
            Description = "The current calendar year is _____."
        };
    }

    public static GenericQuestionModel<MultipleChoiceSingle> GetMultipleChoiceSingleQuestionSample()
    {
        return new GenericQuestionModel<MultipleChoiceSingle>(new MultipleChoiceSingle())
        {
            QuestionType = QuestionType.MultipleChoiceSingle,
            QuestionContent = new MultipleChoiceSingle()
            {
                SubQuestions = new List<MultipleChoiceSingle.SubQuestion>()
                {
                    new MultipleChoiceSingle.SubQuestion()
                    {
                        Text = "Which is the largest planet in our solar system?",
                        Picked = "Jupiter",
                        Choices = new List<string>()
                        {
                            "Earth", "Mars", "Jupiter", "Saturn"
                        }
                    },

                    new MultipleChoiceSingle.SubQuestion()
                    {
                        Text = "What is the chemical symbol for water?",
                        Picked = "H2O",
                        Choices = new List<string>()
                        {
                            "HO2", "H2O", "OH2", "H2"
                        }
                    }
                },
            },
            Description = "Choose the correct option for each question."
        };
    }

    public static GenericQuestionModel<MultipleYesOrNo> GetMultipleYesOrNoQuestionSample()
    {
        return new GenericQuestionModel<MultipleYesOrNo>(new MultipleYesOrNo())
        {
            QuestionType = QuestionType.MultipleYesOrNo,
            QuestionContent = new MultipleYesOrNo()
            {
                SubQuestions = new List<MultipleYesOrNo.SubQuestion>()
                {
                    new MultipleYesOrNo.SubQuestion() { Text = "Is the Earth round?", Checked = false },
                    new MultipleYesOrNo.SubQuestion() { Text = "Is the sky green?", Checked = false }
                },
            },
            Description = "Answer with Yes or No."
        };
    }

    public static GenericQuestionModel<MultipleChoice> GetMultipleChoiceQuestionSample()
    {
        return new GenericQuestionModel<MultipleChoice>(new MultipleChoice())
        {
            QuestionType = QuestionType.MultipleChoice,
            QuestionContent = new MultipleChoice()
            {
                SubQuestions = new List<MultipleChoice.SubQuestion>()
                {
                    new MultipleChoice.SubQuestion()
                    {
                        Text = "Which of the following are programming languages?",
                        Choices = new List<MultipleChoice.Choice>()
                        {
                            new MultipleChoice.Choice() { Text = "Python", Checked = false },
                            new MultipleChoice.Choice() { Text = "HTML", Checked = false },
                            new MultipleChoice.Choice() { Text = "Java", Checked = false },
                            new MultipleChoice.Choice() { Text = "Microsoft Word", Checked = false }
                        }
                    },

                    new MultipleChoice.SubQuestion()
                    {
                        Text = "Which of the following are planets?",
                        Choices = new List<MultipleChoice.Choice>()
                        {
                            new MultipleChoice.Choice() { Text = "Mercury", Checked = false },
                            new MultipleChoice.Choice() { Text = "Venus", Checked = false },
                            new MultipleChoice.Choice() { Text = "Pluto", Checked = false },
                            new MultipleChoice.Choice() { Text = "Sun", Checked = false }
                        }
                    }
                },
            },
            Description = "Select all the correct options."
        };
    }

    public static GenericQuestionModel<OrderingDragAndDrop> GetOrderingDnDQuestionSample()
    {
        return new GenericQuestionModel<OrderingDragAndDrop>(new OrderingDragAndDrop())
        {
            QuestionType = QuestionType.OrderingDragAndDrop,
            QuestionContent = new OrderingDragAndDrop()
            {
                Choices = new List<OrderingDragAndDrop.DropItem>()
                {
                    new OrderingDragAndDrop.DropItem(1) { Name = "Gather Ingredients", Selector = "0" },
                    new OrderingDragAndDrop.DropItem(2) { Name = "Mix Ingredients", Selector = "0" },
                    new OrderingDragAndDrop.DropItem(3) { Name = "Bake at 350°F", Selector = "0" },
                    new OrderingDragAndDrop.DropItem(4) { Name = "Serve", Selector = "0" },
                },
                AnswerZone = new OrderingDragAndDrop.DropZone()
                {
                    Identifier = "1", Name = "Ordered Steps"
                },
                OptionZone = new OrderingDragAndDrop.DropZone()
                {
                    Identifier = "0", Name = "Unordered Steps"
                }
            },
            Description = "Arrange the steps to bake a cake in the correct order."
        };
    }
}
