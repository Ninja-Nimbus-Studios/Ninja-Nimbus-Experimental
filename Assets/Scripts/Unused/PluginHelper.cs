// UNUSED
// Huge plugin helper function that allows data reading for accelerometer and gyroscope data reading when the phone is vibrating.
// This file needs to be used with the correct plugin set up: 
// The mental model is: xcode(edit swift/h/mm files) -> run in Unity -> renders automatically to swift and onto iOS
// you can not make swift files in Unity so you first make a "framework" in xcode and make a swift file, header file, and objective-c file.
// You then drag these files into Unity -> Assets -> Plugins -> iOS -> <YOUR-CHOICE-OF-PATH>
// Then write two scripts, one in Editor folder for Swift code post processing. The other C# script is in charge of IOS plugin. You would reference swift functions/objects/classes in this file through dll(Dynamically Linked Library).
// Build and run the project and Unity will automatically create xcodeproj file. This way, the path creation is done automatically.
// Now go ahead and edit function in swift!

// contact tatsuokumaus@gmail.com for a detailed pdf for working on this!

using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

// Imports for Buttersworth Filter
using System;
using System.Linq;

public class PluginHelper : MonoBehaviour
{
    [SerializeField] private Text textResult;
    public static List<float> listOfGyro = new();
    public static List<float> listOfAccel = new();

    public static int updateCount = 0;
    public static int fixedUpdateCount = 0;

    /**
     * Vibration Processing for Method 1 and 2
     */

    private static Queue<double> xaccelBufferQueue = new();
    private static Queue<double> yaccelBufferQueue = new();
    private static Queue<double> zaccelBufferQueue = new();
    private static List<double> filteredList = new();
    private static double[] xbufferArray = new double[0];
    private static double[] xreflect = new double[0];
    private static double[] xfilteredSignal = new double[0];
    private int accelfltfrq = 8; // Replace with the actual frequency value
    private int samplingRate = 50; // Replace with the actual sampling rate
    private int filterOrder = 5;
    private ButterworthFilter xButterworthFilter;

    /**
     * Vibration Processing for Method 3
     */ 
    public static SignalFilter grippy2ndFilter;
    public static SignalFilter grippy4thFilter;
    public static SignalFilter exampleHighIIRFilter;
    public static List<float> filtered2Array = new();
    public static List<float> filtered4Array = new();
    private float accelX;
    private float filteredOrder2AccelX;
    private float filteredOrder4AccelX;

    /**
     * Variables for Flying Bird
     */ 
    public static bool shouldJump = false;

    [DllImport("__Internal")]
    private static extern void _vibratePhone(double seconds, bool isVibrating);

    void Start()
    {
        textResult = GetComponent<Text>();
        Debug.Log("Application Started!");
        // AddTwoNumber();
        // VibratePhone();

        // Enable the gyroscope
        Input.gyro.enabled = true;

        // Prepare for Buttersworth Filter
        xButterworthFilter = new ButterworthFilter(filterOrder, accelfltfrq, samplingRate);

        // 2nd order high pass butterworth 8Hz at 50 Hz sampling rate
        grippy2ndFilter = new SignalFilter();
        grippy2ndFilter.Order2HighIIRFilter(0.48083842926409f, -0.961676858528181f, 0.48083842926409f, 0.671029090774096f, -0.252324626282266f);
    
        // 4th order high pass butterworth
        grippy4thFilter = new SignalFilter();
        grippy4thFilter.Order4HighIIRFilter(0.250377014350893f, -1.00150805740357f, 1.50226208610536f, -1.00150805740357f, 0.250377014350893f, 1.41198350119658f, -1.12276608082122f, 0.40807095188024f, -0.0632116957162536f);
    }

    /**
     * FixedUpdate runs more frequently than Update
     */ 
    void FixedUpdate()
    {
        // Update sensor data text in real time
        Vector3 accel = Input.acceleration;
        Vector3 gyro = Input.gyro.rotationRateUnbiased;
        string newText = $"*****\nAccelerometer from Input.acceleration:\nX: {accel.x:F2} Y: {accel.y:F2} Z: {accel.z:F2}\n"; 

        textResult.text = newText;

        listOfAccel.Add(accel.x);

        accelX = (float)accel.x;
        
        // Example of filter made with order 2 high pass filter
        filteredOrder2AccelX = grippy2ndFilter.Order2Filter(accelX);
        filtered2Array.Add(filteredOrder2AccelX);

        // order 4 filter
        filteredOrder4AccelX = grippy4thFilter.Order4Filter(accelX);
        if(filteredOrder4AccelX > 0.05) {
            shouldJump = true;
        } else {
            shouldJump = false;
        }
        filtered4Array.Add(filteredOrder4AccelX);
        fixedUpdateCount += 1;
        Debug.Log($"{fixedUpdateCount} times");
    }

