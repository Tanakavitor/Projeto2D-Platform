using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 3f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SlimeController slime = other.GetComponent<SlimeController>();
            if (slime != null)
                slime.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}