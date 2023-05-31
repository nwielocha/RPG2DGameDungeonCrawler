using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
	public Transform target;
	public float speed = 3f;
	public float distanceToShoot = 5f;
	public float distanceToStop = 3f;
	public float fireRate;
	private Rigidbody2D rb2D;
	private float timeToFire;

	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (!target)
		{
			GetTarget();
		}

		if (Vector2.Distance(target.position, transform.position) <= distanceToStop)
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		if (timeToFire <= 0f)
		{
			Debug.Log("Shoot");
			timeToFire = fireRate;
		}
		else
		{
			timeToFire -= Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
		{
			rb2D.velocity = transform.up * speed;
		}
	}

	private void GetTarget()
	{
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
			target = null;
		}
		else if (collision.gameObject.CompareTag("Ammo"))
		{
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}
}
