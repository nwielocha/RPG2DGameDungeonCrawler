using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Ammo"))
		{
			if (collision.gameObject.TryGetComponent<Ammo>(out var hitObject))
			{
				print("Kolizja: " + hitObject.name);
				collision.gameObject.SetActive(false);
			}
		}
	}
}
