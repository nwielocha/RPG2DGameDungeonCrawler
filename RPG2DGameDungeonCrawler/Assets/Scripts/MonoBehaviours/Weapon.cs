using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameObject ammoPrefab;
	public int poolSize;
	static List<GameObject> ammoPool;

	void Awake()
	{
		if (ammoPool == null)
		{
			ammoPool = new List<GameObject>();
		}

		for (int i = 0; i < poolSize; i++)
		{
			GameObject ammoObject = Instantiate(ammoPrefab);
			ammoObject.SetActive(false);
			ammoPool.Add(ammoObject);
		}
		
	}

	void Update()
	{
		//float shootHorizontal = Input.GetAxis("ShootHorizontal");
		//float shootVertical = Input.GetAxis("ShootVertical");
		if (Input.GetMouseButtonDown(0))
		{
			FireAmmo();
		}
	}

	GameObject SpawnAmmo(Vector3 location)
	{
		foreach (GameObject ammo in ammoPool)
		{
			if (ammo.activeSelf == false)
			{
				ammo.SetActive(true);
				ammo.transform.position = location;

				return ammo;
			}
		}

		return null;
	}

	void FireAmmo()
	{

	}

	void OnDestroy()
	{
		ammoPool = null;
	}
}
