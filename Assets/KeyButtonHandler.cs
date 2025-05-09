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
        else if(keyName == "ABC")
        {
            SceneManagerScript.LoadABC();
        }
        else if (keyName == "QWERTY")
        {
            SceneManagerScript.LoadQWERTY();
        }
        else if (keyName == "Caps")
        {
            Debug.Log("Caps enabled");
        }
        else if (keyName == "Input")
        {
            Debug.Log("Input switched");
        }
        else if (keyName == "Save")
        {
            Debug.Log("File saving in progress...");
        }
        else
        {
            // Look for TMP_Text
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
