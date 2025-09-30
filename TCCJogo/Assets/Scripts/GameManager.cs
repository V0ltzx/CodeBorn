using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Crie uma instância estática do GameManager para acesso global
    public static GameManager Instance;

    // Variáveis do Player
    static public int health { get { return currentHealth; } }
    static public int currentHealth;
    static public int maxHealth = 5;
    public string Elemento = "";
    bool SetHealth = false;

    //Váriaveis do Console
    public static string codePer = "";
    public static string codeCha = "";
    public static string codeArm = "";

    public string oriPer = "using System;\r\nusing UnityEngine;\r\nusing UnityEngine.EventSystems;\r\nusing UnityEngine.InputSystem;\r\n\r\npublic class player\r\n{\r\n    string elemento;\r\n\r\n    int vidaMaxima = 5;\r\n    speed = 5;\r\n    int vidaAtual;\r\n    bool podeAtacar = true;\r\n    int cooldownAtaque = 0;\r\n\r\n    void start()\r\n    {\r\n        vidaAtual = vidaMaxima;\r\n    }\r\n\r\n\r\n    void update()\r\n    {\r\n\r\n    // \"Horizontal\" lê as setas ou A/D no teclado\r\n            float moverX = Input.GetKey(\"Horizontal\");\r\n\r\n        // \"Vertical\" lê as setas ou W/S no teclado\r\n            float moverY = Input.GetKey(\"Vertical\");\r\n\r\n        if (Input.GetKeyDown(KeyCode.Mouse0))\r\n            {\r\n                    Atacar();\r\n            }\r\n\r\n        // Interagir com \"E\"\r\n            if (Input.GetKeyDown(KeyCode.E))\r\n               {\r\n                    Interagir();\r\n            }\r\n}\r\n\r\n    void Atacar()\r\n    {\r\n        animationAttack.Start(elemento);\r\n        hitbox.appear(damage == 1);\r\n    }";
    public string oriCha = "using UnityEngine;\r\n\r\npublic class SenhaChave : MonoBehaviour\r\n{\r\n\tstring Senha;\r\n\tint UsosSenha;\r\n\r\n\tvoid Start()\r\n\t{\r\n\t\tSenha = \"\";\r\n\t\tUsosSenha = 1;\r\n\t}\r\n\r\n\tvoid Update()\r\n\t{\r\n\t\tIf(Input.GetKeyDown(KeyCode.E))\r\n\t\t{\r\n\t\t\tUsosSenha -= 1;\r\n\t\t\tif(UsoSenha == 0)\r\n\t\t\t{\r\n\t\t\t\tSenha = \"\";\r\n\t\t\t}\r\n\t\t}\t\r\n\t}\r\n}";
    public string oriArm = "using UnityEngine;\r\n\r\npublic class SwitchArmadilha : MonoBehaviour\r\n{\r\n\tbool ArmadilhaOn;\r\n\tint DistanciaPlayer;\r\n\t\r\n\tvoid Update()\r\n\t{\r\n\t\tif(DistanciaPlayer < 1)\r\n\t\t{\r\n\t\t\tArmadilhaOn = true;\t\r\n\t\t}\r\n\t}\r\n}";

    //posição player
    public Vector2 InitialPosition = new Vector2(-0.5f, -1.4f);
    public Vector2 NextDoor;
    public Vector2 PlayerPosition;
    public bool Started = false;

    //Váriaveis das armadilhas
    public bool EnableTrap = true;


    private void Awake()
    {
        // Implementa o padrão Singleton para garantir que apenas uma instância do GameManager exista
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        codePer = oriPer;
        codeCha = oriCha;
        codeArm = oriArm;

        if (!SetHealth)
        {
            currentHealth = maxHealth; 
            SetHealth = true;
        }
    }

    public void StartReset()
    {
        currentHealth = maxHealth;

        Elemento = "";

        codePer = oriPer;
        codeCha = oriCha;
        codeArm = oriArm;

        EnableTrap = true;
    }

}

