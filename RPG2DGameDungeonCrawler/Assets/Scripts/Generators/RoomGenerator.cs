using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    private RoomController _roomController;
    private GameObject _enemyPrefab;
    private GameObject _shopItemPrefab;

    public RoomGenerator(RoomController controller)
    {
        _roomController = controller;
    }

    public void GenerateDoors()
    {
        GameObject doorPrefab = LevelController.Instance.DoorPrefab;
        RoomComponent room = _roomController.Room;

        foreach (Directions direction in Enum.GetValues(typeof(Directions)))
        {
            if (DungeonController.Instance.NeighbourAtDirection(room, direction))
            {
                GameObject door = UnityEngine.Object.Instantiate(
                    doorPrefab,
                    new Vector3(
                        room.Pos.x * RoomComponent.Width,
                        room.Pos.y * RoomComponent.Height,
                        0
                    ),
                    Quaternion.identity
                );
                door.GetComponent<DoorController>().DefineDirection(direction);
                _roomController.DoorObjects.Add(door);
            }
        }
    }

    public GameObject RandomPrefab(string propName)
    {
        object obj = LevelController.Instance
            .GetType()
            .GetField(propName)
            .GetValue(LevelController.Instance);
        List<GameObject> arr = (List<GameObject>)obj;
        int index = (int)UnityEngine.Random.Range(0, arr.Count);

        return arr[index];
    }

    public void GenerateObstacles()
    {
        RoomComponent room = _roomController.Room;
        GameObject obstacle = RandomPrefab("ObstaclesPrefab");
        GameObject created = UnityEngine.Object.Instantiate(
            obstacle,
            new Vector3(room.Pos.x * RoomComponent.Width, room.Pos.y * RoomComponent.Height, 0),
            Quaternion.identity
        );
        _roomController.ObstacleObject = created;
    }

    public void GenerateTreasure()
    {
        RoomComponent room = _roomController.Room;
        GameObject shop = LevelController.Instance.ShopPrefab;
        GameObject potionItem = RandomPrefab("ShopItemPrefabs");
        GameObject created = UnityEngine.Object.Instantiate(
            shop,
            new Vector3(room.Pos.x * RoomComponent.Width, room.Pos.y * RoomComponent.Height, 0),
            Quaternion.identity
        );
        GameObject potion = created.transform.Find("PotionShopItem").gameObject;
        GameObject heart = created.transform.Find("HeartShopItem").gameObject;
        ShopItemController potionController = potion.GetComponent<ShopItemController>();
        ShopItemController heartController = heart.GetComponent<ShopItemController>();
        float levelMultiplier = 0.75f;
        int levelNumber = LevelController.Instance.LevelNumber;
        int priceBuff = (int)Math.Floor((double)(levelNumber - 1) * levelMultiplier);
        potionController.Price += priceBuff;
        potionController.Item = potionItem;
        heartController.Price += priceBuff;

        _roomController.ShopObject = created;
    }

    public void GenerateLoot()
    {
        GameObject loot = RandomPrefab("LootPrefabs");
        List<GameObject> spawnPoints = new List<GameObject>(
            GameObject.FindGameObjectsWithTag("Spawner")
        ).FindAll(g => g.transform.IsChildOf(_roomController.ObstacleObject.transform));
        int generated = 0;

        do
        {
            int randIndex = (int)UnityEngine.Random.Range(0, spawnPoints.Count - 1);
            var spawnPoint = spawnPoints[randIndex];

            if (spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn == null)
            {
                spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn = loot;
                _roomController.LootObject = spawnPoint;
                generated++;
            }
        } while (generated < 1);
    }

    public void GenerateBoss()
    {
        RoomComponent room = _roomController.Room;
        GameObject boss = RandomPrefab("BossPrefabs");
        GameObject created = UnityEngine.Object.Instantiate(
            boss,
            new Vector3(room.Pos.x * RoomComponent.Width, room.Pos.y * RoomComponent.Height, 0),
            Quaternion.identity
        );
        created.GetComponent<Wander>().RmController = _roomController;
        created.GetComponent<Enemy>().RmController = _roomController;
        int levelNumber = LevelController.Instance.LevelNumber;
        int healthBuff = levelNumber / 3;
        float damageBuffChange = (int)UnityEngine.Random.Range(0, 50);
        int damageBuff = 0;
        if (levelNumber > damageBuffChange)
            damageBuff = levelNumber / 40 + 1;

        created.GetComponent<Enemy>().damageStrength += damageBuff;
        created.GetComponent<Enemy>().MaxHitPoints += healthBuff;
        created.GetComponent<Enemy>().StartingHitPoints += healthBuff;

        _roomController.BossObject = created;
    }

    public void GenerateEnemies()
    {
        GameObject enemy = RandomPrefab("EnemyPrefabs");
        _enemyPrefab = enemy;
        List<GameObject> spawnPoints = new List<GameObject>(
            GameObject.FindGameObjectsWithTag("Spawner")
        ).FindAll(g => g.transform.IsChildOf(_roomController.ObstacleObject.transform));
        int randEnemyNumb = (int)UnityEngine.Random.Range(1, spawnPoints.Count - 2);
        int generated = 0;

        do
        {
            int randIndex = (int)UnityEngine.Random.Range(0, spawnPoints.Count - 1);
            var spawnPoint = spawnPoints[randIndex];

            if (spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn == null)
            {
                spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn = enemy;
                var spawned = spawnPoint.GetComponent<SpawnPoint>().SpawnObject();
                spawned.GetComponent<Wander>().RmController = _roomController;
                spawned.GetComponent<Enemy>().RmController = _roomController;
                int levelNumber = LevelController.Instance.LevelNumber;
                float healthBuffChance = (int)UnityEngine.Random.Range(0, 15);
                float damageBuffChance = (int)UnityEngine.Random.Range(0, 15);
                int damageBuff = 0,
                    healthBuff = 0;
                if (levelNumber > damageBuffChance)
                    damageBuff = levelNumber / 120 + 1;
                if (levelNumber > healthBuffChance)
                    healthBuff = levelNumber / 50 + (int)UnityEngine.Random.Range(0, 4);
                spawned.GetComponent<Enemy>().damageStrength += damageBuff;
                spawned.GetComponent<Enemy>().MaxHitPoints += healthBuff;
                spawned.GetComponent<Enemy>().StartingHitPoints += healthBuff;
                _roomController.EnemyObjects.Add(spawned);
                generated++;
            }
        } while (generated < randEnemyNumb);
    }

    public void RespawnEnemies()
    {
        List<GameObject> spawnPoints = new List<GameObject>(
            GameObject.FindGameObjectsWithTag("Spawner")
        ).FindAll(g => g.transform.IsChildOf(_roomController.ObstacleObject.transform));
        int respawnNumber = (int)UnityEngine.Random.Range(0, spawnPoints.Count - 2);
        int respawned = 0;

        do
        {
            int randIndex = (int)UnityEngine.Random.Range(0, spawnPoints.Count - 1);
            var spawnPoint = spawnPoints[randIndex];
            if (spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn == _enemyPrefab)
            {
                var spawned = spawnPoint.GetComponent<SpawnPoint>().SpawnObject();
                spawned.GetComponent<Wander>().RmController = _roomController;
                spawned.GetComponent<Enemy>().RmController = _roomController;
                _roomController.EnemyObjects.Add(spawned);
                respawned++;
            }
        } while (respawned < respawnNumber);
    }
}
