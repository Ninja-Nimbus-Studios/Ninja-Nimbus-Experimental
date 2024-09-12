// UNUSED
// Used with pluginhelper.cs for filtering out the signal filter in order to read and process data. 
// We were able to check if signal was raw or not by looking at the unfiltered data, but this was needed in verifying whether the signal was REALLY raw or not
// in a rigorous way


using UnityEngine;

/**
 * Imports for Buttersworth Filter
 * Method 3: C# library
 * FilterData class is implemented by Marius Rubo to apply 2nd order high pass butterworth filter
 * https://github.com/mariusrubo/Unity-IIR-Realtime-Filtering/blob/master/FilterData.cs
 */
public class SignalFilter
{
    public float a0;
    public float a1;
    public float a2;
    public float a3;
    public float a4;
    public float b1;
    public float b2;
    public float b3;
    public float b4;

    public float x1;
    public float x2;
    public float x3;
    public float x4;
    public float y1;
    public float y2;
    public float y3;
    public float y4;

    // two parameters indicate a 2nd order Butterworth low-pass filter
    // equation obtained here: https://www.codeproject.com/Tips/1092012/A-Butterworth-Filter-in-Csharp
    // note that you can also use the five-parameter-solution described below. This is just for convenience. 
    public void LowIIRFilter(float samplingrate, float frequency)
    {
        const float pi = 3.14159265358979f;
        float wc = Mathf.Tan(frequency * pi / samplingrate);
        float k1 = 1.414213562f * wc;
        float k2 = wc * wc;
        a0 = k2 / (1 + k1 + k2);
        a1 = 2 * a0;
        a2 = a0;
        float k3 = a1 / k2;
        b1 = -2 * a0 + k3;
        b2 = 1 - (2 * a0) - k3;

        x1 = x2 = y1 = y2 = 0;
    }
	    
    // three parameters indicates a notch filter
    // equation obtained here http://dspguide.com/ch19/3.htm
    public void NotchIIRFilter(float samplingrate, float frequency, float Bandwidth)
    {
        float Pi = 3.141592f;
        float BW = Bandwidth / samplingrate;
        float f = frequency / samplingrate;
        float R = 1 - 3 * BW;
        float K = (1 - 2 * R * Mathf.Cos(2 * Pi * f) + R * R) / (2 - 2 * Mathf.Cos(2 * Pi * f));
        a0 = K;
        a1 = -2 * K * Mathf.Cos(2 * Pi * f);
        a2 = K;
        b1 = 2 * R * Mathf.Cos(2 * Pi * f);
        b2 = -R * R;

        x1 = x2 = y1 = y2 = 0;
    }

    /*
    Five parameters indicate a generic filter. 
    This is necessary for Butterworth high-pass filters or other IIR filters (because I could not implement the process of obtaining these parameters in here).  
        Easy way to find these parameters is using R's "signal" package butter function, and convert parameters like this: 
    
    samplingRate <- 500 # in Hz
    cutoff <- 1 # in Hz
    order <- 2 # 
    nyquist <- samplingRate/2
    W <- cutoff/nyquist
    bf <- signal::butter(order, W, type = "high")
        a0<- bf$b[1] / bf$a[1]
        a1<- bf$b[2] / bf$a[1]
        a2<- bf$b[3] / bf$a[1]
        b1<- -bf$a[2] / bf$a[1]
        b2<- -bf$a[3] / bf$a[1]
    paste0("new IIRFilter(",a0, "f, " ,a1, "f, ", a2, "f, ", b1, "f, ", b2, "f);") 
    */

    /**
    * Set coefficients for Order 2 high pass filter
    */
    public void Order2HighIIRFilter(float a0in, float a1in, float a2in, float b1in, float b2in)
    {
        a0 = a0in;
        a1 = a1in;
        a2 = a2in;
        b1 = b1in;
        b2 = b2in;

        x1 = x2 = y1 = y2 = 0;
    }

    // filter data. Each IIRFilter stores two data points of filtered and unfiltered data. Therefore, filtering should be continuous and not be switched on and off. 
    // Furthermore, each IIRFilter may only process one data stream. If you intend to filter two data streams with the same kind of filter, you need to initialize 
    // two IIRFilters accordingly (e.g. "Notch50_1" and "Notch50_2"), each filtering only one data stream. 
    public float Order2Filter(float x0)
    {
        // Apply filter to data point
        float y = a0 * x0 + a1 * x1 + a2 * x2 + b1 * y1 + b2 * y2;

        // Update data point to next
        x2 = x1;
        x1 = x0;
        y2 = y1;
        y1 = y;

        return y;
    }

    /**
    * Set coefficients for order 4 high pass filter.
    samplingRate <- 500 # in Hz
    cutoff <- 1 # in Hz
    order <- 2 # 
    nyquist <- samplingRate/2
    W <- cutoff/nyquist
    bf2 <- signal::butter(order, W, type = "high")
        a0<- bf2$b[1] / bf2$a[1]
        a1<- bf2$b[2] / bf2$a[1]
        a2<- bf2$b[3] / bf2$a[1]
        a3<- bf2$b[4] / bf2$a[1]
        a4<- bf2$b[5] / bf2$a[1]
        b1<- -bf2$a[2] / bf2$a[1]
        b2<- -bf2$a[3] / bf2$a[1]
        b3<- -bf2$a[4] / bf2$a[1]
        b4<- -bf2$a[5] / bf2$a[1]
    paste0("new IIRFilter(",a0, "f, " ,a1, "f, ", a2, "f, ", a3, "f, ", a4, "f, ", b1, "f, ", b2, "f, ", b3, "f, ", b4, "f);") 
    */
    public void Order4HighIIRFilter(float a0in, float a1in, float a2in, float a3in, float a4in, float b1in, float b2in, float b3in, float b4in)
    {
        a0 = a0in;
        a1 = a1in;
        a2 = a2in;
        a3 = a3in;
        a4 = a4in;
        b1 = b1in;
        b2 = b2in;
        b3 = b3in;
        b4 = b4in;

        x1 = x2 = x3 = x4 = y1 = y2 = y3 = y4 = 0;
    }

    public float Order4Filter(float x0)
    {
        // Apply filter to data point
        float y = a0 * x0 + a1 * x1 + a2 * x2 + a3 * x3 + a4 * x4 
                  + b1 * y1 + b2 * y2 + b3 * y3 + b4 * y4;

        // Update data point to next
        x4 = x3;
        x3 = x2;
        x2 = x1;
        x1 = x0;

        y4 = y3;
        y3 = y2;
        y2 = y1;
        y1 = y;

        return y;
    }
}
