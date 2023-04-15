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
				collision.gameObject.SetActive(false);
			}
		}
	}
}
