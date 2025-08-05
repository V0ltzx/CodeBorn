using UnityEngine;

public class HealthCollectibles : MonoBehaviour
{
    public int Increase = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        // a variavel other é automaticamente preenchida pelo Unity com o objeto que entrou no trigger
        //Debug.Log("Object that entered the trigger: " + other);

        PlayerController controller = other.GetComponent<PlayerController>();

        // É necessario referenciar a variavel controller que esta recebendo o script do outro objeto sempre 
        // que quiser acessar uma variavel ou função do script PlayerController
        if (controller != null && controller.health < controller.maxHealth && controller.InvinciHeal != true)
        {
 
          // Assina um valor a variavel da função changeHealth, que é definida na classe PlayerController
          //controller.ChangeHealth(1);
          controller.ChangeHealth(Increase);
          Destroy(gameObject);
 
        }

    }

}
