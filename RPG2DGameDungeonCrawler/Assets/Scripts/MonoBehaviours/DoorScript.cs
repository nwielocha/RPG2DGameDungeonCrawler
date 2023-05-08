using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Directions direction {get; set;}
    public Sprite DoorSprite;
    public Sprite FrameSprite;

    void Start()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch(direction){
            case Directions.Up:
                spriteRenderer.sprite = DoorSprite;
                break;
            case Directions.Down:
                spriteRenderer.sprite = FrameSprite;
                break;
            case Directions.Right:
                spriteRenderer.sprite = FrameSprite;
                gameObject.transform.Rotate(0f, 0f, 90f, Space.Self);
                break;
            case Directions.Left:
                spriteRenderer.sprite = FrameSprite;
                gameObject.transform.Rotate(0f, 0f, -90f, Space.Self);
                break;
        }
    }
}