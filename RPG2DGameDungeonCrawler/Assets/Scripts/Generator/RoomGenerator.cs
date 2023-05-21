using System;
using UnityEngine;

public class RoomGenerator
{
    private RoomController _roomController;

    public RoomGenerator(RoomController controller)
    {
        _roomController = controller;
    } 

    public void Generate()
    {
        GenerateDoors();
    }

    public void GenerateDoors()
    {
        GameObject doorPrefab = LevelController.MainGameObject.GetComponent<LevelController>().DoorPrefab;
        RoomComponent room = _roomController.Room;
        DungeonController dungeonController = _roomController.Room.Dungeon.Controller;
        
        foreach(Directions direction in Enum.GetValues(typeof(Directions)))
        {
            if(dungeonController.DirectionNeigbour(room, direction))
            {
                GameObject door = UnityEngine.Object.Instantiate(doorPrefab, new Vector3(room.Pos.x * RoomComponent.Width, room.Pos.y * RoomComponent.Height, 0), Quaternion.identity);
                door.GetComponent<DoorController>().DefineDirection(direction);
            }
        }

    }

    public void AlignDoor(GameObject door)
    {

    }

    public void GenerateObstacles()
    {

    }

    public GameObject RandomizeLoot()
    {
        return new GameObject();
    }

    public GameObject RandomizeEnemies()
    {   
        return new GameObject();
    }
}