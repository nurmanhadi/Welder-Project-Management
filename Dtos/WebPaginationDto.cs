namespace WelderProjectManagement.Dtos;

public class WebPagination<T>
{
    public required T Contents { get; set; }
    public required int Page { get; set; }
    public required int Size { get; set; }
    public required int TotalPages { get; set; }
    public required int TotalElements { get; set; }
}
