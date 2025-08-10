using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        

        if (CompareTag("Enemy")) // Fixed the issue by using CompareTag as a method
        {
            controller.ChangeHealth(-1);
        }
    }
}
