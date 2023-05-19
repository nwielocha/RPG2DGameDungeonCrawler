public class RoomComponent
{
    public Position Pos { get; private set; }
    public RoomType Type { get; set; }
    public const uint Width = 15;
    public const uint Height = 15;
 
    public RoomComponent(Position pos)
    {
        Pos = pos;
    }

    public RoomComponent(Position pos, RoomType type)
    {
        Pos = pos;
        Type = type;
    }
}