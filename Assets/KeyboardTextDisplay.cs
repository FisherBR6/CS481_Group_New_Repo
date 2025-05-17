using System;
using TMPro;
using UnityEngine;

public class KeyboardTextDisplay : MonoBehaviour
{
    public static KeyboardTextDisplay Instance;
    public TMP_Text typedText;

    private string currentText = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCharacter(string character)
    {
        currentText += character;
        typedText.text = currentText;
    }

    public void ClearText()
    {
        currentText = "";
        typedText.text = "";
    }

    public void Backspace()
    {
        if (currentText.Length > 0)
        {
            currentText = currentText.Substring(0, currentText.Length - 1); 
            typedText.text = currentText;
        }
    }

    public string getCurrentText()
    {
        return currentText;
    }
}
