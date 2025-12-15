using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using App;

List<Room> rooms = new List<Room>(); //enums för rum
List<User> Users = new List<User>(); //Denna del är till för att programmet ska kunna veta hur den ska kunna läsa av username och lösenord som är lagrad i accounts.txt 

void Main()
{
    rooms.Add(new Room("101", RoomStateEnum.Available));
    rooms.Add(new Room("102", RoomStateEnum.Available));
    rooms.Add(new Room("103", RoomStateEnum.Unavailable));
    rooms.Add(new Room("104", RoomStateEnum.Unavailable));
    rooms.Add(new Room("105", RoomStateEnum.Maintanance));
    
}

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
                IUser user = Users[0];
                foreach (IUser User in Users)
                
                    if (user.TryLogin(username, password)) // om username samt password matchar med dem lagrade i Accounts.txt så kommer userfound vara true!
                    {
                        Active_user = user;
                        Console.WriteLine("welcome back!");
                        UserMenu();
                        break;
                    }
                    else //ifall username och password inte matchar med någon av dem sparade i Accounts.txt så kommer programet inte gå vidare,
                    {
                        Console.WriteLine("Wrong username or password");
                        Console.WriteLine("press random key to continue");
                        Console.ReadKey();
                        break;
                    }
                
            }
            break;
        case "2":
            break;
    
    }
} 
void UserMenu()
{
    while (Active_user != null)
    {
        Console.Clear();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1. Available rooms");
        Console.WriteLine("2. Book a Room ");
        Console.WriteLine("3. Manage rooms");
        Console.WriteLine("4. log out");

        string val = Console.ReadLine();
        
        switch (val)
        {
            case "1":
                {
                    Console.WriteLine("Available Rooms:");                      //en foreach loop som går igenom rummen och skriver ut de tillgängliga.
                    foreach (Room room in rooms)
                    {
                        if (room.RoomState == RoomStateEnum.Available)                          //en if sats som checkar ifall rummet är tillgänglig.
                        {
                            Console.WriteLine($"Room {room.RoomNr} is {room.RoomState}");
                        }
                    }
                }
            break;
            
        }
    }
}   
