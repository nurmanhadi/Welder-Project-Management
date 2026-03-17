namespace WelderProjectManagement.Dtos;

public class WebResponse<T>
{
    public T? Data { get; set; }
    public T? Errors { get; set; }
    public required string Path { get; set; }

    public static WebResponse<T> Success(T data, HttpContext context)
    {
        return new WebResponse<T>
        {
            Data = data,
            Path = context.Request.Path.Value ?? string.Empty
        };
    }
    public static WebResponse<T> Failed(T error, HttpContext context)
    {
        return new WebResponse<T>
        {
            Errors = error,
            Path = context.Request.Path.Value ?? string.Empty
        };
    }
}
