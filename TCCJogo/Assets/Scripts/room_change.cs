using UnityEngine;
using UnityEngine.SceneManagement;

public class room_change : MonoBehaviour
{
    [SerializeField] string Chave;
    [SerializeField] int NumChave;
    public UI_Code ui_code;
    [SerializeField] private string Cena;
    CircleCollider2D col;

    private void Start()
    {
        ui_code = ui_code.GetComponent<UI_Code>();
        col = GetComponent<CircleCollider2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ui_code.Senha == Chave && ui_code.UsosSenha == NumChave)
                {
                    UI_Code.codeCha = ui_code.oriCha;
                    SceneManager.LoadScene(Cena);
                }
                else
                {
                    Debug.Log("Senha Incorreta ou Sem Usos");
                }
            }
        }
    }
}
