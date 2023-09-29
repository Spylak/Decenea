using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Users.Commands.RegisterUser;
using Decenea.Application.Users.Commands.UpdateUser;
using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.User;
using Decenea.Common.Requests.Users;
using Mediator;

namespace Decenea.WebAPI.Features.User;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest,ApiResponse<UserDto>>
{
    private readonly IMediator _mediator;
    private readonly IDeceneaDbContext _dbContext;
    public UpdateUserEndpoint(IMediator mediator, IDeceneaDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/User/Update");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<UserDto>> ExecuteAsync(UpdateUserRequest req, CancellationToken ct)
    {
        _dbContext.CreatedBy = "Anonymous";
        var command = new UpdateUserCommand()
        {
            Id = req.Id,
            Email = req.Email,
            UserName = req.UserName,
            FirstName = req.FirstName,
            LastName = req.LastName,
            MiddleName = req.MiddleName,
            PhoneNumber = req.PhoneNumber,
            CityId = req.CityId,
            Version = req.Version
        };
        
        var result = await _mediator.Send(command);
        
        return new ApiResponse<UserDto>(result.Value,result.IsSuccess,result.Messages);
    }
}