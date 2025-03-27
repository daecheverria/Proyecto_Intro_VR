using UnityEngine;

public class SafeUITrigger : MonoBehaviour
{
    [Header("UI que se mostrará")]
    public GameObject safeUI;

    void Start()
    {
        if (safeUI != null)
            safeUI.SetActive(false); // Ocultar al iniciar
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entre");
            if (safeUI != null)
                safeUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Sali");
            if (safeUI != null)
                safeUI.SetActive(false);
        }
    }




}



