using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            List<float> Freqs = new List<float>();
            List<float> AMP = new List<float>();
            List<float> PH = new List<float>();
            Complex harmonic;
            int N = InputTimeDomainSignal.Samples.Count;
            //X[k]= n∑ x[n]e^(−j2πkn/N).
            for (int k = 0; k < N; k++)
            {
                harmonic = new Complex();
                for (int n = 0; n < N; n++)
                {
                    double num = -k * 2 * Math.PI * n / N;
                    Complex l = new Complex(InputTimeDomainSignal.Samples[n], 0);
                    Complex r = new Complex(Math.Cos(num), Math.Sin(num));
                    harmonic = Complex.Add(harmonic, l * r);
                }
                AMP.Add((float)harmonic.Magnitude);
                PH.Add((float)harmonic.Phase);
                Freqs.Add(k);
            }
            OutputFreqDomainSignal = new Signal(false, Freqs, AMP, PH);
        }
    }
}
