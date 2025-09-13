using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    
    // Cria��o de uma input action que tem como tipo (definido no editor) como value, tendo maior flexibilidade
    public InputAction MoveAction;
    public InputAction Attack;
    public InputAction Interact;

    // Componentes 
    public Animator anim;
    Rigidbody2D rb;
    public GameObject attackPrefab;
    
    // Vari�veis padr�o
    Vector2 move;
    public float speed = 0.1f;
   
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

    // Vari�veis de ataque
    public float timeAttack = 1f;
    float attackCooldown;
    bool canAttack;
    public string Elemento = "";
    

    void Start()
    {
        // ativando a a��o de movimento, que foi definida no editor do Unity
        MoveAction.Enable();
        Attack.Enable();
        Interact.Enable();
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
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        // Timer de invencibilidade de cura
        if (isInvincibleHeal)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para n�o estar invenc�vel
            damageCooldownHeal -= Time.deltaTime;
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

        if(Interact.triggered)
        {
            InteractAction(); // Chama a fun��o InteractAction quando o bot�o de intera��o � pressionado
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
        Debug.Log("Current Health: " + currentHealth);
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

        // Instancia a hitbox do ataque a algumas unidades do personagem, dependendo da dire��o que ele est� virado
        // A hitbox do ataque est� fora da posi��o do seu gameobject,
        // ent�o � necess�rio adicionar um offset para que a hitbox fique na posi��o correta olhando para a direita, mas n�o para esquerda que o offset da hitbox esta pendendo
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
       
        if (Elemento == "Fire" || Elemento == "fire")
        {
            anim.SetTrigger("AttackFire");
        }
        else if (Elemento == "Water" || Elemento == "water")
        {
            anim.SetTrigger("AttackWater");
        }
        else if (Elemento == "Light" || Elemento == "light")
        {
            anim.SetTrigger("AttackLight");
        }
        else if (Elemento == "Dark" || Elemento == "dark")
        {
            anim.SetTrigger("AttackDark");
        }
        else if(Elemento == "")
        {
            anim.SetTrigger("Attack");
        }


    }

    void InteractAction()
    {
        // Um raycast � um tipo de detec��o de colis�o que verifica se h� algum objeto na dire��o e layers especificados
        // O primeiro par�metro � a posi��o de origem do raycast, o segundo par�metro � a dire��o do raycast, o terceiro � a dist�ncia que o raycast ir� percorrer e o quarto � a layer que ele ir� verificar
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.up, 0.3f, LayerMask.GetMask("Dialogo"));

        // Se o ray atingir um collider, ele executa o c�digo dentro do if
        if (hit.collider != null)
        {
            Dialogo interagivel = hit.collider.GetComponent<Dialogo>();
            if (interagivel != null)
            {
                string text = interagivel.text;
                UIHandler.instance.DisplayDialogue(text);
            }
        }
    }
}