    public void VibratePhone()
    {
        print("VibratePhone!");
        var secondsToVibrate = 4000.00;
        var isVibrating = true;
        _vibratePhone(secondsToVibrate, isVibrating);
        Debug.Log("Vibration has been done!");
    }

    public void SaveFile()
    {
        var accelPath = CreatePath("accel.csv");
        // var gyroPath = CreatePath("gyro.csv");
        var filtered2AccelPath = CreatePath("filtered2Accel.csv");
        var filtered4AccelPath = CreatePath("filtered4Accel.csv");
        SaveAccelToCSV(accelPath);
        // SaveGyroToCSV(gyroPath);
        SaveFiltered2AccelToCSV(filtered2AccelPath);
        SaveFiltered4AccelToCSV(filtered4AccelPath);
        Debug.Log($"Update called {updateCount} times and FixedUpdate called {fixedUpdateCount} times.");
    }

    public string CreatePath(string csvName)
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, "testDirectory");
        string filePath = Path.Combine(directoryPath, csvName);

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Debug.Log($"{directoryPath} doesn't exist.");
            Directory.CreateDirectory(directoryPath);
        }

        return filePath;
    }

    public void SaveAccelToCSV(string fileName)
    {
        Debug.Log("Accelorometer will save to " + fileName);

        try
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("X");
                for (int i = 0; i < listOfAccel.Count; i += 1)
                {
                    // Debug.Log($"{listOfAccel[i]},{listOfAccel[i + 1]},{listOfAccel[i + 2]}");
                    // writer.WriteLine($"{listOfAccel[i]},{listOfAccel[i + 1]},{listOfAccel[i + 2]}");
                    Debug.Log($"{listOfAccel[i]}");
                    writer.WriteLine($"{listOfAccel[i]}");
                }
                Debug.Log($"{listOfAccel.Count} data are written.");
            }
            Debug.Log("Accelorometer saved to " + fileName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save accelorometer to CSV: " + e.Message);
        }
    }

    public void SaveFiltered2AccelToCSV(string fileName)
    {
        Debug.Log("Filtered Accelorometer will save to " + fileName);

        try
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("X");
                for (int i = 0; i < filtered2Array.Count; i += 1)
                {
                    Debug.Log($"{filtered2Array[i]}");
                    writer.WriteLine($"{filtered2Array[i]}");
                }
                Debug.Log($"{filtered2Array.Count} data are written.");
            }
            Debug.Log("Filtered Accelorometer saved to " + fileName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save filtered accelorometer to CSV: " + e.Message);
        }
    }

    public void SaveFiltered4AccelToCSV(string fileName)
    {
        Debug.Log("Filtered Accelorometer will save to " + fileName);

        try
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("X");
                for (int i = 0; i < filtered4Array.Count; i += 1)
                {
                    Debug.Log($"{filtered4Array[i]}");
                    writer.WriteLine($"{filtered4Array[i]}");
                }
                Debug.Log($"{filtered4Array.Count} data are written.");
            }
            Debug.Log("Filtered Accelorometer saved to " + fileName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save filtered accelorometer to CSV: " + e.Message);
        }
    }

    public void SaveGyroToCSV(string fileName)
    {
        Debug.Log("Gyroscope will save to " + fileName);

        try
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("X,Y,Z");
                for (int i = 0; i < listOfGyro.Count; i += 3)
                {
                    Debug.Log($"{listOfGyro[i]},{listOfGyro[i + 1]},{listOfGyro[i + 2]}");
                    writer.WriteLine($"{listOfGyro[i]},{listOfGyro[i + 1]},{listOfGyro[i + 2]}");
                }
                Debug.Log($"{listOfGyro.Count} data are written.");
            }
            Debug.Log("Gyroscope saved to " + fileName);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save gyroscope to CSV: " + e.Message);
        }
    }
}

