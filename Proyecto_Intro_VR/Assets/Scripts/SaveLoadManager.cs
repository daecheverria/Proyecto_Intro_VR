using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    private static string PadsFilePath => Path.Combine(Application.persistentDataPath, "pads.json");
    private static string EasterEggFilePath => Path.Combine(Application.persistentDataPath, "easterEggs.json");

    public static void SavePads(PadsSO padsChecklist)
    {
        try
        {
            string json = JsonUtility.ToJson(padsChecklist, true);
            File.WriteAllText(PadsFilePath, json);
            Debug.Log("Pads guardados exitosamente.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error guardando Pads: " + ex.Message);
        }
    }

    public static void SaveEasterEggs(EasterEggsSO easterEggChecklist)
    {
        try
        {
            string json = JsonUtility.ToJson(easterEggChecklist, true);
            File.WriteAllText(EasterEggFilePath, json);
            Debug.Log("Easter Eggs guardados exitosamente.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error guardando Easter Eggs: " + ex.Message);
        }
    }

    public static void LoadPads(PadsSO padsChecklist)
    {
        if (File.Exists(PadsFilePath))
        {
            try
            {
                string json = File.ReadAllText(PadsFilePath);
                JsonUtility.FromJsonOverwrite(json, padsChecklist);
                Debug.Log("Pads cargados exitosamente.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error cargando Pads: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de Pads.");
        }
    }

    public static void LoadEasterEggs(EasterEggsSO easterEggChecklist)
    {
        if (File.Exists(EasterEggFilePath))
        {
            try
            {
                string json = File.ReadAllText(EasterEggFilePath);
                JsonUtility.FromJsonOverwrite(json, easterEggChecklist);
                Debug.Log("Easter Eggs cargados exitosamente.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error cargando Easter Eggs: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de Easter Eggs.");
        }
    }
}
