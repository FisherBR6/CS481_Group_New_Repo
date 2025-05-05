using TMPro;
using UnityEngine;

public class KeyboardTextDisplay : MonoBehaviour
{
    public static KeyboardTextDisplay Instance;
    public TMP_Text typedText;

    private string currentText = "";

    void Awake()
    {
        Instance = this;
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
}
