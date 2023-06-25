public class RoomComponent
{
    public const int Width = 15;
    public const int Height = 9;
    public Position Pos { get; private set; }
    public RoomType Type { get; set; }
    public RoomController Controller { get; set; }

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
