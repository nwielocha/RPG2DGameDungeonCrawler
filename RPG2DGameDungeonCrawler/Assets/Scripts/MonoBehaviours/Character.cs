using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
	public float maxHitPoints;
	public float startingHitPoints;

	public float maxManaPoints;
	public float startingManaPoints;

	public virtual void KillCharacter()
	{
		Destroy(gameObject);
	}

	public abstract void ResetCharacter();
	public abstract IEnumerator DamageCharacter(int damage, float interval); // koprocedura
}
