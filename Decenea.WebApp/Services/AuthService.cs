using Blazored.LocalStorage;
using Decenea.Common.Apis;
using Decenea.Common.Requests.User;
using Decenea.WebApp.Abstractions;
using ErrorOr;
using Refit;

namespace Decenea.WebApp.Services;

public class AuthService : IAuthService
{
    private readonly IAuthApi _authApi;
    private readonly ILocalStorageService _localStorageService;
    private readonly IAuthStateProvider _authStateProvider;
    private readonly IGlobalFunctionService _globalFunctionService;
    public AuthService(IAuthApi authApi, ILocalStorageService localStorageService,
        IAuthStateProvider authStateProvider, IGlobalFunctionService globalFunctionService)
    {
        _authApi = authApi;
        _localStorageService = localStorageService;
        _authStateProvider = authStateProvider;
        _globalFunctionService = globalFunctionService;
    }
    
    public async Task<ErrorOr<object>> Login(LoginUserRequest request)
    {
        try
        {
            var response = await _authApi.Login(request);
            if (response.Data is not  null)
            {
                await _authStateProvider.NotifyUserLogin(response.Data.AuthTokensResponse);
                return true;
            }

            return Error.Failure(description: "Couldn't login.");
        }
        catch (ApiException ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
    
    public async Task<ErrorOr<bool>> UserLogout()
    {
        try
        {
            await _authStateProvider.NotifyUserLogout();
            return true;
        }
        catch (Exception e)
        {
            return Error.Unexpected(description: e.Message);
        }
    }
    
    // public async Task<ResponseModel> SendResetTokenToResetPass(string email)
    // {
    //     try
    //     {
    //         var body = new JsonObject();
    //         body.Add("email",email);
    //         var requestContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
    //         var client = _globalFunctionService.CreateClient();
    //         var response =await  client.PostAsync("/my/user/password/send-reset-token", requestContent);
    //         var contents = await response.Content.ReadAsStringAsync();
    //         if (contents.Contains("ErrorType"))
    //         {
    //             var obj = JsonConvert.DeserializeObject<Dictionary<string,string>>(contents);
    //             return new ResponseModel() {Message =obj!["ErrorType"] ,StatusCode = 400};
    //         }
    //         return new ResponseModel() {StatusCode = (int)response.StatusCode};
    //     }
    //     catch (Exception e)
    //     {
    //         return new ResponseModel() {Message = e.Message,StatusCode = 400};
    //     }
    // }
    
    // public async Task<ResponseModel> ResetPasswordWithToken(string email,string password,string token)
    // {
    //     try
    //     {
    //         var body = new JsonObject();
    //         body.Add("email",email);
    //         body.Add("newPassword",password);
    //         body.Add("resetToken",token);
    //         var requestContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
    //         var client = _globalFunctionService.CreateClient();
    //         var response =await  client.PostAsync("/my/user/password/using-reset-token", requestContent);
    //         var contents = await response.Content.ReadAsStringAsync();
    //         if (contents.Contains("ErrorType"))
    //         {
    //             var obj = JsonConvert.DeserializeObject<Dictionary<string,string>>(contents);
    //             return new ResponseModel() {Message =obj!["ErrorType"] ,StatusCode = 400};
    //         }
    //         return new ResponseModel() {StatusCode = (int)response.StatusCode};
    //     }
    //     catch (Exception e)
    //     {
    //         return new ResponseModel() {Message = e.Message,StatusCode = 400};
    //     }
    // }
    //
    // public async Task<ResponseModel> SetEmailAndOrPassword(string email="",string password="")
    // {
    //     try
    //     {
    //         var Token=await _authService.GetCookie(GenericConstants.Token);
    //         var client = _globalFunctionService.CreateClient();
    //         client.DefaultRequestHeaders.Add("-TokenKey",Token.Data);
    //         var dictionary = new Dictionary<string, string>();
    //         dictionary["email"] = email;
    //         dictionary["password"] = password;
    //         var requestContent = new StringContent(JsonSerializer.Serialize(dictionary), Encoding.UTF8, "application/json");
    //         var response =await  client.PostAsync("/my/user/credentials", requestContent);
    //         return new ResponseModel() { StatusCode = (int)response.StatusCode,Message = response.RequestMessage!.ToString()};
    //     }
    //     catch (Exception e)
    //     {
    //         return new ResponseModel() {StatusCode = 500,Message = e.Message};
    //     }
    // }
}