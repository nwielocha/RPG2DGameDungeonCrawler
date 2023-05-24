using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	public GameObject PrefabToSpawn { get; set; }
	public float repeatInterval = 0;
	public RoomController RmController { get; set; }

	public void Start()
	{
		if (repeatInterval > 0)
		{
			InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
		}
	}

	public void SpawnEnemyLogic()
	{

	}

	public void SpawnLootLogic()
	{

	}

	public GameObject SpawnObject()
	{
		if (PrefabToSpawn != null)
		{
			return Instantiate(PrefabToSpawn, transform.position, Quaternion.identity);
		}

		return null;
	}
}
