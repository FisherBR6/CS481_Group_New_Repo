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
        string keyName = gameObject.name;

        if (keyName == "Delete")
        {
            KeyboardTextDisplay.Instance.Backspace();
        }
        else if (keyName == "Spacebar")
        {
            KeyboardTextDisplay.Instance.AddCharacter(" ");
        }
        else if (keyName == "Tab")
        {
            KeyboardTextDisplay.Instance.AddCharacter("   ");
        }
        else if(keyName == "Enter")
        {
            KeyboardTextDisplay.Instance.AddCharacter("\n");
        }
        else
        {
            // Look for TMP_Text (TextMeshPro)
            TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
            {
                string character = tmpText.text;
                KeyboardTextDisplay.Instance.AddCharacter(character);
            }
            else
            {
                Debug.LogWarning("No text found on key.");
            }
        }
    }
}
