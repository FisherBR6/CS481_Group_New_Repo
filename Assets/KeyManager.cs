using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public List<TMP_Text> keyLabels;
    private bool capsLock = false;
    public List<KeyData> keyDataList;

    public void ToggleCaps()
    {
        capsLock = !capsLock;

        foreach (var label in keyLabels)
        {
            if (!string.IsNullOrEmpty(label.text) && label.text.Length == 1)
            {
                label.text = capsLock ? label.text.ToUpper() : label.text.ToLower();
            }
        }
    }
}

[System.Serializable]
public class KeyData
{
    public TMP_Text label;
    public string baseChar;
}
