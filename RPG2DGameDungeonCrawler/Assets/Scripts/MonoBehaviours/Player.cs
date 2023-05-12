using System.Collections;
using UnityEngine;

public class Player : Character 
{
	public Points hitPoints;
	public Points manaPoints;
	public HealthBar healthBarPrefab;
	public ManaBar manaBarPrefab;
	public Inventory inventoryPrefab;
	HealthBar healthBar;
	ManaBar manaBar;
	Inventory inventory;

	private void OnEnable()
	{
		ResetCharacter();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("CanBePickedUp"))
		{
			Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
			if (hitObject != null)
			{
				print("Kolizja: " +  hitObject.name);
				bool shouldDisappear = false;
				switch (hitObject.itemType)
				{
					case Item.ItemType.COIN:
						shouldDisappear = inventory.AddItem(hitObject);
						break;
					case Item.ItemType.HEALTH:
						shouldDisappear = AdjustHitPoints(hitObject.quantity);
						break;
					case Item.ItemType.MANA:
						shouldDisappear = AdjustManaPoints(hitObject.quantity);
						break;
					default: 
						break;
				}

				if (shouldDisappear)
				{
					collision.gameObject.SetActive(false);
				}
			}
		}
	}

	public bool AdjustHitPoints(int amount)
	{
		if (hitPoints.value < maxHitPoints)
		{
			hitPoints.value += amount;
			print("Nowe punkty: " + amount + ". Razem: " + hitPoints.value);

			return true;
		}
		
		return false;
	}

	public bool AdjustManaPoints(int amount)
	{
		if (manaPoints.value < maxManaPoints)
		{
			manaPoints.value += amount;
			print("Nowe punkty: " + amount + ". Razem: " + hitPoints.value);

			return true;
		}

		return false;
	}

	public override void ResetCharacter()
	{
		inventory = Instantiate(inventoryPrefab);
		healthBar = Instantiate(healthBarPrefab);
		manaBar = Instantiate(manaBarPrefab);

		healthBar.character = this;
		manaBar.character = this;
		
		hitPoints.value = startingHitPoints;
		manaPoints.value = startingManaPoints;
	}

	public override IEnumerator DamageCharacter(int damage, float interval)
	{
		while (true)
		{
			hitPoints.value -= damage;
			if (hitPoints.value < float.Epsilon)
			{
				KillCharacter();
				break;
			}

			if (interval > float.Epsilon)
				yield return new WaitForSeconds(interval);
			else
				break;
		}
	}

	public override void KillCharacter()
	{
		base.KillCharacter();
		Destroy(healthBar.gameObject);
		Destroy(manaBar.gameObject);
		Destroy(inventory.gameObject);
	}
}
