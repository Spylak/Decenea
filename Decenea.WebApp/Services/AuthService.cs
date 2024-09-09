using Blazored.LocalStorage;
using Decenea.Common.Requests.User;
using Decenea.WebApp.Abstractions;
using Decenea.WebApp.Apis;
using ErrorOr;

namespace Decenea.WebApp.Services;

public class AuthService : IAuthService
{
    private readonly IAuthApi _authApi;
    private readonly ILocalStorageService _localStorageService;
    private readonly IAuthStateProvider _authStateProvider;
    public AuthService(IAuthApi authApi, ILocalStorageService localStorageService,
        IAuthStateProvider authStateProvider)
    {
        _authApi = authApi;
        _localStorageService = localStorageService;
        _authStateProvider = authStateProvider;
    }
    public async Task<ErrorOr<object>> Login(LoginUserRequest request)
    {
        try
        {
            var response = await _authApi.Login(request);
            await _localStorageService.SetItemAsync("session", response.Data);
            _authStateProvider.NotifyAuthenticationStateChanged();
            return true;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
    
    public async Task<ErrorOr<bool>> UserLogout()
    {
        try
        {
            await _localStorageService.RemoveItemAsync("session");
            _authStateProvider.NotifyAuthenticationStateChanged();
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
    //         var gyxiToken=await _authService.GetCookie(GenericConstants.GyxiToken);
    //         var client = _globalFunctionService.CreateClient();
    //         client.DefaultRequestHeaders.Add("Gyxi-TokenKey",gyxiToken.Data);
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