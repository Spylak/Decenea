using Decenea.Application.Abstractions.Persistance;
using Decenea.Application.Users.Commands.LoginUser;
using Decenea.Common.Common;
using Decenea.Common.Requests.Users;
using Mediator;

namespace Decenea.WebAPI.Features.User;

public class LoginUserEndpoint : Endpoint<LoginUserRequest, ApiResponse<LoginUserResponse>>
{
    private readonly IMediator _mediator;
    
    private readonly IDeceneaDbContext _dbContext;
    public LoginUserEndpoint(IMediator mediator, IDeceneaDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/User/Login");
        AllowAnonymous();
    }

    public override async Task<ApiResponse<LoginUserResponse>> ExecuteAsync(LoginUserRequest req,
        CancellationToken ct)
    {
        _dbContext.CreatedBy = "Anonymous";
        var command = new LoginUserCommand(req.Email,req.Password,req.RememberMe ?? false);
        var result = await _mediator.Send(command);
        return new ApiResponse<LoginUserResponse>(result.Value, result.IsSuccess, result.Messages);
    }
}