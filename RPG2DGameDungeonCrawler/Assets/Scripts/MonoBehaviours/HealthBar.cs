using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public GameObject healthPrefab;
	public Player player;
	List<HealthHeart> hearts = new List<HealthHeart>();

	void Start()
	{
		DrawHearts();
	}

	public void DrawHearts()
	{
		ClearHearts();

		// Ustalic ile serc chcemy wyswietlac
		float maxHealthRemainder = player.maxHealth % 2;
		int heartsToMake = (int)((player.maxHealth / 2) + maxHealthRemainder);
		for (int i = 0; i < heartsToMake; i++)
		{
			CreateEmptyHeart();
		}

		for (int i = 0; i < hearts.Count; i++)
		{
			int heartStatusRemainder = (int)Mathf.Clamp(player.health - (i * 2), 0, 2);
			hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
		}

	}

	public void CreateEmptyHeart()
	{
		GameObject newHeart = Instantiate(healthPrefab);
		newHeart.transform.SetParent(transform);

		HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
		heartComponent.SetHeartImage(HeartStatus.EMPTY);
		hearts.Add(heartComponent);
	}

	public void ClearHearts()
	{
		foreach (Transform t in transform)
		{
			Destroy(t.gameObject);
		}

		hearts = new List<HealthHeart>();
	}
}
