namespace BaseProjectDotnet.Services.AuditService.Model;

public class AuditTrail
{
  public string user_id;
  public string user_name;
  public string roles;
  public string remote_ip;
  public string session_id;
  public string action;
  public string function_name;
  public string error_message;
  public string old_model;
  public string new_model;
  public string latency;
  public DateTime timestamp;
}
