namespace BaseProjectDotnet.Services.AuditService.Model;

public class AuditTrailModel
{
  public string? id { get; set; }
  public string? user_id { get; set; }
  public string? user_name { get; set; }
  public string? role_id { get; set; }
  public string? role_name { get; set; }
  public string? remote_ip { get; set; }
  public string? session_id { get; set; }
  public string? action { get; set; }
  public string? function_name { get; set; }
  public string? message { get; set; }
  public string? old_model { get; set; }
  public string? new_model { get; set; }
  public string? latency { get; set; }
   public DateTime? created_date { get; set; }
}


