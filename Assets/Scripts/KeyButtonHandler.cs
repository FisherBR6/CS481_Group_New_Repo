using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour, IPointerClickHandler
{
    private Color default_color;
    private Renderer cubeRenderer;

    public SceneManagerScript sceneManagerScript; // Assigned via Inspector

    public static bool capslock = false;
    bool isPressed = false;

    void Start()
    {
#if UNITY_EDITOR
#elif UNITY_IOS || UNITY_ANDROID
        cubeRenderer = GetComponent<Renderer>();
        GetComponent<Button>().onClick.AddListener(OnKeyPress);
        default_color = cubeRenderer.material.color;
#endif

        // Optional: assign sceneManagerScript automatically if not set
        if (sceneManagerScript == null)
        {
            sceneManagerScript = FindObjectOfType<SceneManagerScript>();
            if (sceneManagerScript == null)
                Debug.LogError("SceneManagerScript not found in scene.");
        }
    }

    void Update()
    {
#if UNITY_IOS || UNITY_ANDROID
        isPressed = Input.GetMouseButton(0);
#endif
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnKeyPress();
    }

#if UNITY_IOS || UNITY_ANDROID
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
        if (sceneManagerScript == null)
        {
            Debug.LogError("sceneManagerScript is not assigned.");
            return;
        }

        string keyName = gameObject.name;
        if (sceneManagerScript.TimerStatus())
        {
            sceneManagerScript.ContinueTracking(keyName);
        }
        else
        {
            sceneManagerScript.StartTracking(keyName);
        }

        switch (keyName)
        {
            case "Delete":
                KeyboardTextDisplay.Instance?.Backspace();
                break;
            case "Space":
                KeyboardTextDisplay.Instance?.AddCharacter(" ");
                break;
            case "Tab":
                KeyboardTextDisplay.Instance?.AddCharacter("   ");
                break;
            case "Enter":
                KeyboardTextDisplay.Instance?.AddCharacter("\n");
                break;
            case "ABC":
                SceneManagerScript.Instance?.LoadABC();
                break;
            case "QWERTY":
                SceneManagerScript.Instance?.LoadQWERTY();
                break;
            case "Caps":
                capslock = !capslock;
                UpdateKeyLabels();
                break;
            case "Input":
                Debug.Log("Input switch work in progress");
                break;
            case "Save":
                string textToSave = KeyboardTextDisplay.Instance?.getCurrentText();
                if (string.IsNullOrEmpty(textToSave))
                {
                    Debug.Log("Text input is empty");
                    return;
                }

                string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
                string path = Path.Combine(Application.persistentDataPath, fileName);

                try
                {
                    File.WriteAllText(path, textToSave);
                    Debug.Log("File saved at: " + path);
                    KeyboardTextDisplay.Instance?.ClearText();
                }
                catch (Exception e)
                {
                    Debug.LogError("Error saving file: " + e.Message);
                }
                break;
            default:
                TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
                if (tmpText != null)
                {
                    string character = tmpText.text;
                    if (character.Length == 1 && char.IsLetter(character[0]))
                    {
                        character = capslock ? character.ToUpper() : character.ToLower();
                        Debug.Log("Caps char: " + character);
                    }
                    KeyboardTextDisplay.Instance?.AddCharacter(character);
                }
                else
                {
                    Debug.LogWarning("No text found on key.");
                }
                break;
        }
    }

    private void UpdateKeyLabels()
    {
        KeyButton[] keys = FindObjectsOfType<KeyButton>();
        foreach (var key in keys)
        {
            TMP_Text text = key.GetComponentInChildren<TMP_Text>();
            if (text != null && text.text.Length == 1 && char.IsLetter(text.text[0]))
                text.text = capslock ? text.text.ToUpper() : text.text.ToLower();
        }
    }
}
