namespace App;

interface IUser
{
   public bool TryLogin(string username, string password);
}