using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public GameObject RoomTile;
    public GameObject DoorTile;

    public void RenderRoom(Room room){
        int w = room.RoomWidth;
        int h = room.RoomHeight;
        Instantiate(RoomTile, new Vector2(room.XCoord * w, room.YCoord * h), Quaternion.identity);
        RenderDoors(room);
    }

    public void RenderDoors(Room room){
        int w = room.RoomWidth;
        int h = room.RoomHeight;
        float offset = -0.5f;
        // GameObject instance;
        foreach(Door door in room.doors){
            switch(door.direction){
                case Directions.Up:{
                    var doorInst = Instantiate(DoorTile, new Vector2(room.XCoord * w + offset, room.YCoord * h + h/2 + offset), Quaternion.identity);
                    var doorScript = (DoorScript) doorInst.GetComponent("DoorScript");
                    doorScript.direction = Directions.Up;
                    room.AddDoor(doorInst);
                    break;
                }
                case Directions.Down: {
                    var doorInst = Instantiate(DoorTile, new Vector2(room.XCoord * w + offset, room.YCoord * h - h/2 - offset), Quaternion.identity);
                    var doorScript = (DoorScript) doorInst.GetComponent("DoorScript");
                    doorScript.direction = Directions.Down;
                    room.AddDoor(doorInst);
                    break;
                }
                case Directions.Right: {
                    var doorInst = Instantiate(DoorTile, new Vector2(room.XCoord * w + w/2 + offset, room.YCoord * h + offset), Quaternion.identity);
                    var doorScript = (DoorScript) doorInst.GetComponent("DoorScript");
                    doorScript.direction = Directions.Right;
                    room.AddDoor(doorInst);
                    break;
                }
                case Directions.Left: {
                    var doorInst = Instantiate(DoorTile, new Vector2(room.XCoord * w - w/2 + offset, room.YCoord * h + offset), Quaternion.identity);
                    var doorScript = (DoorScript) doorInst.GetComponent("DoorScript");
                    doorScript.direction = Directions.Left;
                    room.AddDoor(doorInst);
                    break;
                }
            }
        }
    }
}
