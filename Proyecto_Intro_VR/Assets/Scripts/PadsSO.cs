using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PadsSO", menuName = "Game Data/Pads Checklist")]
public class PadsSO : ScriptableObject
{
    [System.Serializable]
    public class PadsInfo
    {
        public int id;
        public bool complete;
    }

    public List<PadsInfo> pads = new List<PadsInfo>();

    public void SetCheckboxValue(int id, bool value)
    {
        foreach (var pad in pads)
        {
            if (pad.id == id)
            {
                pad.complete = value;
                return;
            }
        }
    }

    public bool GetCheckboxValue(int id)
    {
        foreach (var pad in pads)
        {
            if (pad.id == id)
            {
                return pad.complete;
            }
        }
        return false;
    }

    public void SetAllCheckboxesFalse()
    {
        foreach (var pad in pads)
        {
            pad.complete = false;
        }
    }

}

