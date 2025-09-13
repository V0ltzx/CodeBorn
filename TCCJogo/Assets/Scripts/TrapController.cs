using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrapController : MonoBehaviour
{

    public bool enabletrap = true;
    Animator anim;
    BoxCollider2D cd;
    void Awake() // é chamado quando o script é carregado
    {
        anim = GetComponent<Animator>(); // pegar o componente animator
        anim.SetBool("IsActive", false); // começa como false (traps desativadas)
        cd = GetComponent<BoxCollider2D>();
    }
 


    public void disable_trap()
    {
        enabletrap = false;
        cd.enabled = false;
        anim.SetBool("IsActive", false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabletrap) return;

        if (other.CompareTag("Player"))
        {
            anim.SetBool("IsActive", true); // ativa a animação
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!enabletrap) return;

        if (other.CompareTag("Player"))
        {
            anim.SetBool("IsActive", false); 
        }
    }



}
