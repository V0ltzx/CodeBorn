using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TrapController : MonoBehaviour
{

    public bool enabletrap = true;
    Animator anim;
    void Awake() // � chamado quando o script � carregado
    {
        anim = GetComponent<Animator>(); // pegar o componente animator
        anim.SetBool("IsActive", false); // come�a como false (traps desativadas)
    }
 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabletrap) return;

        if (other.CompareTag("Player"))
        {
            anim.SetBool("IsActive", true); // ativa a anima��o
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
