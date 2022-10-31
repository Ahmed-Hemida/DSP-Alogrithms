using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            OutputIntervalIndices = new List<int>();
            OutputSamplesError = new List<float>();
            OutputEncodedSignal = new List<string>();
            float MaxAmp, MinAmp;
            List<float> Intervals = new List<float>();
            List<float> MidPoints = new List<float>();
            List<float> Res = new List<float>();

            // GET Level OR Bits
            if (InputLevel == 0)
            {
                InputLevel = (int)Math.Pow(2, InputNumBits);
            }
            if (InputNumBits == 0)
            {
                InputNumBits = (int)Math.Log(InputLevel, 2);
            }

            // GET Delta
            MaxAmp = InputSignal.Samples.Max();
            MinAmp = InputSignal.Samples.Min();
            float Delta = (MaxAmp - MinAmp) / InputLevel;

            // Intervals and Midpoints
            float temp = MinAmp;
            int a = 0;
            while (temp <= MaxAmp + 0.1)
            {
                Intervals.Add(temp);
                temp += Delta;
                temp = (float)Math.Round(temp, 3);
                float mid = (float)Math.Round((Intervals[a] + temp) / 2, 3);
                MidPoints.Add(mid);
                a++;
            }

            // Midpoint index and Quantized Value
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                for (int j = 0; j < InputLevel; j++)
                {
                    if (InputSignal.Samples[i] >= Intervals[j] && InputSignal.Samples[i] <= Intervals[j + 1])
                    {
                        OutputIntervalIndices.Add(j + 1);
                        Res.Add(MidPoints[j]);
                        break;
                    }
                }
            }
            OutputQuantizedSignal = new Signal(Res, true);

            // ERROR
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float r = OutputQuantizedSignal.Samples[i] - InputSignal.Samples[i];
                r = (float)Math.Round(r, 3);
                OutputSamplesError.Add(r);
            }

            // ENCODE
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                string encode = Convert.ToString(OutputIntervalIndices[i] - 1, 2).PadLeft(InputNumBits, '0');
                OutputEncodedSignal.Add(encode);
            }
        }
    }
}
