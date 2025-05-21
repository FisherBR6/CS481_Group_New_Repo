using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ShareFiles : MonoBehaviour
{
    [SerializeField] private Canvas vrCanvas;
    [SerializeField] private InputField vrInputField;
    [SerializeField] private Button vrSaveButton;
    
    [SerializeField] private GameObject sharePromptPanel;
    [SerializeField] private Button shareYesButton;
    [SerializeField] private Button shareNoButton;
    
    
    private string lastSavedFilePath;
    
    void Start()
    {
        // Setup the canvas to face the camera
        vrCanvas.renderMode = RenderMode.WorldSpace;
        vrCanvas.transform.position = new Vector3(0, 0, 2); // 2 meters in front of camera
        vrCanvas.transform.rotation = Quaternion.LookRotation(vrCanvas.transform.position - Camera.main.transform.position);
        vrCanvas.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f); // Scale appropriately for VR
        
        // Add listener to save button
        vrSaveButton.onClick.AddListener(SaveTextToFile);
        
        // Add listeners to share prompt buttons
        if (shareYesButton != null)
            //shareYesButton.onClick.AddListener(ShareLastSavedFile);
        
        if (shareNoButton != null)
            shareNoButton.onClick.AddListener(CloseSharePrompt);
        
        // Hide share prompt panel initially
        if (sharePromptPanel != null)
            sharePromptPanel.SetActive(false);
    }
    
    public void SaveTextToFile()
    {
        if (vrInputField != null && !string.IsNullOrEmpty(vrInputField.text))
        {
            // Generate filename with current date/time
            string fileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
            
            // Define the path to save the file
            string path = Path.Combine(Application.persistentDataPath, fileName);
            
            try
            {
                // Write the text to the file
                File.WriteAllText(path, vrInputField.text);
                Debug.Log("Text saved successfully to: " + path);
                
                // Store the path for sharing
                lastSavedFilePath = path;
                
                // Show the share prompt
                ShowSharePrompt();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error saving text to file: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Input field is empty or not assigned!");
        }
    }
    
    private void ShowSharePrompt()
    {
        if (sharePromptPanel != null)
        {
            sharePromptPanel.SetActive(true);
            
            // Position the prompt panel in front of the user's view
            sharePromptPanel.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
            sharePromptPanel.transform.rotation = Quaternion.LookRotation(
                sharePromptPanel.transform.position - Camera.main.transform.position);
        }
    }
    
    private void CloseSharePrompt()
    {
        if (sharePromptPanel != null)
            sharePromptPanel.SetActive(false);
    }
    
    // public void ShareLastSavedFile()
    // {
    //     if (!string.IsNullOrEmpty(lastSavedFilePath) && fileSharing != null)
    //     {
    //         fileSharing.ShareFile(lastSavedFilePath, "Text from VR App", "Here's the text I saved in VR!");
    //         CloseSharePrompt();
    //     }
    //     else
    //     {
    //         Debug.LogWarning("No file to share or FileSharing component not assigned!");
    //     }
    // }
}
