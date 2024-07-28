namespace BaseProjectDotnet.Helpers.Global.Models;

public class DbResponseResult()
{
  public int StatusCode { get; set; } = 200;
  public bool Success { get; set; } = true;
  public string? Message { get; set; } = "Success";
  public object? Payload { get; set; }
}
