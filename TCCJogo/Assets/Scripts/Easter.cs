using UnityEngine;

public class Easter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {
            player.transform.localScale = new Vector3(1f, 0.3f, 1f);
            Destroy(gameObject);
        }
    }
}
