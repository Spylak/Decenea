using Decenea.Common.Common;
using Decenea.Common.DataTransferObjects.Group;
using Decenea.Common.DataTransferObjects.Test;
using Decenea.Common.Requests.Group;
using Decenea.Common.Requests.Test;
using Refit;

namespace Decenea.WebApp.Apis;
[Headers("Content-Type: application/json")]
public interface ITestApi
{
    [Post("/api/tests/create")]
    Task<ApiResponseResult<TestDto>> Create([Body] CreateTestRequest request);
    [Put("/api/tests/update")]
    Task<ApiResponseResult<TestDto>> Update([Body] UpdateTestRequest request);
    [Delete("/api/tests/delete")]
    Task<ApiResponseResult<object>> Delete([Body] DeleteTestsRequest request);
    [Get("/api/tests/get")]
    Task<ApiResponseResult<TestDto>> Get(GetTestRequest request);
    [Get("/api/tests/get-many")]
    Task<ApiResponseResult<List<TestDto>>> Get(GetManyTestsRequest request);
    [Put("/api/tests/add-questions")]
    Task<ApiResponseResult<object>> AddQuestions([Body] AddTestQuestionsRequest request);
    [Put("/api/tests/remove-questions")]
    Task<ApiResponseResult<object>> RemoveQuestions([Body] RemoveTestQuestionsRequest request);
}