using UnityEngine;

public class HealZone : MonoBehaviour
{
    public int Heal = 1;
    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            // Check if the player is not at max health before healing
            if (GameManager.health < GameManager.maxHealth)
            {
                controller.ChangeHealth(Heal); 
            }
        }
    }
}
