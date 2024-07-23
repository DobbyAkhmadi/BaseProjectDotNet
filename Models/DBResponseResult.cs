namespace BaseProjectDotnet.Models;

public class DbResponseResult()
{
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "Success";
    public object? Payload { get; set; } = null;
}