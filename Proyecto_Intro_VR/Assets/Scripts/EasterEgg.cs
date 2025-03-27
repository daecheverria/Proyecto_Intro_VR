using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class EasterEgg : MonoBehaviour
{
    [SerializeField] EasterEggsSO easterEggs;
    [SerializeField] string nombre;
    [SerializeField] GameObject easterEggcanvas;
    private int displayTime = 3;
    [SerializeField] Transform playerCamara;
    [SerializeField] float distance = 0.1f;
    private void OnTriggerEnter(Collider other)
    {
         Debug.Log("aaa");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Easter Egg encontrado: " + nombre);
            var easterEgg = easterEggs.GetEasterEggByName(nombre);
            if (easterEgg != null && !easterEgg.completado)
            {
                Debug.Log("2Easter Egg encontrado: " + nombre);
                easterEgg.completado = true;
                ShowCanvas(easterEgg.nombrekey, easterEgg.descripcionkey);
            }
        }
    }
    private void Start()
    {
        playerCamara = Camera.main?.transform;
        if (playerCamara == null)
        {
            Debug.Log("No camara");
            return;
        }
    }
 

    public void ShowCanvas(string tituloKey, string descripcionKey)
    {
       
        if (playerCamara == null) return;

        GameObject canvasInstance = Instantiate(easterEggcanvas, transform);
        canvasInstance.transform.localPosition = Vector3.forward * distance; 
        canvasInstance.transform.rotation = playerCamara.rotation; 
        canvasInstance.transform.Rotate(0, 180, 0); 

        Transform panelTransform = canvasInstance.transform.Find("Panel");
        if (panelTransform == null)
        {
            Debug.LogError("El Panel no se encontró en el Canvas prefab.");
            Destroy(canvasInstance);
            return;
        }

        LocalizeStringEvent[] localizedTexts = panelTransform.GetComponentsInChildren<LocalizeStringEvent>();

        if (localizedTexts.Length >= 2)
        {
            localizedTexts[0].StringReference.SetReference("Easter Eggs", tituloKey);
            localizedTexts[1].StringReference.SetReference("Easter Eggs", descripcionKey);

            localizedTexts[0].RefreshString();
            localizedTexts[1].RefreshString();
        }
        else
        {
            Debug.LogError("No se encontraron suficientes LocalizeStringEvent en los hijos del Panel.");
            Destroy(canvasInstance);
            return;
        }

        StartCoroutine(HideCanvasAfterTime(canvasInstance));
    }

    private IEnumerator HideCanvasAfterTime(GameObject canvasInstance)
    {
        yield return new WaitForSeconds(displayTime);
        Destroy(canvasInstance);
    }
}
