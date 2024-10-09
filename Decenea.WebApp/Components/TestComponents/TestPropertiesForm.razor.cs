using Microsoft.AspNetCore.Components;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Components.TestComponents;

public partial class TestPropertiesForm
{
    [Parameter] public TestModel? Test { get; set; }
    [Parameter] public bool ReadOnly { get; set; }
}