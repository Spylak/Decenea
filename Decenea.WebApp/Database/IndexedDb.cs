using BlazorIDB;
using Decenea.Common.DataTransferObjects.Question.QuestionTypes;
using Microsoft.JSInterop;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Database;

public class IndexedDb(IJSRuntime jsRuntime) : BlazorIndexedDb(jsRuntime)
{
    public readonly IndexedDbTable<GenericQuestionModel> Questions = new(jsRuntime, "questions");
    public readonly IndexedDbTable<TestModel> Tests = new(jsRuntime, "tests");
    public readonly IndexedDbTable<TestModel> CompletedTests = new(jsRuntime, "completedtests");
    public readonly IndexedDbTable<TestModel> UpsertTest = new(jsRuntime, "upserttest");
    public readonly IndexedDbTable<TestModel> OngoingTest = new(jsRuntime, "ongoingtesttest");
}