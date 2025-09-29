using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private string Cena;
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void Play()
    {
        SceneManager.LoadScene(Cena);
        GameManager.Instance.StartReset();
        GameManager.Instance.Started = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
