using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed;
    private Vector3 newPos, initPos, targetPos;
    float newX, newY, newPlayerX, newPlayerY, elapsed = 0, duration = 1f;
    bool active;

    void Start()
    {
        newPos = initPos;
    }

    void Update()
    {   
        if(active)
        {
            elapsed += Time.deltaTime;
            float completion = elapsed / duration;
            transform.position = Vector3.Lerp(initPos, targetPos, completion);
            if(transform.position == targetPos){
                active = false;
                elapsed = 0;
                player.transform.position = new Vector3(
                    newPlayerX,
                    newPlayerY,
                    player.transform.position.z
                );
            } 
        }
    }

    public void Translate(Directions direction)
    {
        active = true;
        initPos = transform.position;
        
        switch(direction){
            case Directions.Up:
                newX = initPos.x;
                newY = initPos.y + (float) Room.baseRoomHeight;
                newPlayerX = player.transform.position.x;
                newPlayerY = player.transform.position.y + 3;
            break;
            case Directions.Down:
                newX = initPos.x;
                newY = initPos.y - (float) Room.baseRoomHeight;
                newPlayerX = player.transform.position.x;
                newPlayerY = player.transform.position.y - 3;
            break;
            case Directions.Right:
                newX = initPos.x + (float) Room.baseRoomWidth;
                newY = initPos.y;
                newPlayerX = player.transform.position.x + 2;
                newPlayerY = player.transform.position.y;
            break;
            case Directions.Left:
                newX = initPos.x - (float) Room.baseRoomWidth; 
                newY = initPos.y;
                newPlayerX = player.transform.position.x - 2;
                newPlayerY = player.transform.position.y;
            break;
        }

        targetPos = new Vector3(
            newX,
            newY,
            initPos.z
        );
    }
}
