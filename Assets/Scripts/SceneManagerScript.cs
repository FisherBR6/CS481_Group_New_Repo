using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Stopwatch = System.Diagnostics.Stopwatch;
using System.Globalization;
using System.IO;
public class SceneManagerScript : MonoBehaviour
{
    //Need to make it an instance because I need to call in keybutton script. Can't use statics on enumerator.
    public static SceneManagerScript Instance { get; private set; }
    public CardboardReticlePointer reticlePointer;

    Boolean trackFlag = false;
    string fileName;

    string prevButton;
    Stopwatch stopwatch = new Stopwatch();

    List<List<string>> csvData = new List<List<string>>();


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    // Assets/Scenes/QWERTY_Scene.unity
    // Assets/Scenes/New_Qwerty.unity
    void Start()
    {
        Debug.Log("Attempting to load QWERTY script...");
        SceneManager.LoadScene("New_Qwerty", LoadSceneMode.Additive);
    }

    public void LoadABC()
    {
        StartCoroutine(SwitchScenes("New_Qwerty", "New_Abc"));
    }

    public void LoadQWERTY()
    {
        StartCoroutine(SwitchScenes("New_Abc", "New_Qwerty"));
    }

    private IEnumerator SwitchScenes(string sceneUnload, string sceneLoad)
    {
        yield return new WaitForSeconds(0.1f);

        if (reticlePointer != null)
        {
            //reticlePointer.ClearCurrentTarget();
            reticlePointer.enabled = false;
        }

        yield return SceneManager.UnloadSceneAsync(sceneUnload);

        yield return null;

        yield return SceneManager.LoadSceneAsync(sceneLoad, LoadSceneMode.Additive);

        yield return null;

        if (reticlePointer != null)
        {
            reticlePointer.enabled = true;
        }

        EventSystem.current.SetSelectedGameObject(null);

        RaycastHit hit;
        if (Physics.Raycast(reticlePointer.transform.position, reticlePointer.transform.forward, out hit))
        {
            Debug.Log("Reticle is pointing at: " + hit.collider.name);
        }
    }

    public Boolean TimerStatus()
    {
        if (trackFlag)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartTracking(string pressedButton)
    {
        stopwatch.Start(); // Start the stopwatch
        if (fileName == null)
            fileName = $"TimeIntervalTracker{DateTime.Now:yyyyMMdd_HHmmss.fff}.csv";
        UnityEngine.Debug.Log($"Button {pressedButton} pressed starting the application at time {DateTime.Now:HH:mm:ss.fff}");
        prevButton = pressedButton;
        //create a new csv file

        trackFlag = true; // Set the flag to true after starting
    }

    public void ContinueTracking(string pressedButton)
    {
        stopwatch.Stop(); // Stop the stopwatch
        UnityEngine.Debug.Log($"{prevButton} -> {pressedButton} in {stopwatch.ElapsedMilliseconds / 1000f:0.000} seconds");
        WriteCsvData(pressedButton, $"{stopwatch.ElapsedMilliseconds / 1000f:0.000}");
        prevButton = pressedButton;
        //write buttons and time interval to the existing csv


        stopwatch.Reset(); // Reset the stopwatch
        stopwatch.Start(); // Start again for the next interval
    }
    
    public void StopTracking()
    {
        stopwatch.Reset();

        trackFlag = false; // Set the flag to true after starting
    }


    /**
    public void WriteToCSV(string currentButton, string interval)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = $"TimeIntervalTracker_{DateTime.Now:yyyyMMdd_HHmmss.fff}.csv";
            Debug.LogWarning("fileName was empty. Auto-generated a new one.");
        }

        string folderPath = Path.Combine(Application.persistentDataPath, "Time_Interval_Performance_Files");
        string fullPath = Path.Combine(folderPath, fileName);

        try
        {
            Directory.CreateDirectory(folderPath);

            using (var writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine($"{prevButton},{currentButton},{interval}");
            }

            Debug.Log($"Logged data to: {Path.GetFullPath(fullPath)}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to write to CSV at {fullPath}: {ex.Message}");
        }
    }
    **/


    public void WriteCsvData(string currentButton, string interval)
    {
        try
        {
            List<string> row = new List<string> { prevButton, currentButton, interval };
            csvData.Add(row);
            prevButton = currentButton; // Optional: update for next call
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to write to CSV data: {ex.Message}");
        }
    }

    public List<List<string>> GetCsvData()
    {
        return csvData;
    }

    public void CopyCSVToDownloads()
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


    /**
    public void ShareCSV()
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
            .SetText("Here is the CSV file from your VR session.")
            .Share();

        Debug.Log("Opened native share sheet for file: " + fullPath);
    }
    **/

    /**
    void OnApplicationQuit()
    {
        #if UNITY_ANDROID
            Debug.Log("Running on Android. The .txt and .csv files will be saved to your downloads folder.");
            //CopyCSVToDownloads();
        #elif UNITY_IOS
            Debug.Log("Running on iOS. A NativeShare sheet has been opened.");
            //ShareCSV();
        #else
            Debug.Log("Running on another platform");
        #endif
    }
    **/


}
