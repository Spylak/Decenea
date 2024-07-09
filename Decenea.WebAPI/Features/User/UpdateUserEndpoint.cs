using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Users.Commands.UpdateUser;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Requests.User;


namespace Decenea.WebAPI.Features.User;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest,ApiResponse<UserDto>>
{
    private readonly UpdateUserCommandHandler _updateUserCommandHandler;
    public UpdateUserEndpoint(UpdateUserCommandHandler updateUserCommandHandler)
    {
        _updateUserCommandHandler = updateUserCommandHandler;
    }

    public override void Configure()
    {
        Post("/User/Update");
    }

    public override async Task<ApiResponse<UserDto>> ExecuteAsync(UpdateUserRequest req, CancellationToken ct)
    {
        var command = new UpdateUserCommand()
        {
            Id = req.Id,
            Email = req.Email,
            UserName = req.UserName,
            FirstName = req.FirstName,
            LastName = req.LastName,
            MiddleName = req.MiddleName,
            PhoneNumber = req.PhoneNumber,
            Version = req.Version
        };
        
        var result = await _updateUserCommandHandler.Handle(command, ct);
        
        return new ApiResponse<UserDto>(result.Value,result.IsSuccess,result.Messages);
    }
}