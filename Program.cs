using App; 

List<User> Users = new List<User>(); //Denna del är till för att programmet ska kunna veta hur den ska kunna läsa av username och lösenord som är lagrad i accounts.txt 
 if (File.Exists("Accounts.txt"))
            {
                foreach (var line in File.ReadAllLines("Accounts.txt")) 
                {
                    string[] parts = line.Split(';'); // ";" innebär att username och password kommer att skiljas åt med att detta tecken kommer att finnas mellan!
                    if (parts.Length == 2)
                        Users.Add(new User(parts[0], parts[1]));
                }
            }
IUser? Active_user = null;
bool is_running = true;
while (is_running)
{
    Console.Clear();
    Console.WriteLine("Welcome to Hotell");
    Console.WriteLine("1. sign in");
    Console.WriteLine("2. Quit");

    string input = Console.ReadLine();
    switch (input)
    {
        case "1":
            if (Active_user == null)
            {
                Console.WriteLine("Username");
                string username = Console.ReadLine();
                Console.WriteLine("password");
                string password = Console.ReadLine();
                foreach (IUser user in Users)
                {
                    if (user.TryLogin(username, password))
                    {
                        Active_user = user;
                        Console.WriteLine("Welcome Back");
                    }
                    else if (Active_user == null)
                    {
                        Console.WriteLine("Wrong Username or password");
                    }
                }
            }
            break;
        case "2":
            return;
    }
}