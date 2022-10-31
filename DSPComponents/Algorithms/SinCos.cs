using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            float f = AnalogFrequency / SamplingFrequency; //  F / Fs
            float res;
            samples = new List<float>();
            switch (type)
            {
                // y(t) = A Cos(2πft + θ))
                case "cos":
                    for (int t = 0; t < SamplingFrequency; t++)
                    {
                        res = A * (float)Math.Cos(2 * Math.PI * f * t + PhaseShift);
                        samples.Add(res);
                    }
                    break;

                // y(t) = A Sin(2πft + θ)
                case "sin":
                    for (int t = 0; t < SamplingFrequency; t++)
                    {
                        res = A * (float)Math.Sin(2 * Math.PI * f * t + PhaseShift);
                        samples.Add(res);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
