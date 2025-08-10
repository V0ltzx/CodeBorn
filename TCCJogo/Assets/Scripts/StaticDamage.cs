using UnityEngine;

public class StaticDamage : MonoBehaviour
{
    //public int damage;
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }

    }
}
