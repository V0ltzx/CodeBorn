using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject _tutorial;
    bool Done = false;
    BoxCollider2D box;

    void Start()
    {
        _tutorial.SetActive(false);
        box = GetComponent<BoxCollider2D>();
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Done)
        {
            box.enabled = false;
            _tutorial.SetActive(true);
            Time.timeScale = 0f;
            Done = true;
        }
    }

    public void CloseTutorial()
    {
        Time.timeScale = 1f;
        _tutorial.SetActive(false);
        Done = true;
    }
}
