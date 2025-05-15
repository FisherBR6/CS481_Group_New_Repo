using System;
using UnityEngine;
using UnityEngine.UI;
using Stopwatch = System.Diagnostics.Stopwatch;
using System.IO;

public class TimeIntervalTracker : MonoBehaviour
{
    private Stopwatch stopwatch;
    private bool trackFlag = false;

    // Declare each button explicitly
    public Button button0;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;
    public Button buttonE;
    public Button buttonF;
    public Button buttonG;
    public Button buttonH;
    public Button buttonI;
    public Button buttonJ;
    public Button buttonK;
    public Button buttonL;
    public Button buttonM;
    public Button buttonN;
    public Button buttonO;
    public Button buttonP;
    public Button buttonQ;
    public Button buttonR;
    public Button buttonS;
    public Button buttonT;
    public Button buttonU;
    public Button buttonV;
    public Button buttonW;
    public Button buttonX;
    public Button buttonY;
    public Button buttonZ;
    public Button buttonTab;
    public Button buttonDelete;
    public Button buttonEnter;
    public Button buttonCaps;
    public Button buttonSpace;
    public Button buttonComma;
    public Button buttonPeriod;

    public Button prevButton;

    public string fileName;

    private void Start()
    {
        stopwatch = new Stopwatch();

        // Ensure filename is initialized early
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = $"TimeIntervalTracker_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        }

        // Wire up listeners for each button manually
        button0.onClick.AddListener(() => OnAnyRelevantButtonPressed(button0));
        button1.onClick.AddListener(() => OnAnyRelevantButtonPressed(button1));
        button2.onClick.AddListener(() => OnAnyRelevantButtonPressed(button2));
        button3.onClick.AddListener(() => OnAnyRelevantButtonPressed(button3));
        button4.onClick.AddListener(() => OnAnyRelevantButtonPressed(button4));
        button5.onClick.AddListener(() => OnAnyRelevantButtonPressed(button5));
        button6.onClick.AddListener(() => OnAnyRelevantButtonPressed(button6));
        button7.onClick.AddListener(() => OnAnyRelevantButtonPressed(button7));
        button8.onClick.AddListener(() => OnAnyRelevantButtonPressed(button8));
        button9.onClick.AddListener(() => OnAnyRelevantButtonPressed(button9));
        buttonA.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonA));
        buttonB.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonB));
        buttonC.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonC));
        buttonD.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonD));
        buttonE.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonE));
        buttonF.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonF));
        buttonG.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonG));
        buttonH.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonH));
        buttonI.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonI));
        buttonJ.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonJ));
        buttonK.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonK));
        buttonL.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonL));
        buttonM.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonM));
        buttonN.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonN));
        buttonO.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonO));
        buttonP.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonP));
        buttonQ.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonQ));
        buttonR.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonR));
        buttonS.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonS));
        buttonT.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonT));
        buttonU.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonU));
        buttonV.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonV));
        buttonW.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonW));
        buttonX.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonX));
        buttonY.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonY));
        buttonZ.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonZ));
        buttonTab.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonTab));
        buttonDelete.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonDelete));
        buttonEnter.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonEnter));
        buttonCaps.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonCaps));
        buttonSpace.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonSpace));
        buttonComma.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonComma));
        buttonPeriod.onClick.AddListener(() => OnAnyRelevantButtonPressed(buttonPeriod));
    }

    public void OnAnyRelevantButtonPressed(Button pressedButton)
    {
        if (!trackFlag)
        {
            stopwatch.Start();
            fileName = $"TimeIntervalTracker_{DateTime.Now:yyyyMMdd_HHmmssfff}.csv";
            Debug.Log($"Button {pressedButton.name} pressed. Starting tracking at {DateTime.Now:HH:mm:ss.fff}");
            prevButton = pressedButton;
            trackFlag = true;
        }
        else
        {
            stopwatch.Stop();
            string interval = $"{stopwatch.ElapsedMilliseconds / 1000f:0.000}";
            Debug.Log($"{prevButton.name} -> {pressedButton.name} in {interval} seconds");

            WriteToCSV(pressedButton, interval);

            prevButton = pressedButton;
            stopwatch.Reset();
            stopwatch.Start();
        }
    }

    public void WriteToCSV(Button currentButton, string interval)
    {
        // Generate file name if it’s missing
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = $"TimeIntervalTracker_{DateTime.Now:yyyy/MM/dd_HH:mm:ss.fff}.csv";
            Debug.LogWarning("fileName was empty. Auto-generated a new one.");
        }

        // Save to: Assets/Time_Interval_Performance_Files/
        string fullPath = Path.Combine(Application.dataPath, "Time_Interval_Performance_Files", fileName);

        try
        {
            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using (var writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine($"{prevButton.name},{currentButton.name},{interval}");
            }

            Debug.Log($"Logged data to: {fullPath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to write to CSV at {fullPath}: {ex.Message}");
        }
    }


}
