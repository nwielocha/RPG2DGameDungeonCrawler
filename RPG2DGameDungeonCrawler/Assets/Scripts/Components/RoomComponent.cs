public class RoomComponent
{
    public DungeonComponent Dungeon { get; private set; }
    public Position Pos { get; private set; }
    public RoomType Type { get; set; }
    public const uint Width = 15;
    public const uint Height = 9;
 
    public RoomComponent(Position pos, DungeonComponent dun)
    {
        Pos = pos;
        Dungeon = dun;
    }

    public RoomComponent(Position pos, DungeonComponent dun, RoomType type)
    {
        Pos = pos;
        Type = type;
        Dungeon = dun;
    }
}