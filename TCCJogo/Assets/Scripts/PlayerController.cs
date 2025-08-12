using System;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    
    // Cria��o de uma input action que tem como tipo (definido no editor) como value, tendo maior flexibilidade
    public InputAction MoveAction;
    public InputAction Attack;

    // Componentes 
    public Animator anim;
    Rigidbody2D rb;
    public GameObject attackPrefab;
    
    // Vari�veis padr�o
    Vector2 move;
    public float speed = 0.1f;
    float FacingDirection = 0.3f; // O personagem inicialmente est� virado para a direita


    //a variavel health � uma propriedade que retorna o valor de currentHealth, assim permitindo o acesso ao valor do currentHealth sem a possibilidade de alter�-lo diretamente
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
        // ativando a a��o de movimento, que foi definida no editor do Unity
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
        // cria��o de uma variavel vector2 para Ler o valor do input de movimento, que � um value e o colocando em um Vector2 (x,y)
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        // Cria��o de uma variavel Vector2 para guardar a posi��o atual do objeto (guardado na unity na parte de position do componente transform,
        // e somando o movimento (move) multiplicado pela velocidade (speed)
        //Vector2 position = (Vector2)transform.position + move * speed * Time.deltaTime; 
        //transform.position = position;

        // Timer de invincibilidade
        if (isInvincible)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para n�o estar invenc�vel
            damageCooldown = Mathf.Clamp(damageCooldown - Time.deltaTime, 0, timeInvincible);
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        // Timer de invencibilidade de cura
        if (isInvincibleHeal)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para n�o estar invenc�vel
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

        // Fun��o Ataque, acionado com o InputAction Attack
        if (Attack.triggered)
        {
            Swing(); // Chama a fun��o Swing quando o bot�o de ataque � pressionado
        }
    }

    void FixedUpdate()
    {
        // Aplicando a velocidade do movimento no Rigidbody2D do objeto, assim permitindo um resposta maior do sistema de calculo de fisica, multiplicando pelo deltaTime para manter a velocidade constante
        Vector2 position = (Vector2)rb.position + move * speed * Time.deltaTime;
        rb.MovePosition(position);

        // Estou colocando os valores de x e y nas variav�is de condi��o do Animator,
        // o move.x/y usa o operador de ponto para pegar as partes especficas da vari�vel position que � um Vector2, possuindo tanto x e y simultaneamente que esta recebendo do MoveAction
        // Mathf.Abs � usado para pegar o valor absoluto do movimento
        anim.SetFloat("horizontal", Mathf.Abs(move.x));
        anim.SetFloat("vertical", Mathf.Abs(move.y));

        // Verifica se o personagem esta virado para o lado contr�rio do movimento e ent�o chama a fun��o Flip que altera a escala do personagem para inverter a dire��o
        if (move.x > 0 && transform.localScale.x < 0 || move.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }


    }

    void Flip()
    {
        FacingDirection *= -1; // Inverte a dire��o de face do personagem
        Debug.Log("Flip: " + FacingDirection);
        // O componente localscale n pode ser alterado individualmente, por isso mudamos oq nos queremos e mantemos o resto do scale do personagem
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z); // Altera a escala do personagem para inverter a dire��o
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            // caso IsInvincible seja verdadeiro, retorna a fun��o e n�o executa o resto do c�digo
            if (isInvincible)
            {
                return;
            }
            // caso IsInvincible seja falso, seta ele para verdadeiro e reseta o cooldown de dano
            isInvincible = true;
            damageCooldown = timeInvincible;
            // Parametros de trigger para o animador, aqui ele considera que o personagem tomou dano e vai executar a anima��o relacionada ao trigger Hit'
            anim.SetTrigger("Hit");
        }

        if (amount > 0)
        {
            // caso IsInvincible seja verdadeiro, retorna a fun��o e n�o executa o resto do c�digo
            if (isInvincibleHeal)
            {
                return;
            }
            // caso IsInvincible seja falso, seta ele para verdadeiro e reseta o cooldown de dano
            isInvincibleHeal = true;
            damageCooldownHeal = timeInvincibleHeal;
        }

        // restringe a uma faixa fixa de valores uma variavel, o primeiro valor (currenthealt + amount) � o valor que ser� restringido,
        // o segundo valor � o valor m�nimo (0) e o terceiro � o valor m�ximo (maxHealth)
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
        

    }

    void Swing()
    {
        if (!canAttack)
        {
            return; // Se n�o puder atacar, sai da fun��o
        }
        canAttack = false; // Desabilita o ataque at� o cooldown acabar
        attackCooldown = timeAttack; // Reseta o cooldown de ataque
        Vector2 hitbox = transform.position;

        // 0.5 unidades � frente na dire��o do player
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
