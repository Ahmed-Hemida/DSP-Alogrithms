using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            int N = InputTimeDomainSignal.Samples.Count;
            float Pi = (float)Math.PI;
            float R, I;
            List<float> Amplitudes = new List<float>();
            List<float> PhaseShifts = new List<float>();
            // x(n)(Cos(θ) - j Sin(θ))
            // R - j I
            // Amp = (R^2 + I^2 )^1/2
            // Φ = tan^-1 (I / R)
            for (int k = 0; k < N; k++)
            {
                R = 0;
                I = 0;
                for (int n = 0; n < N; n++)
                {
                    float θ = (2 * k * Pi * n) / N;
                    R += (float)Math.Cos(θ) * InputTimeDomainSignal.Samples[n];
                    I += (float)Math.Sin(θ) * - InputTimeDomainSignal.Samples[n];
                }
                float Amp2 = (float)(Math.Pow(R, 2) + Math.Pow(I, 2));
                float Amp = (float)Math.Sqrt(Amp2);
                float Φ = (float)Math.Atan2(I, R);
                Amplitudes.Add(Amp);
                PhaseShifts.Add(Φ);
            }
            OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Samples, true);
            OutputFreqDomainSignal.FrequenciesAmplitudes = Amplitudes;
            OutputFreqDomainSignal.FrequenciesPhaseShifts = PhaseShifts;
        }
    }
}
