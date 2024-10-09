using Decenea.Common.Requests.User;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Apis;
using Decenea.WebApp.State;

namespace Decenea.WebApp.Middleware;

public class TokenHandler : DelegatingHandler
{
    private readonly IAuthApi _authApi;
    private readonly IAuthStateProvider _authStateProvider;

    public TokenHandler(IAuthApi authApi, AuthStateProvider authStateProvider)
    {
        _authApi = authApi ?? throw new ArgumentNullException(nameof(authApi));
        _authStateProvider = authStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_authStateProvider.AuthTokensResponse.AccessToken is null)
        {
            throw new ArgumentNullException(nameof(_authStateProvider.AuthTokensResponse.AccessToken));
        }
        
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authStateProvider.AuthTokensResponse.AccessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            if (_authStateProvider.AuthTokensResponse.RefreshToken is null)
            {
                await _authStateProvider.NotifyUserLogout();
                return new HttpResponseMessage();
            }
            
            var refreshTokenResponse = await _authApi.RefreshToken(new RegenerateAuthTokensRequest(_authStateProvider.AuthTokensResponse.RefreshToken));

            if (refreshTokenResponse.IsError && refreshTokenResponse.Data is not null)
            {
                await _authStateProvider.NotifyUserLogin(refreshTokenResponse.Data);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", refreshTokenResponse.Data.AccessToken);

                response = await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
}