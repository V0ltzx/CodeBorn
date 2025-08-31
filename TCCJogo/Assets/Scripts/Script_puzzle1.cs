using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Script_puzzle1 : MonoBehaviour
{
    public TMP_InputField inputField; // Input que o player digita
    public TMP_Text outputText; // saida
    public static string senha_definida; // variavel que vai receber a senha
    void Start()
    {
        inputField.ActivateInputField(); // Foca no campo assim que come�ar

        inputField.onSubmit.AddListener(onSubmit); /* diz ao Unity:
�quando o jogador apertar Enter dentro do campo de texto, execute a fun��o OnSubmit passando o que foi digitado�.*/
    }

   
    void onSubmit(string playerInput) //playerInput recebe o texto do player automaticamente por causa do comando acima
    {
        var lower = playerInput.ToLowerInvariant(); // a variavel lower esta recebendo o que o player digitou s� que tudo min�sculo
                                                    // para evitar erros por causa de letra mai�scula

        if (lower.Contains("if") && lower.Contains("senha") && lower.Contains("disable_trap()")) // analisa palavras chave
        {
            int comeco = lower.IndexOf('"'); /* vai procurar onde est� a primeira aspa, e vai retornar a posi��o da aspa
                                                ChangeAssetObjectPropertiesEventArgs de errado ele vai retornar -1*/
            int fim = lower.LastIndexOf('"');

            if (comeco != -1 && fim != -1 && fim > comeco)
            {
                senha_definida = lower.Substring(comeco + 1, fim - comeco - 1);
                outputText.text = senha_definida;
            }
            else
            {
                outputText.text = "n�o esque�a de usar aspas duplas ao fazer a vari�vel senha receber a senha" ;
            }
        }
        else
        {
            outputText.text = "Senha inv�lida";

        }
    }
}
