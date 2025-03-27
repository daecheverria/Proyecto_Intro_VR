using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class SafeController : MonoBehaviour
{
    [Header("Puerta o parte móvil")]
    public Transform safeDoor;

    [Header("Texto para mostrar lo que se escribe")]
    public TextMeshPro inputDisplay;
    public TextMeshPro instruccionesMonitor;

    private string correctCode = "";
    private string currentInput = "";
    [Header("Cantidad de dígitos del código")]
    public int codeLength = 4;

    [Header("Objetos que mostrarán cada carácter")]
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
        Debug.Log("Código generado aleatoriamente: " + generatedCode);
    }

    void AssignCodeToClues()
    {
        if (clueTexts.Count < generatedCode.Length)
        {
            Debug.LogError("No hay suficientes objetos de pista para mostrar todos los caracteres del código.");
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
                Debug.Log("Código incorrecto");
            }
        }
    }

    void UpdateDisplay()
    {
        if (inputDisplay != null) inputDisplay.text = currentInput;
        if (juegoCompletado) instruccionesMonitor.text = "Toma la llave!\nVe al salón de profesores";
    }

    void SpawnLlave()
    {
        llave.SetActive(true);
    }
}



