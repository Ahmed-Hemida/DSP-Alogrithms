using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; } // Sampling Freq
        public float? InputCutOffFrequency { get; set; } //Fc
        public float? InputF1 { get; set; } // Band/Stop pass
        public float? InputF2 { get; set; } // Band/Stop pass
        public float InputStopBandAttenuation { get; set; } // Stop Band Attenuation
        public float InputTransitionBand { get; set; } // transition width
        public Signal OutputHn { get; set; } // w(d) * h(d)
        public Signal OutputYn { get; set; } // Convolution (Hn , Input)

        float N, Fc, Fc1, Fc2;
        public override void Run()
        {
            string windowName = "";
            float x = 0;
            List<int> outInd = new List<int>();
            List<float> Hd = new List<float>();
            List<float> Wd = new List<float>();
            List<float> HdWd = new List<float>();

            if (InputStopBandAttenuation <= 21)
            {
                windowName = "rectangle";
                x = 0.9f;
            }
            else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
            {
                windowName = "hanning";
                x = 3.1f;
            }
            else if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53)
            {
                windowName = "hamming";
                x = 3.3f;

            }
            else if (InputStopBandAttenuation > 53 && InputStopBandAttenuation <= 74)
            {
                windowName = "blackman";
                x = 5.5f;

            }
            float deltaF = InputTransitionBand / InputFS;  // normalize
            N = x / deltaF;
            N = (float)Math.Ceiling(N); // upper bound
            if (N % 2 == 0) N++;
            int n = (int)((N - 1) / 2);
            for (int i = -n; i <= n; i++)
            {
                outInd.Add(i);
            }
            switch (InputFilterType) //LOW, HIGH, BAND_PASS, BAND_STOP
            {
                case FILTER_TYPES.LOW:
                    Fc = (float)(InputCutOffFrequency + (InputTransitionBand / 2)); //Fc' = Fc + ^f/2
                    Fc = Fc / InputFS;
                    for (int i = -n; i <= n; i++)
                    {
                        Hd.Add(LowPass(i));
                    }
                    break;
                case FILTER_TYPES.HIGH:
                    Fc = (float)(InputCutOffFrequency - (InputTransitionBand / 2)); //Fc' = Fc + ^f/2
                    Fc = Fc / InputFS;
                    for (int i = -n; i <= n; i++)
                    {
                        Hd.Add(HighPass(i));
                    }
                    break;
                case FILTER_TYPES.BAND_PASS:
                    Fc1 = (float)(InputF1 - (InputTransitionBand / 2));
                    Fc2 = (float)(InputF2 + (InputTransitionBand / 2));
                    Fc1 = Fc1 / InputFS;
                    Fc2 = Fc2 / InputFS;
                    for (int i = -n; i <= n; i++)
                    {
                        Hd.Add(BandPass(i));
                    }
                    break;
                case FILTER_TYPES.BAND_STOP:
                    Fc1 = (float)(InputF1 + (InputTransitionBand / 2));
                    Fc2 = (float)(InputF2 - (InputTransitionBand / 2));
                    Fc1 = Fc1 / InputFS;
                    Fc2 = Fc2 / InputFS;
                    for (int i = -n; i <= n; i++)
                    {
                        Hd.Add(BandStop(i));
                    }
                    break;
                default:
                    break;
            }
            switch (windowName)
            {
                case "rectangle":
                    for (int i = -n; i <= n; i++)
                    {
                        Wd.Add(1);
                    }
                    break;
                case "hanning":
                    for (int i = -n; i <= n; i++)
                    {
                        Wd.Add(hanning(i));
                    }
                    break;
                case "hamming":
                    for (int i = -n; i <= n; i++)
                    {
                        Wd.Add(hamming(i));
                    }
                    break;
                case "blackman":
                    for (int i = -n; i <= n; i++)
                    {
                        Wd.Add(blackman(i));
                    }
                    break;
            }
            for (int i = 0; i < Hd.Count; i++)
            {
                HdWd.Add(Hd[i] * Wd[i]);
            }
            OutputHn = new Signal(HdWd, outInd, false);
            DirectConvolution conv = new DirectConvolution();
            conv.InputSignal1 = InputTimeDomainSignal;
            conv.InputSignal2 = new Signal(new List<float>(), new List<int>(), false);
            for (int i = 0; i < OutputHn.Samples.Count; i++)
            {
                conv.InputSignal2.Samples.Add(OutputHn.Samples[i]);
                conv.InputSignal2.SamplesIndices.Add(OutputHn.SamplesIndices[i]);
            }
            conv.Run();
            OutputYn = conv.OutputConvolvedSignal;
        }
        float LowPass(int n)
        {
            return (n == 0)? 2 * Fc:(float)(2 * Fc * (Math.Sin(n * 2 * Math.PI * Fc)) / (n * 2 * Math.PI * Fc));
        }
        float HighPass(int n)
        {
            return (n == 0)? 1 - (2 * Fc): (float)(-2 * Fc * (Math.Sin(n * 2 * Math.PI * Fc)) / (n * 2 * Math.PI * Fc));
        }
        float BandPass(int n)
        {
            return (n == 0)?2 * (Fc2 - Fc1):(float)(2 * Fc2 * (Math.Sin(n * 2 * Math.PI * Fc2)) / (n * 2 * Math.PI * Fc2)) - (float)(2 * Fc1 * (Math.Sin(n * 2 * Math.PI * Fc1)) / (n * 2 * Math.PI * Fc1));
        }
        float BandStop(int n)
        {
           return (n == 0)?  1 - (2 * (Fc2 - Fc1)):(float)(2 * Fc1 * (Math.Sin(n * 2 * Math.PI * Fc1)) / (n * 2 * Math.PI * Fc1)) - (float)(2 * Fc2 * (Math.Sin(n * 2 * Math.PI * Fc2)) / (n * 2 * Math.PI * Fc2));
        }

        float hanning(int n)
        {
            return (float)(0.5 + 0.5 * (float)Math.Cos(2 * (float)Math.PI * n / N));
        }
        float hamming(int n)
        {
            return (float)(0.54 + 0.46 * (float)Math.Cos(2 * (float)Math.PI * n / N));
        }
        float blackman(int n)
        {
            return (float)(0.42 + (float)(0.5 * Math.Cos((2 * Math.PI * n) / (N - 1))) + (float)(0.08 * Math.Cos((4 * Math.PI * n) / (N - 1))));
        }

    }
}
