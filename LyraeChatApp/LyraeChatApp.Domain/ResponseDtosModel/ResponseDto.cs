namespace LyraeChatApp.Domain.ResponseDtosModel;

public class ResponseDto<T>
{
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    public int StatusCode { get; private set; }
    public bool IsSuccessful { get; private set; }   

    public static ResponseDto<T> Success(T data, int statusCode)
    {
        return new ResponseDto<T> { Data = data, StatusCode = statusCode ,IsSuccessful= true};
    }

    public static ResponseDto<T> Success(int statusCode)
    {
        return new ResponseDto<T> { Data=default(T), StatusCode = statusCode ,IsSuccessful= true};
    }

    public static ResponseDto<T> Error(List<string> errors, int statusCode)
    {
        return new ResponseDto<T> { Errors= errors, StatusCode = statusCode ,IsSuccessful= false};
    }

    public static ResponseDto<T> Error(string error, int statusCode)
    {
        return new ResponseDto<T> { Errors = new List<string>() { error }, StatusCode = statusCode};
    }
}
