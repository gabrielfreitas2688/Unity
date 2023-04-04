using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Alvos")]
    public GameObject prefab;

    [Header("Gameplay")]

    public float intervalo;
    public float minx;
    public float maxx;
    public float y;

    [Header("Ovos")]

    public Sprite[] sprites;



    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", intervalo, intervalo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        GameObject instance = Instantiate(prefab);
        instance.transform.position = new Vector2(Random.Range(minx, maxx), y);

        instance.transform.SetParent(transform);

        Sprite randomsprite = sprites[Random.Range(0, sprites.Length)];
        
        instance.GetComponent<SpriteRenderer>().sprite = randomsprite;
    }
}
