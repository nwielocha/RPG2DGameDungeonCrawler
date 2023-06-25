using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonComponent
{
    public static DungeonComponent Instance;
    public int RoomsCount { get; private set; }
    public List<RoomComponent> Rooms { get; } = new List<RoomComponent>();

    public DungeonComponent()
    {
        Instance = this;
        new DungeonController();
    }

    private void CalcRoomsCount()
    {
        int levelConstant = 5;
        int levelNumber = LevelController.Instance.LevelNumber;
        int rand = (int)Math.Floor((double)UnityEngine.Random.Range(0, 3));
        double levelMultiplier = 1.25;

        RoomsCount = (int)Math.Floor((double)levelNumber * levelMultiplier) + levelConstant + rand;
    }

    public void Generate()
    {
        Clear();
        CalcRoomsCount();
        DungeonController.Instance.GenerateRooms();
        DungeonController.Instance.PlaceOnMap();
    }

    private void Clear()
    {
        // implement method for clearing all dependencies
    }
}
