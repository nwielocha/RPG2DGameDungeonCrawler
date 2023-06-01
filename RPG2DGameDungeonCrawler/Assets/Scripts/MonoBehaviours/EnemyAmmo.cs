using UnityEngine;

public class EnemyAmmo : MonoBehaviour
{
	[Range(1, 10)]
	[SerializeField]
	private float speed = 3f;
	[Range(1, 10)]
	[SerializeField]
	private float lifeTime = 3f;
	private Rigidbody2D rb2D;
	Transform target;
	private Vector3 targetDirection;
	private float timer;

	private void Start()
	{
		timer = 0f;
		rb2D = GetComponent<Rigidbody2D>();
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
		//var step = Time.deltaTime * speed;
		//target = GameObject.FindGameObjectWithTag("Player").transform;
		//Vector3 directionToMove = target.position - transform.position;
		//directionToMove = directionToMove.normalized * step;
		//float maxDistance = Vector3.Distance(transform.position, target.position);
		////rb2D.position = Vector3.MoveTowards(rb2D.position, target.position, step);
		//print(target.position);
		//Debug.DrawLine(rb2D.position, target.position);
		//transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
	}

	public void SetDirection(Vector3 direction)
	{
		targetDirection = direction.normalized;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(collision.gameObject);
	}
}
