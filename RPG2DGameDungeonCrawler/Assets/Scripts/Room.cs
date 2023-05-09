using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType{
    Empty,
    Default,
    Start,
    Boss,
}

public struct Door{
    public Door(Directions direction){
        this.direction = direction;
    }
    public Directions direction;
}

public class Room
{
    public int XCoord {get; set;}
    public int YCoord {get; set;}
    public int RoomWidth {get; set;}
    public int RoomHeight {get; set;}
    public bool Cleared {get; set; }
    public List<Door> doors {get; set;}
    public List<GameObject> doorInstances {get; set;}
    public const int baseRoomWidth = 15;
    public const int baseRoomHeight = 9;
    private RoomController _controller;
    private RoomGenerator _generator;

    public Room(int xCoord, int yCoord, int roomWidth = baseRoomWidth, int roomHeight = baseRoomHeight){
        this.XCoord = xCoord;
        this.YCoord = yCoord;
        this.RoomWidth = roomWidth;
        this.RoomHeight = roomHeight;
        this._controller = Level.currentGameObject.GetComponent<RoomController>();
        Cleared = true;
        this.doors = new List<Door>();
        this.doorInstances = new List<GameObject>();
        this._generator = new RoomGenerator(this);
    }


    public void AddDoor(GameObject door){
        doorInstances.Add(door);
        // var doorTrigger = (DoorTrigger) door.GetComponent("DoorTrigger");
        // doorTrigger.isUnlocked = true;
    }

    public void PlaceOnMap(){
        _controller.RenderRoom(this);
    }
}
