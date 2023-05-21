using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : IInstancer
{
    private GameObject _roomPrefab;
    public DungeonComponent Dungeon { get; private set; }
    private DungeonGenerator _generator;

    public DungeonController(DungeonComponent dungeon)
    {
        _roomPrefab = LevelController.MainGameObject.GetComponent<LevelController>().RoomPrefab;
        Dungeon = dungeon;
        _generator = new DungeonGenerator(this);
    }

    public void AddRoom(RoomComponent room)
    {
        Dungeon.Rooms.Add(room);
    }

    public int CountRooms()
    {
        return Dungeon.Rooms.Count;
    }

    public RoomComponent GetRoom(int index)
    {
        return Dungeon.Rooms[index];
    }

    public void ClearAllRooms()
    {
        Dungeon.Rooms.Clear();
    }

    public void GenerateRooms()
    {
        _generator.GenerateRooms();
    }

    public bool DoRoomExist(int x, int y)
    {
        return Dungeon.Rooms.Any(r => r.Pos.x == x && r.Pos.y == y);
    }

    public uint CountNeighbours(Position pos)
    {
        int x = pos.x, y = pos.y;
        List<RoomComponent> allNeigbours = Dungeon.Rooms.FindAll(r => (r.Pos.x == x + 1 && r.Pos.y == y) || 
                                                                        (r.Pos.x == x - 1 && r.Pos.y == y) ||
                                                                        (r.Pos.x == x && r.Pos.y == y + 1) ||
                                                                        (r.Pos.x == x && r.Pos.y == y - 1));

        return (uint) allNeigbours.Count;
    }

    public bool DirectionNeigbour(RoomComponent room, Directions direction)
    {
        return Dungeon.Rooms.Any(r => r.Pos.x == room.Pos.x + (int) direction / 10 && r.Pos.y == room.Pos.y + (int) direction % 10);
    }

    public void PlaceOnMap()
    {
        foreach(RoomComponent r in Dungeon.Rooms){
            var room = UnityEngine.Object.Instantiate(_roomPrefab, new Vector3(RoomComponent.Width * r.Pos.x, RoomComponent.Height * r.Pos.y, 0), Quaternion.identity);
            var roomController = room.GetComponent<RoomController>();
            roomController.Room = r;
        }
    }
}