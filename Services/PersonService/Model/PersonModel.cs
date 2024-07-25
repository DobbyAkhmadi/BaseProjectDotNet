namespace BaseProjectDotnet.Services.PersonService.Model;

public abstract class PersonModel
{
    public int? Id { get; set; } = null;
    public string Name { get; set; }
    public int Age { get; set; }
    public List<PersonModelDetail> PersonList = new();
}

public abstract class PersonModelDetail()
{
    public int IdDetail { get; set; }
    public int Name { get; set; }
}

