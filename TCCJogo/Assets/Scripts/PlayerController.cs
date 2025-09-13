using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    
    // Criação de uma input action que tem como tipo (definido no editor) como value, tendo maior flexibilidade
    public InputAction MoveAction;
    public InputAction Attack;
    public InputAction Interact;

    // Componentes 
    public Animator anim;
    Rigidbody2D rb;
    public GameObject attackPrefab;
    
    // Variáveis padrão
    Vector2 move;
    public float speed = 0.1f;
   
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

    // Variáveis de ataque
    public float timeAttack = 1f;
    float attackCooldown;
    bool canAttack;
    public string Elemento = "";
    

    void Start()
    {
        // ativando a ação de movimento, que foi definida no editor do Unity
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
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        // Timer de invencibilidade de cura
        if (isInvincibleHeal)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para não estar invencível
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

        // Função Ataque, acionado com o InputAction Attack
        if (Attack.triggered)
        {
            Swing(); // Chama a função Swing quando o botão de ataque é pressionado
        }

        if(Interact.triggered)
        {
            InteractAction(); // Chama a função InteractAction quando o botão de interação é pressionado
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
        Debug.Log("Current Health: " + currentHealth);
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

        // Instancia a hitbox do ataque a algumas unidades do personagem, dependendo da direção que ele está virado
        // A hitbox do ataque está fora da posição do seu gameobject,
        // então é necessário adicionar um offset para que a hitbox fique na posição correta olhando para a direita, mas não para esquerda que o offset da hitbox esta pendendo
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
        // Um raycast é um tipo de detecção de colisão que verifica se há algum objeto na direção e layers especificados
        // O primeiro parâmetro é a posição de origem do raycast, o segundo parâmetro é a direção do raycast, o terceiro é a distância que o raycast irá percorrer e o quarto é a layer que ele irá verificar
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.up, 0.3f, LayerMask.GetMask("Dialogo"));

        // Se o ray atingir um collider, ele executa o código dentro do if
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
