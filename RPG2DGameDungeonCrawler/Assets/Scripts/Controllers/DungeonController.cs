using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : IInstancer
{
    public static DungeonController Instance;

    public DungeonController()
    {
        Instance = this;
        new DungeonGenerator();
    }

    public void AddRoom(RoomComponent room)
    {
        DungeonComponent.Instance.Rooms.Add(room);
    }

    public int CountRooms()
    {
        return DungeonComponent.Instance.Rooms.Count;
    }

    public RoomComponent GetRoom(int index)
    {
        return DungeonComponent.Instance.Rooms[index];
    }

    public void GenerateRooms()
    {
        DungeonGenerator.Instance.GenerateRooms();
    }

    public bool DoRoomExist(int x, int y)
    {
        return DungeonComponent.Instance.Rooms.Any(r => r.Pos.x == x && r.Pos.y == y);
    }

    public int CountNeighbours(Position pos)
    {
        int x = pos.x,
            y = pos.y;

        List<RoomComponent> allNeigbours = DungeonComponent.Instance.Rooms.FindAll(
            r =>
                (r.Pos.x == x + 1 && r.Pos.y == y)
                || (r.Pos.x == x - 1 && r.Pos.y == y)
                || (r.Pos.x == x && r.Pos.y == y + 1)
                || (r.Pos.x == x && r.Pos.y == y - 1)
        );

        return allNeigbours.Count;
    }

    public bool NeighbourAtDirection(RoomComponent room, Directions direction)
    {
        return DungeonComponent.Instance.Rooms.Any(
            r =>
                r.Pos.x == room.Pos.x + (int)direction / 10
                && r.Pos.y == room.Pos.y + (int)direction % 10
        );
    }

    public void PlaceOnMap()
    {
        foreach (RoomComponent r in DungeonComponent.Instance.Rooms)
        {
            GameObject roomPrefab = LevelController.Instance.RoomPrefab;

            var room = UnityEngine.Object.Instantiate(
                roomPrefab,
                new Vector3(RoomComponent.Width * r.Pos.x, RoomComponent.Height * r.Pos.y, 0),
                Quaternion.identity
            );

            var roomController = room.GetComponent<RoomController>();
            roomController.Room = r;
        }
    }
}
