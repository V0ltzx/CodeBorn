using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public string spawnName; // nome do spawn point desta cena
    [SerializeField] private string previousSceneName; // nome da cena anterior
    [SerializeField] private GameObject player; // referência ao objeto do jogador
    void Start()
    {
        // Acha o spawn point pelo nome
        GameObject spawn = GameObject.Find(spawnName);
        if (spawn != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
               Instantiate(player, spawn.transform.position, Quaternion.identity);
            }
            if (player != null)
            {
                player.transform.position = spawn.transform.position;
            }
        }
    }
}

