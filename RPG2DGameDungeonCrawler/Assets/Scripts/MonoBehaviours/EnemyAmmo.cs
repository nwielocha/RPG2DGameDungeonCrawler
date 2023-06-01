using UnityEngine;

public class EnemyAmmo : MonoBehaviour
{
	[Range(1, 10)]
	[SerializeField]
	private float speed = 10f;
	[Range(1, 10)]
	[SerializeField]
	private float lifeTime = 3f;

	private Rigidbody2D rb2D;

	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
		Destroy(gameObject, lifeTime);
	}

	private void FixedUpdate()
	{
		rb2D.velocity = transform.up * speed;
	}
}
