using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    public bool vertical;
    public float PatrolTime = 2.0f;
    float Timer;
    int direction = 1;
    public int healthMax;
    int currentHealth;
    public GameObject player;
    float range = 1.0f;
    float distance;
    NavMeshAgent agent;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Timer = PatrolTime;
        currentHealth = healthMax;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    void Update()
    {


        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            direction = -direction;
            Timer = PatrolTime;
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {

        Vector2 playerPosition = player.transform.position;
        Vector2 myPosition = transform.position;

        // Calcula a dist�ncia entre dois vetores
        distance = Vector2.Distance(playerPosition, myPosition);

        // Declara uma vari�vel Vector2 para armazenar a posi��o atual do Rigidbody2D do inimigo
        Vector2 position = rb.position;

        if (distance > range)
        {
            if (vertical == false)
            {
                // Pega o valor x da posi��o atual e adiciona a velocidade multiplicada pelo tempo fixo (Time.fixedDeltaTime) para mover o inimigo horizontalmente
                position.x = position.x + speed * direction * Time.fixedDeltaTime;
            }
            else
            {
                //Pega o valor y da posi��o atual e adiciona a velocidade multiplicada pelo tempo fixo (Time.fixedDeltaTime) para mover o inimigo horizontalmente
                position.y = position.y + speed * direction * Time.fixedDeltaTime;
            }

            rb.MovePosition(position);
        }
        else if (distance <= range)
        {
            // MoveTowards pega a posi��o atual no primeiro par�metro, a posi��o do traget no segundo par�metro e uma velocidade para o movimento no terceiro par�metro
            // � poss�vel adicionar um par�metro de distancia m�xima, que em valores negativos faz o objeto se afastar   
            //transform.position = Vector2.MoveTowards(myPosition, playerPosition, speed * Time.fixedDeltaTime);

            Chase();
        }

        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            // Se o jogador colidir com o inimigo, chama a fun��o ChangeHealth do PlayerController para reduzir a vida do jogador
            controller.ChangeHealth(-1);
        }
    }

    public void DamageSofrido(int amount)
    {
        currentHealth -= amount;
    }

    public void Chase()
    {
        agent.SetDestination(player.transform.position);
    }
}
