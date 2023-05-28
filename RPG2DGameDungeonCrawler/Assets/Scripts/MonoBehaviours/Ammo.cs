using UnityEngine;

public class Ammo : MonoBehaviour
{
	public int damageInflicted;

	void OnTriggerEnter2D(Collider2D collision)
	{
		//if (collision is BoxCollider2D)
		if (collision.gameObject.CompareTag("Enemy"))
		{
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();

			StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));

			gameObject.SetActive(false);
			print("dick");
		}
		if (collision.gameObject.CompareTag("Obstacle"))
		{
			Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
			print("Kolizja z " + obstacle.name);
		}
	}
}
