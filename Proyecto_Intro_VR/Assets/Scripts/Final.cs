using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderData;

public class Final : MonoBehaviour
{
    public GameObject Llave;
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);  
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Llave)
        {
            Canvas.SetActive(true);
        }
    }

    public void Terminar()
    {
        Application.Quit();
    }
}
