using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    private Color default_color; 

    bool isPressed = false;
    private Renderer cubeRenderer;



    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        GetComponent<Button>().onClick.AddListener(OnKeyPressed);
        default_color = cubeRenderer.material.color; // Store the default color
    }

    void Update()
    {
        isPressed = Input.GetMouseButton(0);
    }

    /**
        * Called when a user clicks on the button on the Headset.
    */
    public void OnPointerClick()
    {
        if (!isPressed)
        {
            cubeRenderer.material.color = Color.yellow;
            OnKeyPressed();
            cubeRenderer.material.color = default_color;
        }
    }

    void OnKeyPressed()
    {
        string keyName = gameObject.name;

        
       

        if (keyName == "Delete")
        {
            Debug.Log("Delete key pressed");
            KeyboardTextDisplay.Instance.Backspace();
        }
        else if (keyName == "Spacebar")
        {
            Debug.Log("Spacebar key pressed");
            KeyboardTextDisplay.Instance.AddCharacter(" ");
        }
        else if (keyName == "Tab")
        {
            Debug.Log("Tab key pressed");
            KeyboardTextDisplay.Instance.AddCharacter("   ");
        }
        else if(keyName == "Enter")
        {
            Debug.Log("Enter key pressed");
            KeyboardTextDisplay.Instance.AddCharacter("\n");
        }
        else if(keyName == "ABC")
        {
            Debug.Log("Switching to ABC keyboard");
            SceneManagerScript.LoadABC();
        }
        else if (keyName == "QWERTY")
        {
            Debug.Log("Switching to Qwerty");
            SceneManagerScript.LoadQWERTY();
        }
        else if (keyName == "Caps")
        {
            Debug.Log("Caps enabled");
        }
        else if (keyName == "Input")
        {
            //toggle input (added)
            FindObjectOfType<InputManager>().ToggleInputMode();
  
            Debug.Log("Input switched");
        }
        else if (keyName == "Save")
        {
            Debug.Log("File saving in progress...");
            string textToSave = KeyboardTextDisplay.Instance.getCurrentText();

            if (string.IsNullOrEmpty(textToSave))
            {
                Debug.Log("text input is empty");
                return;
            }

            // save filename with current date/time
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
            string path = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                // save text to the file
                File.WriteAllText(path, textToSave);
                Debug.Log("file saved successfully at: " + path);

                // clear input field after saving
                KeyboardTextDisplay.Instance.ClearText();
            }
            catch (Exception e)
            {
                Debug.LogError("error saving file: " + e.Message);
            }
                    
        }
        else
        {
            // Look for TMP_Text
            TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
            {
                string character = tmpText.text;
                Debug.Log($"Key Pressed: {character}");
                KeyboardTextDisplay.Instance.AddCharacter(character);
            }
            else
            {
                Debug.LogWarning("No text found on key.");
            }
        }


    }
}
