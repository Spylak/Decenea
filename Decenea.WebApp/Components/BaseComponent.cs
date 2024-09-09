using Decenea.WebApp.Abstractions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebApp.Components;

public class BaseComponent : ComponentBase
{
    [Inject] protected IGlobalFunctionService GlobalFunctionService { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
}