using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
		if (collision.gameObject.CompareTag("CanBePickedUp"))
		{
			if (hitObject != null)
			{
				print("Kolizja: " +  hitObject.name);
				switch (hitObject.itemType)
				{
					case Item.ItemType.COIN:
						break;
					case Item.ItemType.HEALTH:
						AdjustHitPoints(hitObject.quantity);
						break;
					default: 
						break;

				}

				collision.gameObject.SetActive(false);
			}
		}
	}

	public void AdjustHitPoints(int amount)
	{
		hitPoints = hitPoints + amount;
		print("Nowe punkty: " + amount + ". Razem: " + hitPoints);
	}
}
