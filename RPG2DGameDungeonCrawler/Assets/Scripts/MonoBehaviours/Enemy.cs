using System.Collections;
using UnityEngine;

public class Enemy : Character 
{
	public RoomController RmController { get; set; }
	public int damageStrength;
	public EnemyType enemyType;
	public GameObject ammoPrefab;
	Coroutine damageCoroutine;
	float hitPoints;

	private void OnEnable()
	{
		ResetCharacter();
	}

	public override IEnumerator DamageCharacter(int damage, float interval)
	{
		while (true)
		{
			StartCoroutine(FlickerCharacter());

			hitPoints -= damage;
			if (hitPoints <= float.Epsilon) // najmniejsza liczba wieksza od zera
			{
				KillCharacter();
				RmController.EnemyObjects.Remove(gameObject);
				break;
			}

			if (interval > float.Epsilon)
				yield return new WaitForSeconds(interval);
			else
				break;
		}
	}

	public override void ResetCharacter()
	{
		hitPoints = startingHitPoints;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			print("Kolizja z " +  collision.gameObject.name);
			Player player = collision.gameObject.GetComponent<Player>();
			if (damageCoroutine == null)
			{
				damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if (damageCoroutine != null)
			{
				StopCoroutine(damageCoroutine);
				damageCoroutine = null;
			}
		}
	}
}
