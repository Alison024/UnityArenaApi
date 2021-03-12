namespace UnityArenaApi.Domain.Responses
{
    public class GenericResponse<T> : BaseResponse where T:class
    {
        public T internalValue{get;set;}
        public GenericResponse(bool success, string message,T internalValue) : base(success, message)
        {
            this.internalValue = internalValue;
        }
        public GenericResponse(T internalValue):this(true,string.Empty,internalValue){}
        public GenericResponse(string mes):this(false,mes,null){}

    }
}