using UnityEngine;
using System.IO;

public class ShareFiles : MonoBehaviour
{
    public void ShareFile(string filePath, string mimeType = "application/msword")
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            return;
        }

        new NativeShare()
            .AddFile(filePath, mimeType)
            .SetSubject("VR Keyboard Data Export")
            .SetText("Here's my exported keyboard data from VR!")
            .SetCallback((result, shareTarget) => 
                Debug.Log("Share result: " + result + ", app: " + shareTarget))
            .Share();
    }
    public void ShareWordDoc()
    {
        string path = Path.Combine(Application.persistentDataPath, "keyboard_data.doc");
        ShareFile(path);
    }

    public void ShareCSV()
    {
        string path = Path.Combine(Application.persistentDataPath, "keyboard_data.csv");
        ShareFile(path, "text/csv");
    }
}