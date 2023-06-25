using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private float _timeToRespawn,
        _time;
    private bool _wasCleared = false;
    public bool IsPlayerPresent { get; private set; }
    private RoomGenerator _roomGenerator;
    public RoomComponent Room { get; set; }
    public GameObject ObstacleObject { get; set; }
    public GameObject LootObject { get; set; }
    public List<GameObject> DoorObjects { get; } = new List<GameObject>();
    public List<GameObject> EnemyObjects { get; } = new List<GameObject>();

    void Start()
    {
        _roomGenerator = new RoomGenerator(this);
        _timeToRespawn = CalculateRespawnTime();
        _roomGenerator.GenerateDoors();

        if (Room.Type == RoomType.Boss)
        {
            _roomGenerator.GenerateBoss();
        }
        else if (Room.Type == RoomType.Shop)
        {
            _roomGenerator.GenerateTreasure();
        }
        else if (Room.Type == RoomType.Normal)
        {
            _roomGenerator.GenerateObstacles();
            _roomGenerator.GenerateEnemies();
            _roomGenerator.GenerateLoot();
        }
    }

    void Update()
    {
        IsPlayerPresent = CheckPlayerPresence();
        if (EnemyObjects.Count == 0 && Room.Type == RoomType.Normal && !_wasCleared)
        {
            if (LootObject != null)
            {
                LootObject.GetComponent<SpawnPoint>().SpawnObject();
            }
            _wasCleared = true;
        }
        if (!IsPlayerPresent && EnemyObjects.Count <= 0 && Room.Type == RoomType.Normal)
        {
            _time += Time.deltaTime;
            if (_time >= _timeToRespawn)
            {
                _roomGenerator.RespawnEnemies();
            }
        }
    }

    private float CalculateRespawnTime()
    {
        int respawnTimeConst = 10;
        int respawnTimeMultiplier = 15;
        int levelNumber = LevelController.Instance.LevelNumber;

        return (float)(respawnTimeConst + respawnTimeMultiplier / levelNumber);
    }

    private bool CheckPlayerPresence()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            float w = (float)RoomComponent.Width;
            float h = (float)RoomComponent.Height;

            if (
                (pos.x <= (float)Room.Pos.x * w + w / 2)
                && (pos.x >= (float)Room.Pos.x * w - w / 2)
                && (pos.y <= (float)Room.Pos.y * h + h / 2)
                && (pos.y >= (float)Room.Pos.y * h - h / 2)
            )
            {
                return true;
            }
        }
        return false;
    }

    public void DeleteRoomObject()
    {
        // delete other dependent objects
        GameObject.Destroy(gameObject);
    }
}
