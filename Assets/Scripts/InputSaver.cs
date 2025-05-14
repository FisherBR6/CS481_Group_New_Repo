using UnityEngine;
using System;
using System.IO;
using TMPro;

public class InputSaver : MonoBehaviour
{
    [SerializeField] private TMP_InputField textInputField;
    
    public void SaveTextToFile()
    {
        string textToSave = textInputField.text;
        
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
            textInputField.text = "";
        }
        catch (Exception e)
        {
            Debug.LogError("error saving file: " + e.Message);
        }
    }
    
    // mthod to read files from saved directory
    public string[] GetSavedFiles()
    {
        return Directory.GetFiles(Application.persistentDataPath, "*.txt");
    }
    
    // method to read a specific file
    public string ReadFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        return "file not found";
    }
}
