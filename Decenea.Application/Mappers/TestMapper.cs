using Decenea.Common.DataTransferObjects.Test;
using Decenea.Domain.Aggregates.TestAggregate;

namespace Decenea.Application.Mappers;

public static class TestMapper
{
    public static TestDto TestToTestDto(this Test test, TestDto? testDto = null)
    {
        testDto ??= new TestDto();
        testDto.Title = test.Title;
        testDto.Description = test.Description;
        testDto.ContactPhone = test.ContactPhone;
        testDto.ContactEmail = test.ContactEmail;
        // microAdDto.ApplicationUserName = microAd.ApplicationUser.FullName;
        return testDto;
    }
}