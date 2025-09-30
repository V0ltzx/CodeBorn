using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine;

public class GradeController : MonoBehaviour
{
    [SerializeField] bool botao;
    bool started = false;

    //Componentes
    Animator anim;
    CapsuleCollider2D Capsule;
    BoxCollider2D Box;
    public GradeController Grade;
    [SerializeField] bool Contrario;

    void Start()
    {
        Capsule = GetComponent<CapsuleCollider2D>();
        Box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        Grade = Grade.GetComponent<GradeController>();

        if (botao)
        {
            Box.enabled = true;
        }
    }

    private void Update()
    {   
        if (!botao)
        {
            if (Contrario && !started)
            {
                anim.SetTrigger("Check");
                started = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if(controller != null)
        {
            if (!botao)
            {
                if (!Contrario)
                {
                    anim.SetTrigger("Check");
                    Box.enabled = true;
                    Capsule.enabled = false;
                }
                else if (Contrario)
                {
                    Box.enabled = false;
                    Capsule.enabled = false;
                    anim.SetBool("Finish", true);
                }
            }
            else if (botao)
            {
                anim.SetTrigger("Click");
                Box.enabled = false;
                Grade.Desativar();
            }
        }

    }

    void Desativar()
    {
        if(botao) return;
        Box.enabled = false;
        anim.SetBool("Finish", true);
    }

}
