using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.Requests.Group;
using Refit;

namespace Decenea.Common.Apis;
[Headers("Content-Type: application/json")]
public interface IGroupApi
{
    [Post(RouteConstants.GroupsCreate)]
    Task<ApiResponseResult<List<GroupDto>>> Create([Body] CreateGroupsRequest request);
    [Put(RouteConstants.GroupsUpdate)]
    Task<ApiResponseResult<GroupDto>> Update([Body] UpdateGroupRequest request);
    [Delete(RouteConstants.GroupsDelete)]
    Task<ApiResponseResult<object>> Delete([Body] DeleteGroupsRequest request);
    [Get(RouteConstants.GroupsGet)]
    Task<ApiResponseResult<GroupDto>> Get(GetGroupRequest request);
    [Get(RouteConstants.GroupsGetMany)]
    Task<ApiResponseResult<List<GroupDto>>> Get();
    [Put(RouteConstants.GroupsUpdateGroupMember)]
    Task<ApiResponseResult<GroupMemberDto>> UpdateMember([Body] UpdateGroupMemberRequest request);
    [Put(RouteConstants.GroupsAddGroupMembers)]
    Task<ApiResponseResult<object>> AddMembers([Body] AddGroupMembersRequest request);
    [Put(RouteConstants.GroupsRemoveGroupMembers)]
    Task<ApiResponseResult<object>> RemoveMembers([Body] RemoveGroupMembersRequest request);
}