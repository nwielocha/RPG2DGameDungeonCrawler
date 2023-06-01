using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	public RoomComponent Room { get; set; }
	//public List<GameObject> RoomObjects { get; private set; } = new List<GameObject>();
	public GameObject ObstacleObject { get; set; }
	public GameObject LootObject { get; set; }
	public List<GameObject> DoorObjects { get; } = new List<GameObject>();
	public List<GameObject> EnemyObjects { get; } = new List<GameObject>();
	private float _timeToRespawn = 10;
	private bool _wasCleared = false;
	private float _time;
	public bool IsPlayerPresent { get; private set; }
	private RoomGenerator _roomGenerator;


	void Start()
	{
		_roomGenerator = new RoomGenerator(this);
		_timeToRespawn = CalculateRespawnTime();
		_roomGenerator.GenerateDoors();

		if (Room.Type == RoomType.Start)
			_roomGenerator.GenerateStart();
		else if (Room.Type == RoomType.Boss)
			_roomGenerator.GenerateBoss();
		else if (Room.Type == RoomType.Treasure)
			_roomGenerator.GenerateTreasure();
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
			// something not working with counting time
			_time += Time.deltaTime;
			if (_time >= _timeToRespawn)
			{
				_roomGenerator.RespawnEnemies();
			}
		}
	}

	private bool CheckPlayerPresence()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player != null)
		{
			var pos = player.transform.position;
			float w = (float)RoomComponent.Width;
			float h = (float)RoomComponent.Height;

			if ((pos.x <= (float)Room.Pos.x * w + w / 2) &&
			(pos.x >= (float)Room.Pos.x * w - w / 2) &&
			(pos.y <= (float)Room.Pos.y * h + h / 2) &&
			(pos.y >= (float)Room.Pos.y * h - h / 2)) return true;
		}
		return false;
	}

	private float CalculateRespawnTime()
	{
		uint levelNumber = LevelController.MainGameObject.GetComponent<LevelController>().LevelNumber;

		return (float)(180 + 30 * levelNumber);
	}

	public void DeleteRoomObject()
	{
		// delete other dependent objects
		GameObject.Destroy(gameObject);
	}
}
