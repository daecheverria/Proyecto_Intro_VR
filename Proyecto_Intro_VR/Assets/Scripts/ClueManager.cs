using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ClueManager : MonoBehaviour
{
    [Header("Cantidad de dígitos del código")]
    public int codeLength = 4;

    [Header("Objetos que mostrarán cada carácter")]
    public List<TextMeshPro> clueTexts;

    [Header("Caja fuerte que debe desbloquearse")]
    public SafeController safeController;

    private string generatedCode = "";

    void Start()
    {
        GenerateRandomCode();
        AssignCodeToClues();
        safeController.SetCorrectCode(generatedCode);
    }

    void GenerateRandomCode()
    {
        generatedCode = "";
        for (int i = 0; i < codeLength; i++)
        {
            generatedCode += Random.Range(0, 10).ToString(); // Puedes cambiarlo por letras si quieres
        }
        safeController.SetCorrectCode(generatedCode);
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
}



