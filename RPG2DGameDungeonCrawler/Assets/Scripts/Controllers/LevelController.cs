using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public GameObject RoomPrefab;
    public GameObject DoorPrefab;
    public GameObject ShopPrefab;
    public GameObject PauseCanvas;
    public List<GameObject> ObstaclesPrefab = new List<GameObject>();
    public List<GameObject> LootPrefabs = new List<GameObject>();
    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public List<GameObject> BossPrefabs = new List<GameObject>();
    public List<GameObject> ShopItemPrefabs = new List<GameObject>();
    public int LevelNumber { get; private set; } = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        new DungeonComponent();
        LevelController.Instance.PauseCanvas.SetActive(false);
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0, 0, 0);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.position = new Vector3(-0.5f, 0.5f, -10);
        DungeonComponent.Instance.Generate();
    }
}
