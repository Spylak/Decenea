
using Decenea.Common.DataTransferObjects.Group;

namespace Decenea.Common.DataTransferObjects.Test;

public class TestDto : BaseTestDto
{
    public List<TestUserDto> TestUsers { get; set; } = [];
    public List<GroupDto> Groups { get; set; } = [];
}