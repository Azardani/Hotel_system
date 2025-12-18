using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using App;

List<Room> rooms = new List<Room>();
List<User> Users = new List<User>();



rooms.Add(new Room("Room101", RoomStateEnum.Available));
rooms.Add(new Room("Room102", RoomStateEnum.Available));
rooms.Add(new Room("Room103", RoomStateEnum.Unavailable));
rooms.Add(new Room("Room104", RoomStateEnum.Unavailable));
rooms.Add(new Room("Room105", RoomStateEnum.Maintenance));
    
SaveRooms();

void SaveRooms()
{
    List<string> lines = new();

    foreach (Room room in rooms)
    {
        lines.Add($"{room.RoomNr};{room.RoomState}");
    }

    File.WriteAllLines("Rooms.txt", lines);
}

if (File.Exists("Accounts.txt"))
    {
        foreach (var line in File.ReadAllLines("Accounts.txt")) 
        {
            string[] parts = line.Split(';');
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
                IUser user = Users[0];
                foreach (IUser User in Users)
                
                    if (user.TryLogin(username, password))
                    {
                        Active_user = user;
                        Console.WriteLine("welcome back!");
                        UserMenu();
                        break;
                    }
                    else
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
        Console.WriteLine("1. Room status");
        Console.WriteLine("2. Book a Room ");
        Console.WriteLine("3. Manage rooms");
        Console.WriteLine("4. log out");


        string input = Console.ReadLine();
        
        switch (input)
        {
            case "1":
                {
                    Console.WriteLine("Room status:");                     
                    foreach (Room room in rooms)
                    {
                        
                        Console.WriteLine($"Room {room.RoomNr} is {room.RoomState}");
                    
                    }
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();
                }break;
            case "2":
                {
                    Console.WriteLine("these room are available");
                    bool found = false;
                    foreach(Room room in rooms)
                    {
                        if(room.RoomState == RoomStateEnum.Available)
                        {
                            
                            Console.WriteLine($"Room {room.RoomNr} is available");
                            found = true;
                        }

                    }
                    if (!found)
                    {
                        Console.WriteLine("No rooms available");
                    }
                    Console.WriteLine("Choose a room to book!");
                    String RoomNr = Console.ReadLine();
                    Room? selectedRoom = rooms.FirstOrDefault(r => r.RoomNr == RoomNr);
                    if (selectedRoom == null)
                    {
                        Console.WriteLine("invalid Room number");
                        Console.WriteLine("press any key to return");
                        Console.ReadKey();
                        break;
                    }
                    if (selectedRoom.RoomState == RoomStateEnum.Available)
                    {
                        Console.WriteLine("Guest Name");
                        String GuestName = Console.ReadLine();

                        selectedRoom.RoomState = RoomStateEnum.Unavailable; // Rummet blir otillgängligt
                        selectedRoom.GuestName = GuestName;  // Spara gästens namn

                        SaveRooms();

                        Console.WriteLine($"Room {RoomNr} has been booked for {GuestName}.");

                    }


                    break;
                }
                
                
                      
                
        }
    }
}   
