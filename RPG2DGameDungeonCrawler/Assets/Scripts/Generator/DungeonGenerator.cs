using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Directions{
    Up = 1,
    Right = 10,
    Down = -1,
    Left = -10
}

public class DungeonGenerator : IGenerator, IObserver
{
    private const double _numberOfRoomsFactor = 2;
    private const int _numberOfRoomsConstant = 12;
    private const double _adjecentRoomsFactor = 0.20; 
    public int _numberOfRooms;
    private int _numberOfMainRooms;
    private int _numberOfAdjecentRooms;
    private List<Room> _managedRooms;
    private System.Random randomGenerator = new System.Random();

    public void UpdateObserver(ISubject subject){
        if(subject is Dungeon){
            (this._numberOfRooms, this._numberOfMainRooms, this._numberOfAdjecentRooms) = CalculateNumberOfRooms((subject as Dungeon).LevelNumber);
            Generate();
        }
    }

    private (int,int,int) CalculateNumberOfRooms(int level){
        int total = (int) Math.Floor(level * _numberOfRoomsFactor) + _numberOfRoomsConstant;
        int adjecent = (int) Math.Floor(total * _adjecentRoomsFactor);
        int main = total - adjecent;
        return (total, main, adjecent);
    }

    public void Generate(){
        // Creating starting room object
        Room startingRoom = new Room(0,0);
        
        // Clearing rooms list and adding to it starting room
        if(_managedRooms != null) _managedRooms = null;
        _managedRooms = new List<Room>();
        _managedRooms.Add(startingRoom);
        Room currentRoom = startingRoom;

        // Starting generation of main rooms
        int generated = 1;
        do{
            Room room = GenerateNext(currentRoom);
            if(room != null){
                currentRoom = room;
                _managedRooms.Add(room);
                generated++;
            }
        }while(generated < _numberOfMainRooms && CountNeighbours(currentRoom) <= 3);

        // Starting generation of adjecent rooms
        generated = 0;
        do{
            
            int adj = randomGenerator.Next(_numberOfAdjecentRooms - generated);
            currentRoom = _managedRooms[randomGenerator.Next(_managedRooms.Count)];
            int i = 0;
            do{
                Room room = GenerateNext(currentRoom);
                if(room != null){
                    currentRoom = room;
                    _managedRooms.Add(room);
                    i++;
                }
            }while(i < adj && CountNeighbours(currentRoom) <= 1);
            generated += i;
        }while(generated < _numberOfAdjecentRooms);

        // Placing rooms on game map
        foreach(Room room in _managedRooms){
            foreach(Directions direction in (Directions[]) Enum.GetValues(typeof(Directions))){
                AddDoor(room, direction);
            }
            room.PlaceOnMap();
        }
    }

    void AddDoor(Room room, Directions direction)
    {
        int x = room.XCoord;
        int y = room.YCoord;
        int dir = (int) direction;
        /*Room neighbour = _managedRooms.Find(r => (r.XCoord == x + dir / 10 && r.YCoord == y + dir % 10));
        if(neighbour != null && !neighbour.doors.Any(d => ((int) d.direction == -dir))){
            room.doors.Add(new Door(direction));
        }*/
        if(_managedRooms.Any(r => (r.XCoord == x + dir / 10 && r.YCoord == y + dir % 10))){
            room.doors.Add(new Door(direction));
        }
    }

    int CountNeighbours(Room currentRoom){
        int x = currentRoom.XCoord;
        int y = currentRoom.YCoord;
        return (_managedRooms.FindAll(r => (r.XCoord == x + 1 && r.YCoord == y) || (r.XCoord == x && r.YCoord == y + 1) || (r.XCoord == x - 1 && r.YCoord == y) || (r.XCoord == x && r.YCoord == y - 1))).Count;
    }

    public Room GenerateNext(Room currentRoom){
        Array directionValues = Enum.GetValues(typeof(Directions));
        int direction = (int) directionValues.GetValue(randomGenerator.Next(directionValues.Length));
        if(_managedRooms.Find(r => r.XCoord == currentRoom.XCoord + direction / 10  && r.YCoord == currentRoom.YCoord + direction % 10) != null) return null;
        return new Room(currentRoom.XCoord + direction / 10, currentRoom.YCoord + direction % 10);
    }
}
