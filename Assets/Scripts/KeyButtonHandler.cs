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
    private string fileName;

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

    public void CopyTXTToDownloads()
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("File name is not set. Cannot copy CSV.");
            return;
        }

        string sourcePath = Path.Combine(Application.persistentDataPath, "Time_Interval_Performance_Files", fileName);
        string destinationPath = Path.Combine("/storage/emulated/0/Download", fileName);

        try
        {
            if (!File.Exists(sourcePath))
            {
                Debug.LogError("Source file does not exist: " + sourcePath);
                return;
            }

            File.Copy(sourcePath, destinationPath, true); // Overwrite = true
            Debug.Log($"Successfully copied to: {destinationPath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error copying file to Downloads: {e.Message}");
        }
    }

    public void ShareTXT()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "Time_Interval_Performance_Files");
        string fullPath = Path.Combine(folderPath, fileName);

        if (!File.Exists(fullPath))
        {
            Debug.LogError("File does not exist: " + fullPath);
            return;
        }

        new NativeShare()
            .AddFile(fullPath)
            .SetSubject("VR Session Data")
            .SetText("Here is the TXT file from your VR session.")
            .Share();

        Debug.Log("Opened native share sheet for file: " + fullPath);
    }

    public string WriteToTXT(string textToSave)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = $"TimeIntervalTracker_{DateTime.Now:yyyyMMdd_HHmmss.fff}.txt";
            Debug.LogWarning("fileName was empty. Auto-generated a new one.");
        }

        string folderPath = Path.Combine(Application.persistentDataPath, "Time_Interval_Performance_Files");
        string fullPath = Path.Combine(folderPath, fileName);

        try
        {
            Directory.CreateDirectory(folderPath);
            File.WriteAllText(fullPath, textToSave);
            Debug.Log($"Logged data to: {Path.GetFullPath(fullPath)}");
            KeyboardTextDisplay.Instance?.ClearText();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to write to TXT at {fullPath}: {ex.Message}");
        }
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

                    WriteToTXT();

                    #if UNITY_ANDROID
                        Debug.Log("Running on Android. The .txt and .csv files will be saved to your downloads folder.");
                        CopyTXTToDownloads();
                    #elif UNITY_IOS
                        Debug.Log("Running on iOS. A NativeShare sheet has been opened.");
                        ShareCSV();
                    #else
                    Debug.Log("Running on another platform");
                    #endif
                    string folderPath = Path.Combine(Application.persistentDataPath, "Time_Interval_Performance_Files");
                    string fullPath = Path.Combine(folderPath, fileName);

                    
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
