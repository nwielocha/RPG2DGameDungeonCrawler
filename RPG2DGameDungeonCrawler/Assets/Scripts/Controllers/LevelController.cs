using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject RoomPrefab;
    public GameObject DoorPrefab;
    public GameObject ShopPrefab;
	public GameObject PauseCanvas { get; private set; }
    public static LevelController Instance;
    public List<GameObject> ObstaclesPrefab = new List<GameObject>();
    public List<GameObject> LootPrefabs = new List<GameObject>();
    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public List<GameObject> BossPrefabs = new List<GameObject>();
    public uint LevelNumber { get; private set; } = 0;
    public static GameObject MainGameObject;
    private DungeonComponent _dungeon;

    void Awake()
    {
        Instance = this;
    }

    void Start() 
    { 
        MainGameObject = gameObject;
        _dungeon = new DungeonComponent(this);
        PauseCanvas = GameObject.Find("PauseCanvas");
        UnPause();
        NextLevel();
    }
    
    public static void Pause()
    {
        Time.timeScale = 0;
        LevelController.Instance.PauseCanvas.SetActive(true);
    }

    public static void UnPause()
    {
        LevelController.Instance.PauseCanvas.SetActive(false);
        Time.timeScale = 1;
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