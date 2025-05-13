using System;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class TimeIntervalTracker : MonoBehaviour
{
    private Stopwatch stopwatch;
    private bool trackFlag = false;

    private void Start()
    {
        stopwatch = new Stopwatch();
    }

    public void OnAnyRelevantButtonPressed(Button pressedButton)
    {
        if (!trackFlag)
        {
            switch (pressedButton.name)
            {
                case "e":
                case "2":
                    Debug.Log($"The application started at {DateTime.Now:HH:mm:ss.fff}");
                    stopwatch.Start(); // Start the stopwatch
                    trackFlag = true;
                    break;
            }
        }
        else
        {
            switch (pressedButton.name)
            {
                case "1":
                case "2":
                    stopwatch.Stop(); // Stop the stopwatch
                    Debug.Log($"Button {pressedButton.name} pressed at timer signal: {stopwatch.ElapsedMilliseconds / 1000f:0.000} seconds");
                    stopwatch.Reset(); // Reset the stopwatch
                    stopwatch.Start(); // Start again for the next interval
                    break;
            }
        }
    }
}
