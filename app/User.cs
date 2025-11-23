public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public User(string name, int age, string email)
    {
        Name = name;
        Age = age;
        Email = email;
    }
}
