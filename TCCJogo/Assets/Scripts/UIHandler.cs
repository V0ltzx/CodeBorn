using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }
    private VisualElement m_Healthbar;

    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private Label labelText;
    private float m_TimerDisplay;
    public Dialogo dialogo;
    

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        dialogo = GetComponent<Dialogo>();

        UIDocument uiDocument = GetComponent<UIDocument>();

        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
       
        SetHealthValue(1.0f);

        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("Background");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        labelText = uiDocument.rootVisualElement.Q<Label>("Label");
        m_TimerDisplay = -1.0f;


    }

    private void Update()
    {
        
        if (m_TimerDisplay > 0.0f)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay <= 0.0f)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }
    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    public void DisplayDialogue(string texto)
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        labelText.text = texto;
        m_TimerDisplay = displayTime;
    }
}
