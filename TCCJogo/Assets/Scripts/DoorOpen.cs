using UnityEditor;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] string Chave;
    [SerializeField] int NumChave;
    public UI_Code ui_code;
    BoxCollider2D box;
    SpriteRenderer spriteRenderer;
    SpriteRenderer Lock;
    public Sprite OpenSprite;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        ui_code = ui_code.GetComponent<UI_Code>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Lock = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
       if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (ui_code.Senha == Chave && ui_code.UsosSenha == NumChave)
                {
                    box.enabled = false;
                    Lock.enabled = false;
                    spriteRenderer.sprite = OpenSprite;
                }
                else
                {
                    Debug.Log("Senha Incorreta ou Sem Usos");
                }
            }
        }
    }
}
