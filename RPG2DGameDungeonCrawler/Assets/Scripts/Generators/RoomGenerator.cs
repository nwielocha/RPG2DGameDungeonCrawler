using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    private RoomController _roomController;
    private GameObject _enemyPrefab; 
    public RoomGenerator(RoomController controller)
    {
        _roomController = controller;
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
                _roomController.DoorObjects.Add(door);
            }
        }

    }

    public void GenerateObstacles()
    {
        RoomComponent room = _roomController.Room;
        GameObject obstacle = RandomPrefab("ObstaclesPrefab");
        GameObject created = UnityEngine.Object.Instantiate(obstacle, new Vector3(room.Pos.x * RoomComponent.Width, room.Pos.y * RoomComponent.Height, 0), Quaternion.identity);
        _roomController.ObstacleObject = created;
    }

    public void GenerateTreasure()
    {

    }


    public GameObject RandomPrefab(string propName)
    {
        LevelController levelController = LevelController.MainGameObject.GetComponent<LevelController>();
        object obj = levelController.GetType().GetField(propName).GetValue(levelController);
        List<GameObject> arr = (List<GameObject>) obj; 
        int index = (int) UnityEngine.Random.Range(0, arr.Count - 1);
        return arr[index];
    }
    public void GenerateLoot()
    {
        GameObject loot = RandomPrefab("LootPrefabs");
        List<GameObject> spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner")).FindAll(g => g.transform.IsChildOf(_roomController.ObstacleObject.transform));
        int generated = 0;
        do{
            int randIndex = (int) UnityEngine.Random.Range(0, spawnPoints.Count - 1);
            var spawnPoint = spawnPoints[randIndex];

            if(spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn == null)
            {
                spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn = loot;
                _roomController.LootObject = spawnPoint;
                generated++;
            }

        }while(generated < 1);
        // getObstacleSpawnPoints // Pick it at random // SetSpawn after clearing room 
    }

    public void GenerateBoss()
    {   
    }

    public void GenerateStart()
    {

    }

    public void GenerateEnemies()
    {
        GameObject enemy = RandomPrefab("EnemyPrefabs");
        _enemyPrefab = enemy;
        List<GameObject> spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner")).FindAll(g => g.transform.IsChildOf(_roomController.ObstacleObject.transform));
        int randEnemyNumb = (int) UnityEngine.Random.Range(1, spawnPoints.Count - 2); 
        int generated = 0;

        do
        {
            int randIndex = (int) UnityEngine.Random.Range(0, spawnPoints.Count - 1);
            var spawnPoint = spawnPoints[randIndex];

            if(spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn == null)
            {
                spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn = enemy;
                var spawned = spawnPoint.GetComponent<SpawnPoint>().SpawnObject();
                spawned.GetComponent<Wander>().RmController = _roomController;
                spawned.GetComponent<Enemy>().RmController = _roomController;
                _roomController.EnemyObjects.Add(spawned);
                generated++;
            }

        } while(generated < randEnemyNumb);
    }

    public void RespawnEnemies()
    {
        List<GameObject> spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawner")).FindAll(g => g.transform.IsChildOf(_roomController.ObstacleObject.transform));
        int respawnNumber = (int) UnityEngine.Random.Range(0, spawnPoints.Count - 2);
        int respawned = 0;
        do{
            int randIndex = (int) UnityEngine.Random.Range(0, spawnPoints.Count - 1);
            var spawnPoint = spawnPoints[randIndex];
            if(spawnPoint.GetComponent<SpawnPoint>().PrefabToSpawn == _enemyPrefab)
            {
                var spawned = spawnPoint.GetComponent<SpawnPoint>().SpawnObject();
                spawned.GetComponent<Wander>().RmController = _roomController;
                spawned.GetComponent<Enemy>().RmController = _roomController;
                _roomController.EnemyObjects.Add(spawned);
                respawned++;
            }
        }while(respawned < respawnNumber);
    }
}