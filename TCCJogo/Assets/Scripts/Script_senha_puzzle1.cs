using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Script_senha_puzzle1 : MonoBehaviour
{

    public TMP_InputField inputField;
    public TMP_Text outputText;
    public TrapController[] traps;
    
    public void unlock()
    {
        foreach (var trap in traps)
        {
            trap.disable_trap();
        }
    }
    void Start()
    {
        inputField.ActivateInputField(); // Foca no campo assim que começar

        inputField.onSubmit.AddListener(verificar_senha); /* diz ao Unity:
“quando o jogador apertar Enter dentro do campo de texto, execute a função OnSubmit passando o que foi digitado”.*/
    }

    void verificar_senha (string senha)
    {
        if (senha == Script_puzzle1.senha_definida)
        {
            unlock();
            outputText.text = "armadilhas desabilitadas";
        }
        else
        {
            outputText.text = "senha incorreta";
        }
    }
}
