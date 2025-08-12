using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    
    // Criação de uma input action que tem como tipo (definido no editor) como value, tendo maior flexibilidade
    public InputAction MoveAction;
    public InputAction Attack;

    // Componentes 
    public Animator anim;
    Rigidbody2D rb;
    public GameObject attackPrefab;
    
    // Variáveis padrão
    Vector2 move;
    public float speed = 0.1f;
    float FacingDirection = 0.3f; // O personagem inicialmente está virado para a direita


    //a variavel health é uma propriedade que retorna o valor de currentHealth, assim permitindo o acesso ao valor do currentHealth sem a possibilidade de alterá-lo diretamente
    public int health { get { return currentHealth; } }
    public int maxHealth = 5;
    int currentHealth;
    

    // Timer de invencibilidade
    public float timeInvincible = 1.0f;
    float damageCooldown;
    bool isInvincible;

    // Timer de invencibilidade de cura
    public float timeInvincibleHeal = 2.0f;
    bool isInvincibleHeal;
    public bool InvinciHeal { get { return isInvincibleHeal; } }   
    float damageCooldownHeal;

    // Cooldown de ataque
    public float timeAttack = 1f;
    float attackCooldown;
    bool canAttack;
    

    void Start()
    {
        // ativando a ação de movimento, que foi definida no editor do Unity
        MoveAction.Enable();
        Attack.Enable();
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

        // Timer de invincibilidade
        if (isInvincible)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para não estar invencível
            damageCooldown = Mathf.Clamp(damageCooldown - Time.deltaTime, 0, timeInvincible);
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        // Timer de invencibilidade de cura
        if (isInvincibleHeal)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para não estar invencível
            damageCooldownHeal = Mathf.Clamp(damageCooldownHeal - Time.deltaTime, 0, timeInvincibleHeal);
            if (damageCooldownHeal < 0)
            {
                isInvincibleHeal = false;
            }
        }

        // Cooldown de ataque
        if (!canAttack)
        {
            attackCooldown = Mathf.Clamp(attackCooldown - Time.deltaTime, 0, timeAttack);
         
            if (attackCooldown <= 0f) // Se o cooldown de ataque for menor ou igual a 0, o jogador pode atacar novamente
            {
                canAttack = true;
            }

        }

        // Função Ataque, acionado com o InputAction Attack
        if (Attack.triggered)
        {
            Swing(); // Chama a função Swing quando o botão de ataque é pressionado
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
        Debug.Log("Flip: " + FacingDirection);
        // O componente localscale n pode ser alterado individualmente, por isso mudamos oq nos queremos e mantemos o resto do scale do personagem
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z); // Altera a escala do personagem para inverter a direção
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
            // Parametros de trigger para o animador, aqui ele considera que o personagem tomou dano e vai executar a animação relacionada ao trigger Hit'
            anim.SetTrigger("Hit");
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

        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        

    }

    void Swing()
    {
        if (!canAttack)
        {
            return; // Se não puder atacar, sai da função
        }
        canAttack = false; // Desabilita o ataque até o cooldown acabar
        attackCooldown = timeAttack; // Reseta o cooldown de ataque
        Vector2 hitbox = transform.position;

        // 0.5 unidades à frente na direção do player
        if (transform.localScale.x > 0) 
        { 
            Vector2 spawnOffset = new Vector2(0.187f, 0.1f);
        
            GameObject projectileObject = Instantiate(attackPrefab, hitbox + spawnOffset, Quaternion.identity);
        }
        if(transform.localScale.x < 0) 
        { 
            Vector2 spawnOffset = new Vector2( 0.0f, 0.1f);
        
            GameObject projectileObject = Instantiate(attackPrefab, hitbox + spawnOffset, Quaternion.identity);
        }
        anim.SetTrigger("Attack");
        
    }
}
