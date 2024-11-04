namespace Decenea.Common.Helpers;

public class ListHelper
{
    public static void UpdateOrders<T>(T targetScenario, int newOrder, List<T> items) where T : ISortable
    {
        items = items.OrderBy(i => i.Order).ToList();
        bool seen = false;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Text == targetScenario.Text)
            {
                items[i].Order = newOrder;
                seen = true;
            }
            else if (seen)
            {
                if (items[i].Order <= newOrder)
                {
                    items[i].Order--; // move it left
                }
            }
            else
            {
                if (items[i].Order >= newOrder)
                {
                    items[i].Order++; // move it right
                }
            }
        }
    }
}
public interface ISortable
{
    string Text { get; set; }
    int Order { get; set; }
}