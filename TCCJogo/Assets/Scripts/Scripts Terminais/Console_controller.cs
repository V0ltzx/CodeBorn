using Unity.VisualScripting;
using UnityEngine;

public class Console_controller : MonoBehaviour
{
    public GameObject console_ui;
    bool isOpen = false;
    bool perto = false;
   
    
    void Update()
    {
        if (perto && Input.GetKeyDown(KeyCode.Tab))
        {
            abrir_terminal();
        }

        // comando abaixo resolve um "bug" que estava ocorrendo
        /* quando o console era aberto, o jogo era pausado, mas quando se apertava ESC aparecia a tela de pause e o jogo 
            continuava pausado (at� a� tudo bem). Mas quando saia da tela de pause, o jogo despausava, mas o console continuava aberto
        for�amdo o player a apertar a tecla de abrir/fechar o console para pausar novamente*/

        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            console_ui.SetActive(false); // este comando faz o console se fechar quando a tecla ESC for clicada

            /* Isso foi feito por que o bob�o aqui (Pedro) se acostumou a fechar as coisa com a tecla ESC.
             essa corre��o foi feita com o objetivo de evitar erros na hora da apresenta��o e passarmos vergonha*/ 

        }
    }

    void abrir_terminal()
    {
        isOpen = !isOpen; /* a vari�vel isOpen recebe o valor contr�rio (se for true vai receber false/ e se for false recebe true)
                           Essa l�gica permite que o player aperte E novamente para fechar, pois no void update o if esta analisando se perto for
                            true e se a tecla E for precionada, quando E � precionado, chama a fun��o "abrir_terminal" que vai fazer o terminal
                            abrir ou fechar */
        console_ui.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f; // pausa o jogo
        }
        else
        {
            Time.timeScale = 1f; // despausa o jogo
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            perto = true;
        }

    }

    private void OnTriggerExit2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            perto = false;
        }
    }
}
