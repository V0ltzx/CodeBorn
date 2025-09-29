using UnityEngine;
using UnityEngine.SceneManagement;

public class room_change : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] string Chave;
    [SerializeField] int NumChave;
    public UI_Code ui_code;
    [SerializeField] private string Cena;
    CircleCollider2D col;

    public Vector2 destino;

    

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
                GameManager.Instance.NextDoor = destino;
                
                if (ui_code.Senha == Chave && ui_code.UsosSenha == NumChave)
                {
                    GameManager.codeCha = GameManager.Instance.oriCha;
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
