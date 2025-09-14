using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public string spawnName; // nome do spawn point desta cena

    void Start()
    {
        // Acha o spawn point pelo nome
        GameObject spawn = GameObject.Find(spawnName);
        if (spawn != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawn.transform.position;
            }
        }
    }
}

