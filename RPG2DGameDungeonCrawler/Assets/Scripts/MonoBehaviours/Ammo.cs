using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int DamageInflicted;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D && collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                StartCoroutine(enemy.DamageCharacter(DamageInflicted, 0.0f));
                gameObject.SetActive(false);
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
        }
    }
}
