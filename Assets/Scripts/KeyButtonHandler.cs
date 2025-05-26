using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    private Color default_color;
    private Renderer cubeRenderer;

    private static bool capslock = false;
    private bool isPressed;

    // FIX: Ensure this is assigned automatically to avoid null reference
    private SceneManagerScript sceneManagerScript;

    void Start()
    {
        isPressed = false;
        cubeRenderer = GetComponent<Renderer>();
        default_color = cubeRenderer.material.color;

        // Minimal fix: assign sceneManagerScript
        sceneManagerScript = SceneManagerScript.Instance;
        if (sceneManagerScript == null)
        {
            Debug.LogError("SceneManagerScript.Instance is null — make sure it's initialized.");
        }
    }

    void Update()
    {
        isPressed = Input.GetMouseButton(0);
    }

    public void OnPointerClick()
    {
        OnKeyPress();
    }

    public void OnPointerEnter()
    {
    }

    public void OnPointerExit()
    {
    }

    void OnKeyPress()
    {
        if (!isPressed)
        {
            isPressed = true;
            Debug.Log("in key press method isPressed is: " + isPressed);
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
                    string folderPath = Path.Combine(Application.persistentDataPath, "Time_Interval_Performance_Files");
                    string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff") + ".txt";
                    string fullPath = Path.Combine(folderPath, fileName);

                    try
                    {
                        Directory.CreateDirectory(folderPath);
                        File.WriteAllText(fullPath, textToSave);
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
    }

    private void UpdateKeyLabels()
    {
        KeyButton[] keys = FindObjectsOfType<KeyButton>();
        foreach (var key in keys)
        {
            TMP_Text text = key.GetComponentInChildren<TMP_Text>();
            if (text != null && text.text.Length == 1 && char.IsLetter(text.text[0]))
            {
                text.text = capslock ? text.text.ToUpper() : text.text.ToLower();
            }
        }
    }
}
