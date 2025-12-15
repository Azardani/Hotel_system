namespace App;
    public enum RoomStateEnum
    {
        Available,  
        Unavailable,
        Maintanance,
    }
    
class Room
{
    public string RoomNr;
    public RoomStateEnum RoomState;

    public Room(string roomNumber, RoomStateEnum roomState = RoomStateEnum.Available)
    {
        RoomNr = roomNumber;
        RoomState = roomState;
    }
}