using UnityEngine;

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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Timer = PatrolTime;
        currentHealth = healthMax;
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


        // Declara uma variável Vector2 para armazenar a posição atual do Rigidbody2D do inimigo
        Vector2 position = rb.position;

        if (vertical == false)
        {
            // Pega o valor x da posição atual e adiciona a velocidade multiplicada pelo tempo fixo (Time.fixedDeltaTime) para mover o inimigo horizontalmente
            position.x = position.x + speed * direction * Time.fixedDeltaTime;
        }
        else
        {
            //Pega o valor y da posição atual e adiciona a velocidade multiplicada pelo tempo fixo (Time.fixedDeltaTime) para mover o inimigo horizontalmente
            position.y = position.y + speed * direction * Time.fixedDeltaTime;
        }

        // Move o rigidbody para a nova posição calculada
        rb.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            // Se o jogador colidir com o inimigo, chama a função ChangeHealth do PlayerController para reduzir a vida do jogador
            controller.ChangeHealth(-1);
        }
    }

    public void DamageSofrido(int amount)
    {
        currentHealth -= amount;
    }
}
