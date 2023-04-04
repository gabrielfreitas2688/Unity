using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Variáveis de Velocidade")]
    public float minvelocityX;
    public float maxvelocityX;

    public float minvelocityY;

    public float maxvelocityY;

    [Header("Variável de Gameplay")]
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(minvelocityX, maxvelocityX), Random.Range(minvelocityY, maxvelocityY));
    }
    

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
