using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    //public List<TextMeshProUGUI> keys;
    //public KeyManager abcKeyManager;
    //public KeyManager qwertyKeyManager;
    private Color default_color;

    //bool isPressed = false;
    private Renderer cubeRenderer;

    private TMP_Text Label;

    public static bool capslock = false;
    bool isPressed = false;

    void Start()
    {
#if UNITY_EDITOR
#elif UNITY_IOS || UNITY_ANDROID
        cubeRenderer = GetComponent<Renderer>();
        GetComponent<Button>().onClick.AddListener(OnKeyPress);
        default_color = cubeRenderer.material.color; // Store the default color
#endif
    }

    void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        isPressed = Input.GetMouseButton(0);
#endif
    }

#if UNITY_EDITOR
    public void OnPointerClick(PointerEventData eventData)
    {
        OnKeyPress();
    }
#elif UNITY_IOS || UNITY_ANDROID
    /**
        * Called when a user clicks on the button on the Headset.
    */
    public void OnPointerClick()
    {
        if (!isPressed)
        {
            cubeRenderer.material.color = Color.yellow;
            OnKeyPress();
            cubeRenderer.material.color = default_color;
        }
    }
#endif
    void OnKeyPress()
    {
        string keyName = gameObject.name;

        if (keyName == "Delete")
        {
            KeyboardTextDisplay.Instance.Backspace();
        }
        else if (keyName == "Space")
        {
            KeyboardTextDisplay.Instance.AddCharacter(" ");
        }
        else if (keyName == "Tab")
        {
            KeyboardTextDisplay.Instance.AddCharacter("   ");
        }
        else if (keyName == "Enter")
        {
            KeyboardTextDisplay.Instance.AddCharacter("\n");
        }
        else if (keyName == "ABC")
        {
            //SceneManagerScript.LoadABC();
        }
        else if (keyName == "QWERTY")
        {
            //SceneManagerScript.LoadQWERTY();
        }
        else if (keyName == "Caps")
        {
            capslock = !capslock;
            UpdateKeyLabels();
        }
        else if (keyName == "Input")
        {
            Debug.Log("Input switch work in progress");
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
                if (character.Length == 1 && char.IsLetter(character[0]))
                    character = capslock ? character.ToUpper() : character.ToLower(); // If capslock is on, give the text display uppercase, otherwise, lowercase
                KeyboardTextDisplay.Instance.AddCharacter(character);
            }
            else
            {
                Debug.LogWarning("No text found on key.");
            }
        }
    }

    /** 
     * This is a method to update all the necessary keys to caps. It takes all the objects with the keybutton tag, then loops through
     * If the key is of length 1 (keeps from messing with space and so on), then if caps lock is enabled, move to lowercase, otherwise to upper
     */
    private void UpdateKeyLabels()
    {
        KeyButton[] keys = FindObjectsOfType<KeyButton>();
        foreach (var key in keys)
        {
            TMP_Text text = key.GetComponentInChildren<TMPro.TMP_Text>();
            if (text != null && text.text.Length == 1 && char.IsLetter(text.text[0]))
                text.text = capslock ? text.text.ToUpper() : text.text.ToLower();
        }
    }
}