using UnityEngine;

public class Senha_controller : MonoBehaviour
{
    public GameObject Senha_UI;
    bool isOpen = false;
    bool perto = false;

 
    void Update()
    {
        if (perto && Input.GetKeyDown(KeyCode.Tab))
        {
            abrir_senha();
        }
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            Senha_UI.SetActive(false);

        }
    }

    void abrir_senha()
    {
        isOpen = !isOpen;
        Senha_UI.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            perto = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            perto = false;
        }
    }


}
