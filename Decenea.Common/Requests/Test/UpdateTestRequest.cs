using Decenea.Common.Requests.Common;

namespace Decenea.Common.Requests.Test;

public class UpdateTestRequest : UpdateRequest
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
}