using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonComponent
{
    public uint RoomsNumber { get; private set; }
    public List<RoomComponent> Rooms { get; private set; } = new List<RoomComponent>();
    private LevelController _levelController;
    public DungeonController Controller { get; private set; }

    public DungeonComponent(LevelController levelController)
    {
        _levelController = levelController;
        Controller = new DungeonController(this);
    }

    public uint CalcRoomsNumber()
    {    
        uint levelConstant = 5;
        uint levelNumber = _levelController.LevelNumber;
        uint rand = (uint) Math.Floor((double) UnityEngine.Random.Range(0, 3));
        double levelMultiplier = 1.25;
        
        return (uint) Math.Floor((double) levelNumber * levelMultiplier) + levelConstant + rand;
    }

    public void Generate()
    {
        RoomsNumber = CalcRoomsNumber();
        Controller.GenerateRooms();
        Controller.PlaceOnMap();
    }
}