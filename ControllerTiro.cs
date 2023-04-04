using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTiro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Destroi o objeto depois que ele saí da cena
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
