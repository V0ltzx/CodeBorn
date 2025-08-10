using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    // Cria��o de uma input action que tem como tipo (definido no editor) como value, tendo maior flexibilidade
    public InputAction MoveAction;
    public Animator anim;
    Rigidbody2D rb;
    Vector2 move;

    //a variavel health � uma propriedade que retorna o valor de currentHealth, assim permitindo o acesso ao valor do currentHealth sem a possibilidade de alter�-lo diretamente
    public int health { get { return currentHealth; } }
    public int maxHealth = 5;
    int currentHealth;
    int FacingDirection = 1; // O personagem inicialmente est� virado para a direita

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
        // ativando a a��o de movimento, que foi definida no editor do Unity
        MoveAction.Enable();
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

        // caso isInvincible seja verdadeiro
        if (isInvincible)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para n�o estar invenc�vel
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }

        if (isInvincibleHeal)
        {
            // retira por frame, quanto tempo um frame dura, essencialmente contando o tempo que o jogador falta para n�o estar invenc�vel
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

        // O componente localscale n pode ser alterado individualmente, por isso mudamos oq nos queremos e mantemos o resto do scale do personagem
        transform.localScale = new Vector3(FacingDirection, transform.localScale.y, transform.localScale.z); // Altera a escala do personagem para inverter a dire��o
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

        Debug.Log(currentHealth + "/" + maxHealth);

        
    }
}
