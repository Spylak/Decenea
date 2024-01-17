using Decenea.WebApp.Database;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.State;

public class TestContainer
{
    private IndexedDb _indexedDb { get; set; }
    private Test _upsertTest { get; set; }
    private Test _onGoingTest { get; set; }
    public TestContainer(IndexedDb indexedDb)
    {
        _upsertTest = new Test();
        _onGoingTest = new Test();
        _indexedDb = indexedDb;
    }
    public async Task UpsertTestToIndexedDb(Test test)
    {
        var testResult = await _indexedDb.Tests.GetByIdAsync(test.Id);
        if (testResult.Message == "There are no entities in the table." ||
            testResult.Message == "No entity with that id.")
        {
            await _indexedDb.Tests.AddAsync(test);
        }
        else
        {
            await _indexedDb.Tests.UpdateAsync(test);
        }
    }
    public Test UpsertTest
    {
        get
        {
            return _upsertTest;
        }
        set
        {
            _upsertTest = value;
            NotifyStateChanged();
        }
    }
    public Test GetUpsertTest()
    {
        return _upsertTest;
    }
    public Test OngoingTest
    {
        get
        {
            return _onGoingTest;
        }
        set
        {
            _onGoingTest = value;
            NotifyStateChanged();
        }
    }
    public Test GetOnGoingTestTest()
    {
        return _onGoingTest;
    }
    public event Action? StateHasChanged;

    private void NotifyStateChanged() => StateHasChanged?.Invoke();
}