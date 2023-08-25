namespace Decenea.Domain.DataTransferObjects.Advertisement;

public class CreateMicroAdServiceRequestDto : CreateMicroAdRequestDto
{
    public CreateMicroAdServiceRequestDto()
    {
        
    }
    public CreateMicroAdServiceRequestDto(CreateMicroAdRequestDto createMicroAdRequestDto)
    {
        Title = createMicroAdRequestDto.Title;
        Description = createMicroAdRequestDto.Description;
        ContactEmail = createMicroAdRequestDto.ContactEmail;
        ContactPhone = createMicroAdRequestDto.ContactPhone;
    }
    public long ApplicationUserId { get; set; }
    public string CityId { get; set; }
}