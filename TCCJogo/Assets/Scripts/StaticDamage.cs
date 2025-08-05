using UnityEngine;

public class StaticDamage : MonoBehaviour
{
    public int Damage;
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth (Damage);
        }

    }
}
