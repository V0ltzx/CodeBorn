using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Morte : MonoBehaviour
{
    public GameObject Tela_morte;
    [SerializeField] private string Cena_menu;
    public PlayerController player;

    void Start()
    {
        Tela_morte.SetActive(false);
    }
    void Update()
    {
       if (player.health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Tela_morte.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Botao_casa()
    {
        SceneManager.LoadScene(Cena_menu);
    }
}
