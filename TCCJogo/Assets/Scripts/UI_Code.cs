using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using Microsoft.CodeAnalysis;



public class UI_Code : MonoBehaviour
    {
        public GameObject Tela_code;
        bool isActive = false;
        public GameObject target;
        SpriteRenderer sr;
        public TMP_InputField inputField;
        public string CodeName;
        string code;
      
    void Start()
        {
            Tela_code.SetActive(false);
            sr = target.GetComponent<SpriteRenderer>();
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
                Time.timeScale = 1f;
                Tela_code.SetActive(false);
                isActive = false;
            }
        }

        public void Load()
        {
            string textreader = Application.dataPath + "/Scripts/" + CodeName + ".cs";
            List<string> finallines = File.ReadAllLines(textreader).ToList();
            inputField.text = ""; // Limpa antes de carregar
            foreach (string line in finallines)
            {
                inputField.text += "\n" + line;
            }
            
        }
        public void Run()
        {
            code = inputField.text;
            if (code.Contains("sr = GetComponent<SpriteRenderer>();") && code.Contains("sr.color = Color.red;"))
            {
                sr.color = Color.red;
            }
            else if(inputField.text == "")
            {
                Debug.LogWarning("O campo de entrada está vazio. Nada para executar.");
            }
        }

    }
