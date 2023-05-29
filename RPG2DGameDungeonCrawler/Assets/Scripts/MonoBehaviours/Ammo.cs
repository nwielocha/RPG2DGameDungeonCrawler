using UnityEngine;

public class Ammo : MonoBehaviour
{
	public int damageInflicted;
	Rigidbody2D rb2D;

	private void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision is BoxCollider2D && collision.gameObject.CompareTag("Enemy"))
		{
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();

			StartCoroutine(enemy.DamageCharacter(damageInflicted, 0.0f));

			gameObject.SetActive(false);
		}
		else if (collision.gameObject.CompareTag("Obstacle"))
		{
			print("kutrwaasssssss");
			gameObject.SetActive(false);
		}
	}
}
