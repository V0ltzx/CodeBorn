using TMPro;
using UnityEngine;

public class UI_Code : MonoBehaviour
{
    public GameObject Tela_code;
    bool isActive = false;
    public TMP_InputField inputField;
    public PlayerController playerController;
    //public TrapController[] trapController;


    // 1 - Personagem, 2 - Chave, 3 - Armadilha 0 - Nenhum
    int Selecionado = 0;

    //Chave
    public string Senha;
    public int UsosSenha;

    void Awake()
    {
        Senha = "";
        UsosSenha = 1;
    }
    void Start()
    {
        Tela_code.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = 0f;
            Tela_code.SetActive(true);
            isActive = true;
        }
        else if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            Tela_code.SetActive(false);
            isActive = false;
        }
        
        if (Selecionado == 1)
        {
            GameManager.codePer = inputField.text;
        }
        else if (Selecionado == 2)
        {
            GameManager.codeCha = inputField.text;
        }
        else if (Selecionado == 3)
        {
            GameManager.codeArm = inputField.text;
        }
    }

    public void Personagem()
    {
        Selecionado = 1;
        inputField.text = ""; // Limpa antes de carregar
        inputField.text = GameManager.codePer;
    }

    public void Chave()
    {
        Selecionado = 2;
        inputField.text = ""; // Limpa antes de carregar
        inputField.text = GameManager.codeCha;
    }

    public void Armadilha()
    {
        Selecionado = 3;
        inputField.text = ""; // Limpa antes de carregar
        inputField.text = GameManager.codeArm;
    }
    public void Run()
    {
        //Pesonagem
        if (Selecionado == 1)
        {
            if (GameManager.codePer.Contains("fire") || GameManager.codePer.Contains("Fire"))
            {
                GameManager.Instance.Elemento = "fire";
            }
            if (GameManager.codePer.Contains("dark") || GameManager.codePer.Contains("Dark"))
            {
                GameManager.Instance.Elemento = "dark";
            }
            if (GameManager.codePer.Contains("light") || GameManager.codePer.Contains("Light"))
            {
                GameManager.Instance.Elemento = "light";
            }
            if (GameManager.codePer.Contains("water") || GameManager.codePer.Contains("Water"))
            {
                GameManager.Instance.Elemento = "water";
            }
        }
        //Chave
        else if (Selecionado == 2)
        {
            // colocar \" \" faz a string reconher a aspas como parte do texto

            if (GameManager.codeCha.Contains("1234"))
            {
                Senha = "1234";
            }
            else if (GameManager.codeCha.Contains("4321"))
            {
                Senha = "4321";
            }
            else if (GameManager.codeCha.Contains("2486"))
            {
                Senha = "2486";
            }

            if (GameManager.codeCha.Contains("UsosSenha = 1;"))
            {
                UsosSenha = 1;
            }
            else if (GameManager.codeCha.Contains("UsosSenha = 2;"))
            {
                UsosSenha = 2;
            }
            else if (GameManager.codeCha.Contains("UsosSenha = 3;"))
            {
                UsosSenha = 3;
            }
            Debug.Log("Senha: " + Senha + " Usos: " + UsosSenha);
        }

        //Armadilha
        else if (Selecionado == 3)
        {

            if (GameManager.codeArm.Contains("ArmadilhaOn = false;"))
            {
                GameManager.Instance.EnableTrap = false;
            }

            if (GameManager.codeArm.Contains("ArmadilhaOn = true;"))
            {
                GameManager.Instance.EnableTrap = true;
            }
        }
        else
        {
            inputField.text = "Nenhum objeto selecionado";
        }
    }

}
