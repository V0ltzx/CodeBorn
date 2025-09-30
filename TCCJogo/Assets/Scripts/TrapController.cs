using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrapController : MonoBehaviour
{
    public bool Parede;
    Animator anim;
    BoxCollider2D cd;
    void Awake() 
    {
        anim = GetComponent<Animator>(); 
        anim.SetBool("IsActive", false); 
        cd = GetComponent<BoxCollider2D>();

        if (Parede)
        {
            cd.enabled = false;
        }
    }

    private void Update()
    {
        if (Parede)
        {
            if (!GameManager.Instance.EnableTrap)
            {
                cd.enabled = true;
                anim.SetBool("IsActive", false);
            }
            else
            {
                cd.enabled = false;
                anim.SetBool("IsActive", true);
            }
        }
        else if (!Parede)
        {
            if (!GameManager.Instance.EnableTrap)
            {
                cd.enabled = false;
                anim.SetBool("IsActive", false);
            }
            else
            {
                cd.enabled = true;
                anim.SetBool("IsActive", true);
            }
        }
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
        if(!Parede)
        {
            if (!GameManager.Instance.EnableTrap) return;

            if (other.CompareTag("Player"))
            {
                anim.SetBool("IsActive", true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!GameManager.Instance.EnableTrap) return;

        if (other.CompareTag("Player"))
        {
            anim.SetBool("IsActive", false); 
        }
    }
}
