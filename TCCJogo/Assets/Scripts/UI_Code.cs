using TMPro;
using UnityEngine;

public class UI_Code : MonoBehaviour
{
    public GameObject Tela_code;
    bool isActive = false;
    public TMP_InputField inputField;
    public PlayerController playerController;
    public TrapController[] trapController;


    // 1 - Personagem, 2 - Chave, 3 - Armadilha 0 - Nenhum
    int Selecionado = 0;

    string codePer = "";
    string codeCha = "";
    string codeArm = "";

    string oriPer = "using System;\r\nusing UnityEngine;\r\nusing UnityEngine.EventSystems;\r\nusing UnityEngine.InputSystem;\r\n\r\npublic class player\r\n{\r\n    string elemento;\r\n\r\n    int vidaMaxima = 5;\r\n    speed = 5;\r\n    int vidaAtual;\r\n    bool podeAtacar = true;\r\n    int cooldownAtaque = 0;\r\n\r\n    void start()\r\n    {\r\n        vidaAtual = vidaMaxima;\r\n    }\r\n\r\n\r\n    void update()\r\n    {\r\n\r\n    // \"Horizontal\" lê as setas ou A/D no teclado\r\n            float moverX = Input.GetKey(\"Horizontal\");\r\n\r\n        // \"Vertical\" lê as setas ou W/S no teclado\r\n            float moverY = Input.GetKey(\"Vertical\");\r\n\r\n        if (Input.GetKeyDown(KeyCode.Mouse0))\r\n            {\r\n                    Atacar();\r\n            }\r\n\r\n        // Interagir com \"E\"\r\n            if (Input.GetKeyDown(KeyCode.E))\r\n               {\r\n                    Interagir();\r\n            }\r\n}\r\n\r\n    void Atacar()\r\n    {\r\n        animationAttack.Start(elemento);\r\n        hitbox.appear(damage == 1);\r\n    }";
    string oriCha = "using UnityEngine;\r\n\r\npublic class SenhaChave : MonoBehaviour\r\n{\r\n\tstring Senha;\r\n\tint UsosSenha;\r\n\r\n\tvoid Start()\r\n\t{\r\n\t\tSenha = \"\";\r\n\t\tUsosSenha = 1;\r\n\t}\r\n\r\n\tvoid Update()\r\n\t{\r\n\t\tIf(Input.GetKeyDown(KeyCode.E))\r\n\t\t{\r\n\t\t\tUsosSenha -= 1;\r\n\t\t\tif(UsoSenha == 0)\r\n\t\t\t{\r\n\t\t\t\tSenha = \"\";\r\n\t\t\t}\r\n\t\t}\t\r\n\t}\r\n}";
    string oriArm = "using UnityEngine;\r\n\r\npublic class SwitchArmadilha : MonoBehaviour\r\n{\r\n\tbool ArmadilhaOn;\r\n\tint DistanciaPlayer;\r\n\t\r\n\tvoid Update()\r\n\t{\r\n\t\tif(DistanciaPlayer < 1)\r\n\t\t{\r\n\t\t\tArmadilhaOn = true;\t\r\n\t\t}\r\n\t}\r\n}";

    //Chave
    string Senha;
    int UsosSenha;

    void Start()
    {
        Tela_code.SetActive(false);

        Senha = "";
        UsosSenha = 1;

        codePer = oriPer;
        codeCha = oriCha;
        codeArm = oriArm;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Time.timeScale = 0.01f;
            Tela_code.SetActive(true);
            isActive = true;
        }
        if (isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            Tela_code.SetActive(false);
            isActive = false;
        }
        
        if (Selecionado == 1)
        {
            codePer = inputField.text;
        }
        else if (Selecionado == 2)
        {
            codeCha = inputField.text;
        }
        else if (Selecionado == 3)
        {
            codeArm = inputField.text;
        }

        if (UsosSenha <= 0)
        {
            Senha = "";
            codeCha = oriCha;
        }
    }

    public void Personagem()
    {
        Selecionado = 1;
        inputField.text = ""; // Limpa antes de carregar
        inputField.text = codePer;
    }

    public void Chave()
    {
        Selecionado = 2;
        inputField.text = ""; // Limpa antes de carregar
        inputField.text = codeCha;
    }

    public void Armadilha()
    {
        Selecionado = 3;
        inputField.text = ""; // Limpa antes de carregar
        inputField.text = codeArm;
    }
    public void Run()
    {
        //Pesonagem
        if (Selecionado == 1)
        {

            if (codePer.Contains("fire") || codePer.Contains("Fire"))
            {
                playerController.Elemento = "fire";
            }
            if (codePer.Contains("dark") || codePer.Contains("Dark"))
            {
                playerController.Elemento = "dark";
            }
            if (codePer.Contains("light") || codePer.Contains("Light"))
            {
                playerController.Elemento = "light";
            }
            if (codePer.Contains("water") || codePer.Contains("Water"))
            {
                playerController.Elemento = "water";
            }
        }
        //Chave
        else if (Selecionado == 2)
        {
            // colocar \" \" faz a string reconher a aspas como parte do texto

            if (codeCha.Contains("Senha\"Alguma senha\";"))
            {
                Senha = "Alguma senha";
            }
            else if (codeCha.Contains("Senha\"Outra senha\";"))
            {
                Senha = "Outra senha";
            }
            else if (codeCha.Contains("Senha\"Senha123\";"))
            {
                Senha = "Senha123";
            }

            if (codeCha.Contains("UsosSenha = 1;"))
            {
                UsosSenha = 1;
            }
            else if (codeCha.Contains("UsosSenha = 2;"))
            {
                UsosSenha = 2;
            }
            else if (codeCha.Contains("UsosSenha = 3;"))
            {
                UsosSenha = 3;
            }
            
        }

        //Armadilha
        else if (Selecionado == 3)
        {

            if (codeArm.Contains("ArmadilhaOn = false;"))
            {
                foreach (var trap in trapController)
                {
                    trap.disable_trap();
                }
            }
        }
        else
        {
            inputField.text = "Nenhum objeto selecionado";
        }
    }

}
