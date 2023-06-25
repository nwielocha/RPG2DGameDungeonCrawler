using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator
{
    public static DungeonGenerator Instance;
    private int _mainRooms;
    private int _adjecentRooms;

    public DungeonGenerator()
    {
        Instance = this;
    }

    public void GenerateRooms()
    {
        (_mainRooms, _adjecentRooms) = PartitionRooms();
        GenerateMainRooms();
        GenerateAdjRooms();
    }

    private void GenerateMainRooms()
    {
        RoomComponent startingRoom = new RoomComponent(new Position(0, 0), RoomType.Start);
        RoomComponent currentRoom = startingRoom;
        DungeonController.Instance.AddRoom(startingRoom);

        int generated = 1;
        do
        {
            RoomComponent room = GenerateNextRoom(currentRoom.Pos);
            if (room != null && DungeonController.Instance.CountNeighbours(room.Pos) <= 1)
            {
                DungeonController.Instance.AddRoom(room);
                currentRoom = room;
                generated++;

                if (generated == _mainRooms)
                {
                    currentRoom.Type = RoomType.Boss;
                }
            }
        } while (generated < _mainRooms);
    }

    private void GenerateAdjRooms()
    {
        RoomComponent currentRoom = null;
        int generated = 0;
        do
        {
            int subGroupRooms = (int)
                Math.Floor((double)UnityEngine.Random.Range(1, _adjecentRooms - generated));
            int randIndex = (int)
                Math.Floor(
                    (double)UnityEngine.Random.Range(0, DungeonController.Instance.CountRooms() - 1)
                );
            currentRoom = DungeonController.Instance.GetRoom(randIndex);

            int subGenerated = 0;
            do
            {
                RoomComponent room = GenerateNextRoom(currentRoom.Pos);
                if (room != null)
                {
                    DungeonController.Instance.AddRoom(room);
                    currentRoom = room;
                    subGenerated++;
                    generated++;
                    if (generated == _adjecentRooms)
                    {
                        currentRoom.Type = RoomType.Shop;
                    }
                }
            } while (
                subGenerated < subGroupRooms
                && DungeonController.Instance.CountNeighbours(currentRoom.Pos) <= 3
            );
        } while (generated < _adjecentRooms);
    }

    private RoomComponent GenerateNextRoom(Position pos)
    {
        Array directions = Enum.GetValues(typeof(Directions));
        int randIndex = UnityEngine.Random.Range(0, directions.Length - 1);
        int randDir = (int)directions.GetValue(randIndex);

        if (!DungeonController.Instance.DoRoomExist(pos.x + randDir / 10, pos.y + randDir % 10))
        {
            return new RoomComponent(
                new Position(pos.x + randDir / 10, pos.y + randDir % 10),
                RoomType.Normal
            );
        }

        return null;
    }

    private (int, int) PartitionRooms()
    {
        double adjRoomsPercentage = 0.2;
        int total = DungeonComponent.Instance.RoomsCount;
        int adj = (int)Math.Floor(total * adjRoomsPercentage);
        int main = total - adj;

        return (main, adj);
    }
}
