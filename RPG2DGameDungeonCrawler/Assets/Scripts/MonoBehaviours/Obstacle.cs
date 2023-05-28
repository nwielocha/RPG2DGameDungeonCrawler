using UnityEngine;

public class Obstacle : MonoBehaviour
{
	Rigidbody2D rb2D;

	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}
}
