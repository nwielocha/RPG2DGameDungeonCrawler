using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject RoomPrefab;
    public GameObject DoorPrefabs;
    public GameObject ObstaclePrefab;
    public List<GameObject> LootPrefabs = new List<GameObject>();
    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public List<GameObject> BossPrefabs = new List<GameObject>();
    public uint LevelNumber { get; private set; } = 0;
    private DungeonComponent _dungeon;

    void Start() 
    { 
        _dungeon = new DungeonComponent(this);
        NextLevel();
    }
    
    public void NextLevel() 
    {
        LevelNumber++;
        if(LevelNumber == uint.MaxValue)
        {
            // handle running out of range
        }
        
        _dungeon.Generate();
    }
}