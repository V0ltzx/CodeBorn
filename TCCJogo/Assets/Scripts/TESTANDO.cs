using TMPro;
using UnityEditor;
using UnityEngine;

public class TESTANDO : MonoBehaviour
{
    public TMP_InputField inputField; // onde o player digita
    public TMP_Text outputText;       // texto de saída

    void Start()
    {
        // Foca no campo assim que começar
        inputField.ActivateInputField();

        // Evento chamado quando o player pressiona Enter
        inputField.onSubmit.AddListener(OnSubmit); /* diz ao Unity:
“quando o jogador apertar Enter dentro do campo de texto, execute a função OnSubmit passando o que foi digitado”.*/
    }

    void OnSubmit(string playerInput) //playerInput recebe o texto do player automaticamente por causa do comando acima
    {
        if (!string.IsNullOrEmpty(playerInput))
        {
            outputText.text = playerInput;
            
        }

        // Garante que o campo continua focado para digitar de novo
        inputField.ActivateInputField();
    }
}
