namespace UnityArenaApi.Domain.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess{get;protected set;}
        public string Message{get;protected set;}
        public BaseResponse(bool success,string message){
            IsSuccess = success;
            Message = message;
        }
    }
}