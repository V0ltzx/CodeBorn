using UnityEngine;

public class ControllerVertical : MonoBehaviour
{
    public float speed = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // O tipo de variavel vector2 guarda dois numerais, no caso guardaremos o X e Y
        // A variavel que setamos pega as informa��es do componente Transform do objeto e guarda na variavel position (o ponto(.) serve para pegar o componente Transform do objeto)
        Vector2 position = transform.position;

        // utilizando o ponto pegamos a parte X da variavel position e adicionamos um valor a ela a cada frame
        // logo em seguida setamos a variavel position de volta ao componente Transform do objeto
        position.y = position.y + speed;
        transform.position = position;
    }
}

