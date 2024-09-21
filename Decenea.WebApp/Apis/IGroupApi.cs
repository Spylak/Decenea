using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Requests.Group;
using Refit;

namespace Decenea.WebApp.Apis;
[Headers("Content-Type: application/json")]
public interface IGroupApi
{
    [Post("/api/groups/create")]
    Task<ApiResponseResult<List<GroupDto>>> Create([Body] CreateGroupsRequest request);
    [Put("/api/groups/update")]
    Task<ApiResponseResult<GroupDto>> Update([Body] UpdateGroupRequest request);
    [Delete("/api/groups/delete")]
    Task<ApiResponseResult<object>> Delete([Body] DeleteGroupsRequest request);
    [Get("/api/groups/get")]
    Task<ApiResponseResult<GroupDto>> Get(GetGroupRequest request);
    [Get("/api/groups/get-many")]
    Task<ApiResponseResult<List<GroupDto>>> Get();
    [Put("/api/groups/update-member")]
    Task<ApiResponseResult<GroupMemberDto>> UpdateMember([Body] UpdateGroupMemberRequest request);
    [Put("/api/groups/add-members")]
    Task<ApiResponseResult<object>> AddMembers([Body] AddGroupMembersRequest request);
    [Put("/api/groups/remove-members")]
    Task<ApiResponseResult<object>> RemoveMembers([Body] RemoveGroupMembersRequest request);
}