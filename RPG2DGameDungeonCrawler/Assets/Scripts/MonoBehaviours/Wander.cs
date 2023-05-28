using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
	public RoomController RmController { get; set; }
	public float pursuitSpeed;
	public float wanderSpeed;
	public float directionChangeInterval;
	public bool followPlayer;
	float currentSpeed;
	Coroutine moveCoroutine;
	Rigidbody2D rb2d;
	Animator animator;
	Transform targetTransform = null;
	Vector3 endPosition;
	float currentAngle = 0;
	CircleCollider2D circleCollider;
	Obstacle obstacle;

	void Start()
	{
		obstacle = GetComponent<Obstacle>();
		endPosition = transform.position;
		circleCollider = GetComponent<CircleCollider2D>();
		animator = GetComponent<Animator>();
		currentSpeed = wanderSpeed;
		rb2d = GetComponent<Rigidbody2D>();
		StartCoroutine(WanderRoutine());
	}

	void Update()
	{
		Debug.DrawLine(rb2d.position, endPosition, Color.red);
	}

	public IEnumerator WanderRoutine()
	{
		while (true)
		{
			ChooseNewEndpoint();
			if (moveCoroutine != null)
			{
				StopCoroutine(moveCoroutine);
			}

			moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));

			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

	public IEnumerator Move(Rigidbody2D rigidbodyToMove, float speed)
	{
		float remainingDistance = (transform.position - endPosition).sqrMagnitude;
		while (remainingDistance > float.Epsilon)
		{
			if (targetTransform != null)
			{
				endPosition = targetTransform.position;
			}

			if (rigidbodyToMove != null)
			{
				animator.SetBool("isWalking", true);

				Vector3 newPosition = Vector3.MoveTowards(rigidbodyToMove.position, endPosition, speed * Time.deltaTime);

				rb2d.MovePosition(newPosition);

				remainingDistance = (transform.position - endPosition).sqrMagnitude;
			}

			yield return new WaitForFixedUpdate();
		}

		animator.SetBool("isWalking", false);
	}

	// Metoda wybierajaca nowy punkt docelowy
	void ChooseNewEndpoint()
	{
		currentAngle += Random.Range(0, 360);
		currentAngle = Mathf.Repeat(currentAngle, 360);
		endPosition += Vector3FromAngle(currentAngle);

		
		//if (obstacle.GetComponent<Collider>().bounds.Containts(endPosition))
		//{
		//	print("endPosition is inside obstacle");
		//}
		//if (hitToTest.collider.bounds.Contains(telePosition))
		//{
		//	print("point is inside collider");
		//}
	}

	Vector3 Vector3FromAngle(float inputAngleDegrees)
	{
		float inputAngleRadians = inputAngleDegrees * Mathf.Deg2Rad;

		return new Vector3(Mathf.Cos(inputAngleRadians), Mathf.Sin(inputAngleRadians), 0);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && followPlayer)
		{
			currentSpeed = pursuitSpeed;
			targetTransform = collision.gameObject.transform;

			if (moveCoroutine != null)
			{
				StopCoroutine(moveCoroutine);
			}

			moveCoroutine = StartCoroutine(Move(rb2d, currentSpeed));
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			animator.SetBool("isWalking", false);
			currentSpeed = wanderSpeed;

			if (moveCoroutine != null)
			{
				StopCoroutine(moveCoroutine);
			}

			targetTransform = null;
		}

	}

	void OnDrawGizmos()
	{
		if (circleCollider != null)
		{
			Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
		}
	}
}
