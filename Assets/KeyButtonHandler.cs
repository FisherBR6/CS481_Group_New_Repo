using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnKeyPress);
    }

    void OnKeyPress()
    {
        // Look for TMP_Text (TextMeshPro)
        TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
        if (tmpText != null)
        {
            Debug.Log("Key Pressed: " + tmpText.text);
            return;
        }

        // Fallbacks for legacy components
        Text uiText = GetComponentInChildren<Text>();
        if (uiText != null)
        {
            Debug.Log("Key Pressed: " + uiText.text);
            return;
        }

        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        if (textMesh != null)
        {
            Debug.Log("Key Pressed: " + textMesh.text);
            return;
        }

        Debug.LogWarning("No text found on key.");
    }
}
