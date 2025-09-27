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
        GameManager.Instance.Resethealth();
        GameManager.Instance.ResetCode();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
