namespace BaseProjectDotnet.Helpers.Global.Models
{
  public class DataTableRequestModel
  {
    public DataTableRequestModel()
    {
      order = new List<DataTableOrderBindingModel>();
      search = new DataTableSearchBindingModel();
      columns = new List<DataTableColumnBindingModel>();
      advSearch = new Dictionary<string, string>();
    }

    public int draw { get; set; }
    public int start { get; set; }
    public int length { get; set; }
    public List<DataTableOrderBindingModel> order { get; set; }
    public DataTableSearchBindingModel search { get; set; }
    public List<DataTableColumnBindingModel> columns { get; set; }
    public Dictionary<string, string> advSearch { get; set; }

    public int TypeActive { get; set; }=1;

    public class DataTableOrderBindingModel
    {
      public int column { get; set; }
      public string dir { get; set; }
    }

    public class DataTableColumnBindingModel
    {
      public DataTableColumnBindingModel()
      {
        search = new DataTableSearchBindingModel();
      }

      public string data { get; set; }
      public string name { get; set; } = string.Empty;
      public bool searchable { get; set; }
      public bool orderable { get; set; }
      public DataTableSearchBindingModel search { get; set; }
    }

    public class DataTableSearchBindingModel
    {
      public string value { get; set; } = string.Empty;
      public bool regex { get; set; }
    }
  }

}