/**
 * Method 2: ChatGPT
 * GPT generated a sample Butterworth Filter that works with out current scope. 
 */ 
public class ButterworthFilter
{
    private double[] aCoefficients;
    private double[] bCoefficients;
    private double[] inputHistory;
    private double[] outputHistory;

    public ButterworthFilter(int order, double cutoffFrequency, double samplingRate)
    {
        CalculateCoefficients(order, cutoffFrequency, samplingRate);
        inputHistory = new double[aCoefficients.Length];
        outputHistory = new double[bCoefficients.Length];
    }

    public double[] ApplyHighPassFilter(double[] signal)
    {
        double[] result = new double[signal.Length];
        
        for (int i = 0; i < signal.Length; i++)
        {
            Array.Copy(inputHistory, 0, inputHistory, 1, inputHistory.Length - 1);
            inputHistory[0] = signal[i];
            
            double newValue = 0.0;
            
            for (int j = 0; j < bCoefficients.Length; j++)
            {
                newValue += bCoefficients[j] * inputHistory[j];
            }
            
            for (int j = 1; j < aCoefficients.Length; j++)
            {
                newValue -= aCoefficients[j] * outputHistory[j];
            }
            
            newValue /= aCoefficients[0];
            
            Array.Copy(outputHistory, 0, outputHistory, 1, outputHistory.Length - 1);
            outputHistory[0] = newValue;
            
            result[i] = newValue;
        }

        return result;
    }

    private void CalculateCoefficients(int order, double cutoffFrequency, double samplingRate)
    {
        double nyquist = 0.5 * samplingRate;
        double normalCutoff = cutoffFrequency / nyquist;
        
        double[] a = new double[order + 1];
        double[] b = new double[order + 1];
        
        double[] tA = new double[order + 1];
        double[] tB = new double[order + 1];
        
        double[] theta = new double[order];
        
        for (int i = 0; i < order; i++)
        {
            theta[i] = Math.PI * (2 * i + 1 + order) / (2.0 * order);
        }
        
        double st, ct, s2t, c2t;
        
        for (int i = 0; i < order; i++)
        {
            st = Math.Sin(theta[i]);
            ct = Math.Cos(theta[i]);
            s2t = 2 * st;
            c2t = 2 * ct;
            
            tA[0] = 1.0;
            tA[1] = -2 * ct;
            tA[2] = 1.0;
            
            tB[0] = (1.0 + st) / 2.0;
            tB[1] = -(1.0 + st);
            tB[2] = (1.0 + st) / 2.0;
            
            for (int j = 0; j < 3; j++)
            {
                a[j] += tA[j];
                b[j] += tB[j];
            }
        }
        
        double scale = a[0];
        
        for (int i = 0; i < a.Length; i++)
        {
            a[i] /= scale;
        }
        
        scale = b[0];
        
        for (int i = 0; i < b.Length; i++)
        {
            b[i] /= scale;
        }
        
        aCoefficients = a;
        bCoefficients = b;
    }
}

/**
 * Method 2: ChatGPT
 * GPT generated a sample Butterworth Filter that works with out current scope.
 */ 
public class UtilMethods
{
    public static double[] PadSignal(double[] signal, string method)
    {
        double[] newSignal;
        // Implement your padding logic here
        switch (method)
        {
            case "reflect":
                double[] revSig = Reverse(signal);
                double[] newSig = new double[] { };
                newSig = ConcatenateArray(newSig, revSig);
                newSig = ConcatenateArray(newSig, signal);
                newSig = ConcatenateArray(newSig, revSig);
                newSignal = newSig;
                break;
            default:
                throw new ArgumentException("padSignalforConvolution modes can only be reflect, constant, nearest, mirror, or wrap");
        }

        return newSignal;
    }

    public static double[] ConcatenateArray(double[] arr1, double[] arr2)
    {
        double[] output = new double[arr1.Length + arr2.Length];
        Array.Copy(arr1, 0, output, 0, arr1.Length);
        Array.Copy(arr2, 0, output, arr1.Length, arr2.Length);
        return output;
    }

    public static double[] Reverse(double[] arr)
    {
        double[] inverted = new double[arr.Length];
        for (int i = 0; i < inverted.Length; i++)
        {
            inverted[i] = arr[arr.Length - 1 - i];
        }
        return inverted;
    }

}
