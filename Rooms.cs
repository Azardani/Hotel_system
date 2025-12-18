namespace App
{
    public enum RoomStateEnum
    {
        Available,
        Unavailable,
        Maintained
    }

    public class Room
    {
        public string RoomNr;
        public RoomStateEnum RoomState;

        public string GuestName;

        public Room(string roomNumber, RoomStateEnum roomState = RoomStateEnum.Available)
        {
            RoomNr = roomNumber;
            RoomState = roomState;
            GuestName = null;
        }

    }
}

 
