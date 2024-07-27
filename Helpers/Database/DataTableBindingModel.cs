namespace BaseProjectDotnet.Helpers.Database;

public class DataTableBindingModel
{
  public int draw { get; set; }
  public int start { get; set; }
  public int length { get; set; }
  public List<DataTableOrderBindingModel> order { get; set; } = new();
  public DataTableSearchBindingModel search { get; set; } = new();
  public List<DataTableColumnBindingModel> columns { get; set; } = new();
  public Dictionary<string, string>? advSearch { get; set; }
  public int TypeActive { get; set; }
}

public abstract class DataTableOrderBindingModel
{
  public int column { get; set; }
  public string dir { get; set; }
}

public class DataTableSearchBindingModel
{
  public string value { get; set; }
  public bool regex { get; set; }
}

public abstract class DataTableColumnBindingModel(string data, string name)
{
  public string data { get; set; } = data;
  public string name { get; set; } = name;
  public bool searchable { get; set; }
  public bool orderable { get; set; }
  public DataTableSearchBindingModel search { get; set; } = new();
}
