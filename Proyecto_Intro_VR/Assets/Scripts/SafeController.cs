using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class SafeController : MonoBehaviour
{
    [Header("Puerta o parte m�vil")]
    public Transform safeDoor;

    [Header("Texto para mostrar lo que se escribe")]
    public TextMeshPro inputDisplay;
    public TextMeshPro instruccionesMonitor;

    private string correctCode = "";
    private string currentInput = "";
    [Header("Cantidad de d�gitos del c�digo")]
    public int codeLength = 4;

    [Header("Objetos que mostrar�n cada car�cter")]
    public List<TextMeshPro> clueTexts;

    private string generatedCode = "";

    public bool juegoCompletado = false;

    public GameObject llave;

    void Start()
    {
        GenerateRandomCode();
        AssignCodeToClues();
        SetCorrectCode(generatedCode);
        instruccionesMonitor.text = "Ingresa clave:";
        llave.SetActive(false);
    }

    void GenerateRandomCode()
    {
        generatedCode = "";
        for (int i = 0; i < codeLength; i++)
        {
            generatedCode += Random.Range(0, 10).ToString(); // Puedes cambiarlo por letras si quieres
        }
        Debug.Log("C�digo generado aleatoriamente: " + generatedCode);
    }

    void AssignCodeToClues()
    {
        if (clueTexts.Count < generatedCode.Length)
        {
            Debug.LogError("No hay suficientes objetos de pista para mostrar todos los caracteres del c�digo.");
            return;
        }

        for (int i = 0; i < generatedCode.Length; i++)
        {
            clueTexts[i].text = generatedCode[i].ToString();
        }
    }
    public void SetCorrectCode(string code)
    {
        correctCode = code;
    }

    public void AddDigit(string digit)
    {
        if (juegoCompletado)
        {
            return;
        }
        currentInput += digit;
        Debug.Log(currentInput.Length);
        Debug.Log(correctCode.Length);
        UpdateDisplay();

        if (currentInput.Length == correctCode.Length)
        {
            if (currentInput == correctCode)
            {
                juegoCompletado = true;
                currentInput = "";
                UpdateDisplay();
                SpawnLlave();
            }
            else
            {
                currentInput = "";
                UpdateDisplay();
                Debug.Log("C�digo incorrecto");
            }
        }
    }

    void UpdateDisplay()
    {
        if (inputDisplay != null) inputDisplay.text = currentInput;
        if (juegoCompletado) instruccionesMonitor.text = "Toma la llave!\nVe al sal�n de profesores";
    }

    void SpawnLlave()
    {
        llave.SetActive(true);
    }
}



