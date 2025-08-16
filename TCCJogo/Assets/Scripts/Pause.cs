using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{

    [SerializeField] private string voltar_menu;
    public GameObject menu_pause;
    void Start()
    {
        menu_pause.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            menu_pause.SetActive(true);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        menu_pause.SetActive(false);
    }
    public void Quit_menu()
    {
        SceneManager.LoadScene(voltar_menu);
    }
}
