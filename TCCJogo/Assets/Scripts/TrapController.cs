using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrapController : MonoBehaviour
{

    public bool enabletrap = true;
    public bool Parede;
    Animator anim;
    BoxCollider2D cd;
    void Awake() // é chamado quando o script é carregado
    {
        anim = GetComponent<Animator>(); // pegar o componente animator
        anim.SetBool("IsActive", false); // começa como false (traps desativadas)
        cd = GetComponent<BoxCollider2D>();

        if (Parede)
        {
            cd.enabled = false;
        }
    }
 
    

    public void disable_trap(bool Ativo)
    {
        GameManager.Instance.EnableTrap = Ativo;
        if (Parede)
        {
            if (!GameManager.Instance.EnableTrap)
            {
                cd.enabled = true;
            }
            else
            {
                cd.enabled = false;
            }
        }
        else if (!Parede)
        {
            if (!GameManager.Instance.EnableTrap)
            {
                cd.enabled = false;
            }
            else
            {
                cd.enabled = true;
            }
        }

        anim.SetBool("IsActive", false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Parede)
        {
            if (!GameManager.Instance.EnableTrap) return;

            if (other.CompareTag("Player"))
            {
                anim.SetBool("IsActive", true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instance.EnableTrap) return;

        if (other.CompareTag("Player"))
        {
            anim.SetBool("IsActive", true); 
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
