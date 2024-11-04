using Decenea.Common.Common;
using Decenea.Common.Constants;
using Decenea.Common.DataTransferObjects.Question;
using Decenea.Common.Requests.Question;
using Refit;

namespace Decenea.Common.Apis;
[Headers("Content-Type: application/json")]
public interface IQuestionApi
{
    [Post(RouteConstants.QuestionsCreate)]
    Task<ApiResponseResult<List<QuestionDto>>> Create([Body] CreateQuestionsRequest request);
    // [Put(RouteConstants.QuestionsUpdate)]
    // Task<ApiResponseResult<QuestionDto>> Update([Body] UpdateTestRequest request);
    [Delete(RouteConstants.QuestionsDelete)]
    Task<ApiResponseResult<object>> Delete([Body] DeleteQuestionsRequest request);
    [Get(RouteConstants.QuestionsGet)]
    Task<ApiResponseResult<QuestionDto>> Get(GetQuestionRequest request);
    [Get(RouteConstants.QuestionsGetMany)]
    Task<ApiResponseResult<List<QuestionDto>>> Get(GetManyQuestionsRequest request);
}