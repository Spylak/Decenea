namespace Decenea.Common.Constants;

public static class RouteConstants
{
    public const string BaseApiUrl = "http://localhost:5080/api";
    
    public const string TestsCreate = "/tests/create";
    public const string TestsUpdate = "/tests/update";
    public const string TestsDelete = "/tests/delete";
    public const string TestsGet = "/tests/get";
    public const string TestsGetActive = "/tests/get-active";
    public const string TestsGetMany = "/tests/get-many";
    public const string TestsAddQuestions = "/tests/add-questions";
    public const string TestsRemoveQuestions = "/tests/remove-questions";
    
    public const string UsersLogin = "/auth/login";
    public const string UsersLogout = "/auth/logout";
    public const string UsersRegenerateAuthTokens = "/auth/regenerate-auth-tokens";
    
    public const string UsersGet = "/users/get";
    public const string UsersGetMany = "/users/get-many";
    public const string UsersUpdate = "/users/update";
    public const string UsersRegister = "/users/register";
    
    public const string GroupsCreate = "/groups/create";
    public const string GroupsGet = "/groups/get";
    public const string GroupsDelete = "/groups/delete";
    public const string GroupsUpdate = "/groups/update";
    public const string GroupsGetMany = "/groups/get-many";
    public const string GroupsAddGroupMembers = "/groups/add-group-members";
    public const string GroupsUpdateGroupMember = "/groups/update-group-member";
    public const string GroupsRemoveGroupMembers = "/groups/remove-group-members";
    
    public const string AnswersUpsert = "/answers/upsert";
    
    public const string QuestionsCreate = "/questions/create";
    public const string QuestionsUpdate = "/questions/update";
    public const string QuestionsDelete = "/questions/delete";
    public const string QuestionsGet = "/questions/get";
    public const string QuestionsGetMany = "/questions/get-many";
}