using Decenea.Common.Constants;
using Decenea.WebApp.Helpers;

namespace Decenea.WebApp.Models.QuestionTypes;

public class Ordering
{
    public List<Choice> Choices { get; set; } = new List<Choice>();

    public class Choice : ISortable
    {
        public int Order { get; set; }
        public bool Active { get; set; }
        public string Text { get; set; }
    }
}