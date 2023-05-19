using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DungeonGenerator
{
    private DungeonController _controller;
    private uint _mainRooms;
    private uint _adjecentRooms;

    public DungeonGenerator(DungeonController controller)
    {
        _controller = controller;
    }

    public void GenerateRooms()
    {
        (_mainRooms, _adjecentRooms) = PartitionRooms();
        
        _controller.ClearAllRooms();
        GenerateMainRooms();
        GenerateAdjRooms();
    }

    private void GenerateMainRooms()
    {
        RoomComponent startingRoom = new RoomComponent(new Position(0, 0), RoomType.Start);
        RoomComponent current = startingRoom;
        _controller.AddRoom(startingRoom);
        uint generated = 1;

        do {
            RoomComponent room = GenerateNextRoom(current.Pos);
            if(room != null && _controller.CountNeighbours(room.Pos) <= 1)
            {
                _controller.AddRoom(room);
                current = room;
                generated++;
            }
        } while(generated < _mainRooms);

        RoomComponent lastRoom = _controller.GetRoom(_controller.CountRooms() - 1);
        lastRoom.Type = RoomType.Boss;
    }

    private void GenerateAdjRooms()
    {
        uint generated = 0;
        RoomComponent current;

        do {
            uint partialNumber = (uint) Math.Floor((double) UnityEngine.Random.Range(1, _adjecentRooms - generated));
            uint subGenerated = 0;
            int index = (int) Math.Floor((double) UnityEngine.Random.Range(0, _controller.CountRooms() - 1));
            current = _controller.GetRoom(index);
            do {
                RoomComponent room = GenerateNextRoom(current.Pos);
                if(room != null)
                {
                    _controller.AddRoom(room);
                    current = room;
                    subGenerated++;
                    generated++;
                }
            } while(subGenerated < partialNumber && _controller.CountNeighbours(current.Pos) <= 3);
        } while(generated < _adjecentRooms);
        
        RoomComponent lastRoom = _controller.GetRoom(_controller.CountRooms() - 1);
        lastRoom.Type = RoomType.Treasure;
    }

    private RoomComponent GenerateNextRoom(Position pos)
    {
        Array directions = Enum.GetValues(typeof(Directions));
        int index = UnityEngine.Random.Range(0, directions.Length - 1);
        int rDir = (int) directions.GetValue(index);

        if(!_controller.DoRoomExist(pos.x + rDir / 10, pos.y + rDir % 10))
            return new RoomComponent(new Position(pos.x + rDir / 10, pos.y + rDir % 10), RoomType.Normal);
        return null;
    }

    private (uint, uint) PartitionRooms()
    {
        uint total = _controller.Dungeon.RoomsNumber;
        double adjRoomsPercentage = 0.2;

        uint adj = (uint) Math.Floor(total * adjRoomsPercentage);
        uint main = total - adj;

        return (main, adj);
    }
}