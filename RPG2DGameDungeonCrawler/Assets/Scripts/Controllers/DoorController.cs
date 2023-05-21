using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Directions Direction { get; private set; }
    public Sprite Door;
    public Sprite DoorFrame; 

    public void DefineDirection(Directions direction)
    {
        Direction = direction;
        float offset = 0.5f;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (Direction)
        {
            case Directions.Up:
                spriteRenderer.sprite = Door;
                gameObject.transform.Translate(new Vector3(0, ((float)(RoomComponent.Height / 2) - offset), 0));
            break;
            case Directions.Down:
                spriteRenderer.sprite = DoorFrame;
                gameObject.transform.Translate(new Vector3(0, -((float)(RoomComponent.Height / 2) - offset), 0));
            break;
            case Directions.Right:
                spriteRenderer.sprite = DoorFrame;
                gameObject.transform.Translate(new Vector3(((float)(RoomComponent.Width / 2) - offset), 0 ,0));
                gameObject.transform.Rotate(0f, 0f, 90f, Space.Self);
            break;
            case Directions.Left:
                spriteRenderer.sprite = DoorFrame;
                gameObject.transform.Translate(new Vector3(-((float)(RoomComponent.Width / 2) + offset), 0 ,0));
                gameObject.transform.Rotate(0f, 0f, -90f, Space.Self);
            break;

        }
    }
}