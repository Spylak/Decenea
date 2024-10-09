using Decenea.WebApp.Database;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.State;

public class TestContainer
{
    private readonly IndexedDb _indexedDb;
    private TestModel _upsertTestModel;
    private TestModel? _onGoingTest;
    public TestContainer(IndexedDb indexedDb)
    {
        _upsertTestModel = new TestModel();
        _indexedDb = indexedDb;
    }
    public async Task UpsertTestToIndexedDb(TestModel? test)
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
    public TestModel UpsertTestModel
    {
        get => _upsertTestModel;
        set
        {
            _upsertTestModel = value;
            NotifyStateChanged();
        }
    }
    public TestModel GetUpsertTest()
    {
        return _upsertTestModel;
    }
    public TestModel? OngoingTest
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
    public TestModel? GetOnGoingTestTest()
    {
        return _onGoingTest;
    }
    public event Action? StateHasChanged;

    private void NotifyStateChanged() => StateHasChanged?.Invoke();
}