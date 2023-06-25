using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	public float movementSpeed = 10.0f;
	public float newMovementSpeed = 10.0f;
	public float duration = 3.0f;
	private float originalSpeed;
	public bool isSpeedChanged = false;
	Vector2 movement = new();
	Animator animator;
	Rigidbody2D rb2D;

	void Start()
	{
		animator = GetComponent<Animator>();
		rb2D = GetComponent<Rigidbody2D>();
		originalSpeed = movementSpeed;
	}

	void Update()
	{
		UpdateState();
	}

	void FixedUpdate()
	{
		MoveCharacter();
	}

	private void MoveCharacter()
	{
		if (isSpeedChanged)
		{
			StartCoroutine(ChangeSpeed());
		}
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		movement.Normalize();
		rb2D.velocity = movement * movementSpeed;
	}

	private void UpdateState()
	{
		if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
		{
			animator.SetBool("isWalking", false);
		}
		else
		{
			animator.SetBool("isWalking", true);
		}

		animator.SetFloat("xDir", movement.x);
		animator.SetFloat("yDir", movement.y);
	}

	public IEnumerator ChangeSpeed()
	{
		movementSpeed = newMovementSpeed;

		yield return new WaitForSeconds(duration);

		movementSpeed = originalSpeed;
		isSpeedChanged = false;
	}

}
