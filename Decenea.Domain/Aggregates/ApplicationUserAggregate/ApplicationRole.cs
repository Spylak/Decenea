using Decenea.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Decenea.Domain.Aggregates.ApplicationUserAggregate;

public class ApplicationRole : Entity<int>
{
    public string Name { get; set; }
}