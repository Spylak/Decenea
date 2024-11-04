using Decenea.Common.DataTransferObjects.Test;
using Decenea.WebApp.Models;

namespace Decenea.WebApp.Mappers;

public static class TestModelMapper
{
    public static TestDto ToDto(this TestModel model, string userName, string userId)
    {
        return new TestDto
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            MinutesToComplete = model.MinutesToComplete,
            Version = model.Version,
            UserName = userName,
            StartingTime = model.StartingTime,
            UserId = userId,
            Questions = model.GenericQuestionModels.Select(q => q.ToDto()).ToList()
        };
    }
    
    public static TestModel ToTestModel(this TestDto dto)
    {
        return new TestModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            MinutesToComplete = dto.MinutesToComplete,
            Version = dto.Version,
            StartingTime = dto.StartingTime,
            GenericQuestionModels = dto
                .Questions
                .Select(q => q.ToGenericQuestionModel())
                .ToList()
        };
    }
    
    public static TestModel ToTestModel(this ActiveTestDto dto)
    {
        var tesModel =  new TestModel
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            MinutesToComplete = dto.MinutesToComplete,
            Version = dto.Version,
            StartingTime = dto.StartingTime,
            GenericQuestionModels = dto
                .Questions
                .Select(q => q.ToGenericQuestionModel())
                .ToList()
        };
        
        return tesModel;
    }
}