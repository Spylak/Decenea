using Decenea.Application.Users.Commands.UpdateUser;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Extensions;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Features.User;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, ApiResponseResult<UserDto>>
{
    public override void Configure()
    {
        Post("/users/update");
    }

    public override async Task<ApiResponseResult<UserDto>> ExecuteAsync(UpdateUserRequest req, CancellationToken ct)
    {
        var result = await new UpdateUserCommand()
        {
            Id = req.Id,
            Email = req.Email,
            UserName = req.UserName,
            FirstName = req.FirstName,
            LastName = req.LastName,
            MiddleName = req.MiddleName,
            PhoneNumber = req.PhoneNumber,
            Version = req.Version
        }.ExecuteAsync(ct);
        
        return new ApiResponseResult<UserDto>(result.Value, result.IsError, result.Errors.ToErrorDictionary());
    }
}