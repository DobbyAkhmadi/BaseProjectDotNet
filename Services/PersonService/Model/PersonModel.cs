namespace BaseProjectDotnet.Services.PersonService.Model;

public abstract class PersonModel
{
    public int? Id { get; set; } = null;
    public int IdGender { get; set; }
    public int IdHobby { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}