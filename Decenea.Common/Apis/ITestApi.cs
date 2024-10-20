using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Requests.Test;
using Refit;

namespace Decenea.Common.Apis;
[Headers("Content-Type: application/json")]
public interface ITestApi
{
    [Post(RouteConstants.TestsCreate)]
    Task<ApiResponseResult<TestDto>> Create([Body] CreateTestRequest request);
    [Put(RouteConstants.TestsUpdate)]
    Task<ApiResponseResult<TestDto>> Update([Body] UpdateTestRequest request);
    [Delete(RouteConstants.TestsDelete)]
    Task<ApiResponseResult<object>> Delete([Body] DeleteTestsRequest request);
    [Get(RouteConstants.TestsGet)]
    Task<ApiResponseResult<TestDto>> Get(GetTestRequest request);
    [Get(RouteConstants.TestsGetMany)]
    Task<ApiResponseResult<List<TestDto>>> Get(GetManyTestsRequest request);
    [Put(RouteConstants.TestsAddQuestions)]
    Task<ApiResponseResult<object>> AddQuestions([Body] AddTestQuestionsRequest request);
    [Put(RouteConstants.TestsRemoveQuestions)]
    Task<ApiResponseResult<object>> RemoveQuestions([Body] RemoveTestQuestionsRequest request);
}