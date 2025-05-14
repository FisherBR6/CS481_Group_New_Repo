using System;
using UnityEngine;
using UnityEngine.UI;
using Stopwatch = System.Diagnostics.Stopwatch;
using System.Globalization;
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

    public String fileName;

    private void Start()
    {
        stopwatch = new Stopwatch();

        // Manually wire up listeners for each button
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

    // The method that is called when any button is pressed
    public void OnAnyRelevantButtonPressed(Button pressedButton)
    {
        //UnityEngine.Debug.Log($"Button pressed: {pressedButton.name}");

        if (!trackFlag)
        {
            switch (pressedButton.name)
            {
                // Numeric and alphabetic buttons
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "a":
                case "b":
                case "c":
                case "d":
                case "e":
                case "f":
                case "g":
                case "h":
                case "i":
                case "j":
                case "k":
                case "l":
                case "m":
                case "n":
                case "o":
                case "p":
                case "q":
                case "r":
                case "s":
                case "t":
                case "u":
                case "v":
                case "w":
                case "x":
                case "y":
                case "z":
                case "Tab":
                case "Delete":
                case "Enter":
                case "Caps":
                case "Spacebar":
                case "Comma":
                case "Period":
                    stopwatch.Start(); // Start the stopwatch
                    if(fileName == null)
                        fileName = $"TimeIntervalTracker{DateTime.Now:HH:mm:ss.fff}.csv";
                    UnityEngine.Debug.Log($"Button {pressedButton.name} pressed starting the application at time {DateTime.Now:HH:mm:ss.fff}");
                    prevButton = pressedButton;
                    //create a new csv file

                    trackFlag = true; // Set the flag to true after starting
                    break;
            }
        }
        else
        {
            // If trackFlag is true, handle as you did before:
            switch (pressedButton.name)
            {
                // Numeric and alphabetic buttons
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                case "a":
                case "b":
                case "c":
                case "d":
                case "e":
                case "f":
                case "g":
                case "h":
                case "i":
                case "j":
                case "k":
                case "l":
                case "m":
                case "n":
                case "o":
                case "p":
                case "q":
                case "r":
                case "s":
                case "t":
                case "u":
                case "v":
                case "w":
                case "x":
                case "y":
                case "z":
                case "Tab":
                case "Delete":
                case "Enter":
                case "Caps":
                case "Spacebar":
                case "Comma":
                case "Period":
                    stopwatch.Stop(); // Stop the stopwatch
                    UnityEngine.Debug.Log($"{prevButton.name} -> {pressedButton.name} in {stopwatch.ElapsedMilliseconds / 1000f:0.000} seconds");
                    WriteToCSV(pressedButton, $"{stopwatch.ElapsedMilliseconds / 1000f:0.000}");
                    prevButton = pressedButton;
                    //write buttons and time interval to the existing csv
                    

                    stopwatch.Reset(); // Reset the stopwatch
                    stopwatch.Start(); // Start again for the next interval
                    break;
            }
        }
    }

    public void WriteToCSV(Button currentButton, string interval)
    {
        // Open the file in append mode (if it doesn't exist, it will be created)
        using (var writer = new StreamWriter(fileName, true)) // 'true' for appending
        {
            // Write a line with the button names and the interval
            writer.WriteLine($"{prevButton.name},{currentButton.name},{interval}");
        }
    }

}
