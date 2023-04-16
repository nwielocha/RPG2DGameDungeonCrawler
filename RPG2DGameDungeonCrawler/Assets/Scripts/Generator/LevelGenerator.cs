using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class LevelGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private const int _roomHeight = 9;
    private const int _roomWidth = 15;
    private const double _roomsGeneratorNumber = 1;
    private const double _adjecentRoomsPercentage = 0.20;
    private const int _maxRoomCount = 20;
    private List<Room> _rooms = new List<Room>();
    public GameObject roomTile;
    public GameObject player;

    public enum RoomType
    {
        Start,
        Boss,
        Normal,
    }

    public enum RoomDirection
    {
        Top,
        Right,
        Down,
        Left
    }

    public class Room 
    { 
        public int XCord { get; } 
        public int YCord { get; } 
        public RoomType RoomType { get; set; }

        public Room(int xCord, int yCord, RoomType roomType)
        {
            this.XCord = xCord;
            this.YCord = yCord;
            this.RoomType = roomType;
        }
    }

    void Generate(int levelNumber)
    {
        int numberOfRooms = (int) Math.Floor(_roomsGeneratorNumber * levelNumber + 5);
        if (numberOfRooms > _maxRoomCount) numberOfRooms = _maxRoomCount;
        
        int numberOfAdjecentRooms = (int) Math.Floor(numberOfRooms * _adjecentRoomsPercentage);
        numberOfRooms -= numberOfAdjecentRooms;

        Room startingRoom = new Room(0,0,RoomType.Start);
        _rooms.Add(startingRoom);
        Room currentRoom = startingRoom;
        
        int roomsGenerated = 1;
        System.Random rnd = new System.Random();

        do
        {
            int nextRoomDirection = rnd.Next(4);
            if(nextRoomDirection == (int) RoomDirection.Top && _rooms.Find(room => room.YCord == currentRoom.YCord + 1) == null){
                Room newRoom = new Room(currentRoom.XCord, currentRoom.YCord + 1,RoomType.Normal);
                _rooms.Add(newRoom);
                currentRoom = newRoom;
                roomsGenerated++;  
            }
            else if(nextRoomDirection == (int) RoomDirection.Right && _rooms.Find(room => room.XCord == currentRoom.XCord + 1) == null){
                Room newRoom = new Room(currentRoom.XCord + 1, currentRoom.YCord,RoomType.Normal); 
                _rooms.Add(newRoom);
                currentRoom = newRoom;
                roomsGenerated++;   
            }
            else if(nextRoomDirection == (int) RoomDirection.Down && _rooms.Find(room => room.YCord == currentRoom.YCord - 1) == null){
                Room newRoom = new Room(currentRoom.XCord, currentRoom.YCord - 1,RoomType.Normal);  
                _rooms.Add(newRoom);
                currentRoom = newRoom;
                roomsGenerated++;   
            }
            else if(nextRoomDirection == (int) RoomDirection.Left && _rooms.Find(room => room.XCord == currentRoom.XCord - 1) == null){
                Room newRoom = new Room(currentRoom.XCord + 1, currentRoom.YCord,RoomType.Normal);  
                _rooms.Add(newRoom);
                currentRoom = newRoom;
                roomsGenerated++;   
            }
        } while (roomsGenerated < numberOfRooms);

       // _rooms.Last().RoomType = RoomType.Boss;

        Instantiate(player, new Vector2(0, 0), Quaternion.identity);
        foreach(Room room in _rooms){
            Instantiate(roomTile, new Vector2((_roomWidth - 1) * room.XCord, ( _roomHeight - 1 ) * room.YCord), Quaternion.identity);
        }

        roomsGenerated = 0;
        do{
            // int randomRoomIndex = rnd.Next(_rooms.Count());
        roomsGenerated++;
        }while(roomsGenerated < numberOfAdjecentRooms);
    }

    // Start is called before the first frame update
    void Start()
    {
        Generate(10);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
