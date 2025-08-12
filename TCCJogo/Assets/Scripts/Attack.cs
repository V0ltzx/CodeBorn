using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public int damage;
    Collider2D collider2d;
    
    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
        Destroy(gameObject, 10.25f);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.DamageSofrido(damage);
        }
        Debug.Log("Projectile collision with " + other.gameObject);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

}
