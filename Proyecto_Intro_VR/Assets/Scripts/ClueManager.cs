using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ClueManager : MonoBehaviour
{
    [Header("Cantidad de d�gitos del c�digo")]
    public int codeLength = 4;

    [Header("Objetos que mostrar�n cada car�cter")]
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
}



