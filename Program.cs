using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using App;

List<Room> rooms = new List<Room>();
List<User> Users = new List<User>();


///enums för room
rooms.Add(new Room("Room101", RoomStateEnum.Available));
rooms.Add(new Room("Room102", RoomStateEnum.Available));
rooms.Add(new Room("Room103", RoomStateEnum.Maintained));
rooms.Add(new Room("Room104", RoomStateEnum.Available));
rooms.Add(new Room("Room105", RoomStateEnum.Maintained));
    
SaveRooms(); //sparar room

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
            string[] parts = line.Split(';');                       //hur en account sparas i accounts.txt
            if (parts.Length == 2)
                Users.Add(new User(parts[0], parts[1]));
        }
    }
IUser? Active_user = null; ///ifall ingen användare registreras så öppnas menyn
bool is_running = true;
while (is_running)
{
  Console.Clear();


Console.WriteLine(@"██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝");


Console.WriteLine("1. Sign in");
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
                foreach (IUser User in Users) ///kollar alla användare, men i detta fall finns bara en,(Stilpoäng)
                
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
            return;
    
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
                        
                        Console.WriteLine($"{room.RoomNr} is {room.RoomState}");
                    
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
                        
                        Console.WriteLine($"{room.RoomNr} is available");
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

                    selectedRoom.RoomState = RoomStateEnum.Unavailable;  
                    selectedRoom.GuestName = GuestName;  

                    SaveRooms();

                    Console.WriteLine($"{RoomNr} has been booked for {GuestName}.");
                    Console.WriteLine("press any key to continue");
                    Console.ReadKey();

                }


                break;
         
           }
        case "3":
            {
                Console.WriteLine("Room status:");

                
                foreach (Room room in rooms)
                {
                    Console.WriteLine($"Room {room.RoomNr} is {room.RoomState}");
                }

                Console.WriteLine("Choose a room to manage:");
                string roomNr = Console.ReadLine();

                
                Room? selectedRoom = rooms.FirstOrDefault(r => r.RoomNr == roomNr);

                if (selectedRoom == null)
                {
                    Console.WriteLine("Invalid room number, try again.");
                    Console.ReadKey(); 
                    break; 
                }

                

                if (selectedRoom.RoomState == RoomStateEnum.Available)
                {
                    Console.WriteLine("What would you like to do with this room?");
                    Console.WriteLine("1. Close for Maintenance");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                           
                            selectedRoom.RoomState = RoomStateEnum.Maintained;
                            Console.WriteLine($"Room {selectedRoom.RoomNr} is now under {selectedRoom.RoomState}.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                if (selectedRoom.RoomState == RoomStateEnum.Maintained)
                {
                    Console.WriteLine("What would you like to do with this room?");
                    Console.WriteLine("1. Open for Guests");
                    Console.WriteLine("2. Do nothing");

                    string action = Console.ReadLine();

                    switch (action)
                    {
                        case "1":
                            
                            selectedRoom.RoomState = RoomStateEnum.Available;
                            Console.WriteLine($"Room {selectedRoom.RoomNr} is now {selectedRoom.RoomState}.");
                            Console.WriteLine("press any key to continue");
                            Console.ReadKey();
                            break;

                        case "2":
                            Console.WriteLine("No changes made.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                    SaveRooms();
                    break;
                }
                
                
                
                if(selectedRoom.RoomState == RoomStateEnum.Unavailable)
                {
                    Console.WriteLine($"this Room is occupied by {selectedRoom.GuestName}");
                    Thread.Sleep(2000);
                    Console.WriteLine("would you like to check the guest out?");
                    Console.WriteLine("1. yes");
                    Console.WriteLine("2. No");
                    string val = Console.ReadLine();

                    switch (val)
                        {
                            case "1":
                                {
                                    selectedRoom.RoomState = RoomStateEnum.Available;
                                    Console.WriteLine("The guest has been checked out");
                                    Thread.Sleep(1000);
                                    Console.WriteLine("this room is now available");
                                    Console.WriteLine("press any key to continue");
                                    Console.ReadKey();
                                    break;
                                }
                            case "2":
                                {
                                    Console.WriteLine("Guest stays");
                                    break;
                                }

                        }
                }break;

                            
            
        
                        
                
                
            }
            
    }
}
}