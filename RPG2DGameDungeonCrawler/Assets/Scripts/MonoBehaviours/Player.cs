using UnityEngine;

public class Player : Character 
{
	public HealthBar healthBarPrefab;
	HealthBar healthBar;
	public ManaBar manaBarPrefab;
	ManaBar manaBar;

	void Start()
	{
		hitPoints.value = startingHitPoints;
		manaPoints.value = startingManaPoints;

		healthBar = Instantiate(healthBarPrefab);
		manaBar = Instantiate(manaBarPrefab);

		healthBar.character = this;
		manaBar.character = this;
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
						shouldDisappear = true;
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
}
