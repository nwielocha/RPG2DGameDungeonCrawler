using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
	public RoomController RmController { get; set; }
	public int damageStrength;
	public EnemyType enemyType;
	public GameObject enemyAmmoPrefab;
	public float speed = 3f;
	public float distanceToShoot = 5f;
	public float distanceToStop = 3f;
	public float fireRate;
	private Transform target;
	private Rigidbody2D rb2D;
	private float timeToFire;
	Coroutine damageCoroutine;
	float hitPoints;

	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
		print("START() ENEMY");
	}

	private void Update()
	{
		if (enemyType == EnemyType.Ranged)
		{
			if (!target)
			{
				GetTarget();
				print("GETTARGET() ENEMY: " + target);
			}

			if (target != null && Vector2.Distance(target.position, transform.position) <= distanceToShoot)
			{
				Shoot();
			}
		}
	}

	//private void FixedUpdate()
	//{
	//	if (enemyType == EnemyType.Ranged)
	//	{
	//		if (target != null)
	//		{
	//			if (Vector2.Distance(target.position - transform.position, transform.position) >= distanceToStop)
	//			{
	//				rb2D.velocity = transform.up * speed;
	//			}
	//			else
	//			{
	//				rb2D.velocity = Vector2.zero;
	//			}
	//		}

	//	}
	//}

	private void Shoot()
	{
		if (timeToFire <= 0f)
		{
			Debug.Log("Shoot");
			Instantiate(enemyAmmoPrefab, transform.position, transform.rotation);
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}

	private void GetTarget()
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

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
		if (collision.gameObject.CompareTag("Player") && enemyType == EnemyType.Melee)
		{
			print("Kolizja z " + collision.gameObject.name);
			Player player = collision.gameObject.GetComponent<Player>();
			if (damageCoroutine == null)
			{
				damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
			}
		}
		//else if (collision.gameObject.CompareTag("Ammo") && enemyType == EnemyType.Ranged)
		//{
		//	print("Kolizja z " + collision.gameObject.name);
		//	Player player = collision.gameObject.GetComponent<Player>();
		//	if (damageCoroutine == null)
		//	{
		//		damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
		//	}
		//}
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
