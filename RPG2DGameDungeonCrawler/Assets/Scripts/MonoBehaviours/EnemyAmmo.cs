using UnityEngine;

public class EnemyAmmo : MonoBehaviour
{
	public int damageInflicted;
	[Range(1, 10)]
	[SerializeField]
	private float speed = 1f;
	[Range(1, 10)]
	[SerializeField]
	private float lifeTime = 1f;
	private Vector3 targetDirection;
	private float timer;

	private void Start()
	{
		timer = 0f;
		Destroy(gameObject, lifeTime);
	}

	private void FixedUpdate()
	{
		transform.Translate(speed * Time.deltaTime * targetDirection);
		timer += Time.deltaTime;
		if (timer >= lifeTime)
		{
			Destroy(gameObject);
		}
	}

	public void SetDirection(Vector3 direction)
	{
		targetDirection = direction.normalized;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision is BoxCollider2D && collision.gameObject.CompareTag("Player"))
		{
			if (collision.gameObject.TryGetComponent<Player> (out var player)) 
			{
				StartCoroutine(player.DamageCharacter(damageInflicted, 0.0f));
				gameObject.SetActive(false);
			}
		}
		else if (collision.gameObject.CompareTag("Obstacle"))
		{
			gameObject.SetActive(false);
		}
	}
}
