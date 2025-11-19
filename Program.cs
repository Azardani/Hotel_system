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
    switch (input) // Lägger till funktioner till cases , i detta fall så kommer "1" och "2" kunna göra något
    {
        case "1":
            if (Active_user == null) //Active user innebär att en användare är inloggad, i detta fall finns ingen användare än, alltså null
            {
                Console.WriteLine("Username");
                string username = Console.ReadLine();
                Console.WriteLine("password");
                string password = Console.ReadLine();

                bool userfound = false; // programmet utgår ifrån att den inte hittat en användare från början, 
                foreach (IUser user in Users)
                {
                    if (user.TryLogin(username, password)) // om username samt password matchar med dem lagrade i Accounts.txt så kommer userfound vara true!
                    {
                        Active_user = user;
                        userfound = true;
                        Console.WriteLine("Welcome Back");
                        Console.ReadKey();
                        foreach (Hotel hotel in Hotels)
                        {
                            
                        }
                    }
                    else if (!userfound) //ifall username och password inte matchar med någon av dem sparade i Accounts.txt så kommer programet inte gå vidare,
                    {
                        Console.WriteLine("Wrong username or password");
                        Console.WriteLine("press random key to continue");
                        Console.ReadKey();
                        break;
                    }
                }
            }
            break;
        case "2":
            return;
    }
         
    
}    
