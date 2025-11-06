namespace App;
public class User : IUser
{
    public string Username;
    string Password;


    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return Username == username && Password == password;
    }
}
