using Decenea.WebApp.Database;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.State;

public class TestContainer
{
    private readonly IndexedDb _indexedDb;
    private Test _upsertTest;
    private Test? _onGoingTest;
    public TestContainer(IndexedDb indexedDb)
    {
        _upsertTest = new Test();
        _indexedDb = indexedDb;
    }
    public async Task UpsertTestToIndexedDb(Test? test)
    {
        if (test is null)
        {
            return;
        }
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
        get => _upsertTest;
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
    public Test? OngoingTest
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
    public Test? GetOnGoingTestTest()
    {
        return _onGoingTest;
    }
    public event Action? StateHasChanged;

    private void NotifyStateChanged() => StateHasChanged?.Invoke();
}