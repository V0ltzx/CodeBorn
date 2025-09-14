using UnityEngine;
using UnityEngine.SceneManagement;

public class room_change : MonoBehaviour
{
    [SerializeField] private string Cena;
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            SceneManager.LoadScene(Cena);
        }
    }
}
