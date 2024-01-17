using BlazorIDB;
using Microsoft.JSInterop;
using Decenea.WebApp.Models;
using Decenea.WebApp.Models.QuestionTypes;

namespace Decenea.WebApp.Database;

public class IndexedDb : BlazorIndexedDb
{
    public readonly IndexedDbTable<object> Questions;
    public readonly IndexedDbTable<Test> Tests;
    public readonly IndexedDbTable<Test> CompletedTests;
    public readonly IndexedDbTable<Test> UpsertTest;
    public readonly IndexedDbTable<Test> OngoingTest;

    
    public IndexedDb(IJSRuntime jsRuntime) : base(jsRuntime)
    {
        Questions =new IndexedDbTable<object>(jsRuntime , "questions");
        Tests = new IndexedDbTable<Test>(jsRuntime, "tests");
        CompletedTests = new IndexedDbTable<Test>(jsRuntime, "completedtests");
        UpsertTest = new IndexedDbTable<Test>(jsRuntime, "upserttest");
        OngoingTest = new IndexedDbTable<Test>(jsRuntime, "ongoingtesttest");
    }
}