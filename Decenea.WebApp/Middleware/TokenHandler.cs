using Blazored.LocalStorage;
using Decenea.Common.DataTransferObjects.Auth;
using Decenea.Common.Requests.User;
using Decenea.WebApp.Apis;
using Decenea.WebApp.State;

namespace Decenea.WebApp.Middleware;

public class TokenHandler : DelegatingHandler
{
    private readonly IAuthApi _authApi;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthStateProvider _authStateProvider;

    public TokenHandler(IAuthApi authApi, ILocalStorageService localStorage, AuthStateProvider authStateProvider)
    {
        _authApi = authApi ?? throw new ArgumentNullException(nameof(authApi));
        _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
        _authStateProvider = authStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var session = await _localStorage.GetItemAsync<LoginUserResponse>("session", cancellationToken);
        if (session is not { RefreshToken : not null })
        {
            throw new ArgumentNullException(nameof(session));
        }
        
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authStateProvider.AccessToken);

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshTokenResponse = await _authApi.RefreshToken(new RegenerateAuthTokensRequest(session.RefreshToken));

            if (refreshTokenResponse.IsError && refreshTokenResponse.Data is not null)
            {
                await _localStorage.SetItemAsync("session", refreshTokenResponse.Data, cancellationToken);

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", refreshTokenResponse.Data.AccessToken);

                response = await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }
}