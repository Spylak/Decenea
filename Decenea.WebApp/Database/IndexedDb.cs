using BlazorIDB;
using Microsoft.JSInterop;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Database;

public class IndexedDb(IJSRuntime jsRuntime) : BlazorIndexedDb(jsRuntime)
{
    public readonly IndexedDbTable<GenericQuestionModel> Questions = new(jsRuntime, "questions");
    public readonly IndexedDbTable<Test> Tests = new(jsRuntime, "tests");
    public readonly IndexedDbTable<Test> CompletedTests = new(jsRuntime, "completedtests");
    public readonly IndexedDbTable<Test> UpsertTest = new(jsRuntime, "upserttest");
    public readonly IndexedDbTable<Test> OngoingTest = new(jsRuntime, "ongoingtesttest");
}