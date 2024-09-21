using Blazored.LocalStorage;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Database;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Decenea.WebApp.Pages.Shared;

public abstract class BasePage : ComponentBase
{
    [Inject] protected IndexedDb IndexedDb { get; set; }
    [Inject] protected NavigationManager NavigationManager { get; set; }
    [Inject] protected ILocalStorageService LocalStorageService { get; set; }
    [Inject] protected IDialogService DialogService { get; set; }
    [Inject] protected IGlobalFunctionService GlobalFunctionService { get; set; }
}