namespace BaseProjectDotnet.Helpers.Database
{
  public class DataTableResultModel
  {
    public int draw { get; set; } = 0;
    public int recordsTotal { get; set; } = 0;
    public int recordsFiltered { get; set; } = 0;
    public object data { get; set; } = null;
    public string? error { get; set; } = "";
  }
}
