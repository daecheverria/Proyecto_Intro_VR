using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    public GameObject Carnet;
    public GameObject PuertaC;
    public GameObject PuertaA;
    [SerializeField] int id;
     [SerializeField] PadsSO pads;
    private bool estado;


    void Awake()
    {
        estado = pads.GetCheckboxValue(id);
    }
    void Start()
    {
        PuertaC.SetActive(!estado);
        PuertaA.SetActive(estado);
    }

   void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Carnet)
        {
            estado = !estado;
            pads.SetCheckboxValue(id, estado); 
            PuertaA.SetActive(estado);
            PuertaC.SetActive(!estado);
        }
    }
}
