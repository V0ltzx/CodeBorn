using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    // Criação de uma input action que tem como tipo (definido no editor) como value, tendo maior flexibilidade
    public InputAction MoveAction;
    public Animator anim;
    Rigidbody2D rb;
    Vector2 move;

    //a variavel health é uma propriedade que retorna o valor de currentHealth, assim permitindo o acesso ao valor do currentHealth sem a possibilidade de alterá-lo diretamente
    public int health { get { return currentHealth; } }
    public int maxHealth = 5;
    int currentHealth;
    int FacingDirection = 1; // O personagem inicialmente está virado para a direita

    // Variables related to temporary invincibility
    public float timeInvincible = 1.0f;
    public float timeInvincibleHeal = 2.0f;
    bool isInvincible;
    bool isInvincibleHeal;
    public bool InvinciHeal { get { return isInvincibleHeal; } }
    float damageCooldown;
    float damageCooldownHeal;

    void Start()
    {
        // ativando a ação de movimento, que foi definida no editor do Unity
        MoveAction.Enable();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

   

        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // criação de uma variavel vector2 para Ler o valor do input de movimento, que é um value e o colocando em um Vector2 (x,y)
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        // Criação de uma variavel Vector2 para guardar a posição atual do objeto (guardado na unity na parte de position do componente transform,
        // e somando o movimento (move) multiplicado pela velocidade (speed)
        //Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime; 
        //transform.position = position;

        // caso isInvincible seja verdadeiro
        if (isInvincible)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para não estar invencível
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (isInvincibleHeal)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para não estar invencível
            damageCooldownHeal -= Time.deltaTime;
            if (damageCooldownHeal < 0)
            {
                isInvincibleHeal = false;
            }
        }

    }

    void FixedUpdate()
    {
        // Aplicando a velocidade do movimento no Rigidbody2D do objeto, assim permitindo um resposta maior do sistema de calculo de fisica, multiplicando pelo deltaTime para manter a velocidade constante
        Vector2 position = (Vector2)rb.position + move * speed * Time.deltaTime;
        rb.MovePosition(position);

        // Estou colocando os valores de x e y nas variavéis de condição do Animator,
        // o move.x/y usa o operador de ponto para pegar as partes especficas da variável position que é um Vector2, possuindo tanto x e y simultaneamente que esta recebendo do MoveAction
        // Mathf.Abs é usado para pegar o valor absoluto do movimento
        anim.SetFloat("horizontal", Mathf.Abs(move.x));
        anim.SetFloat("vertical", Mathf.Abs(move.y));

        // Verifica se o personagem esta virado para o lado contrário do movimento e então chama a função Flip que altera a escala do personagem para inverter a direção
        if (move.x > 0 && transform.localScale.x < 0 || move.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
         
           
    }
    
    void Flip()
    {
        FacingDirection *= -1; // Inverte a direção de face do personagem

        // O componente localscale n pode ser alterado individualmente, por isso mudamos oq nos queremos e mantemos o resto do scale do personagem
        transform.localScale = new Vector3(FacingDirection, transform.localScale.y, transform.localScale.z); // Altera a escala do personagem para inverter a direção
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            // caso IsInvincible seja verdadeiro, retorna a função e não executa o resto do código
            if (isInvincible)
            {
                return;
            }
            // caso IsInvincible seja falso, seta ele para verdadeiro e reseta o cooldown de dano
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        if (amount > 0)
        {
            // caso IsInvincible seja verdadeiro, retorna a função e não executa o resto do código
            if (isInvincibleHeal)
            {
                return;
            }
            // caso IsInvincible seja falso, seta ele para verdadeiro e reseta o cooldown de dano
            isInvincibleHeal = true;
            damageCooldownHeal = timeInvincibleHeal;
        }

        // restringe a uma faixa fixa de valores uma variavel, o primeiro valor (currenthealt + amount) é o valor que será restringido,
        // o segundo valor é o valor mínimo (0) e o terceiro é o valor máximo (maxHealth)
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        Debug.Log(currentHealth + "/" + maxHealth);

        
    }
}
