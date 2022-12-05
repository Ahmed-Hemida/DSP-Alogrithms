using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            List<float> L = new List<float>();
            int N = InputFreqDomainSignal.Frequencies.Count;
            Complex harmonic;
            for (int k = 0; k < N; k++)
            {
                harmonic = new Complex();
                for (int n = 0; n < N; n++)
                {
                    double num = k * 2 * Math.PI * n / N;
                    Complex l = new Complex(InputFreqDomainSignal.FrequenciesAmplitudes[n] * Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[n]), InputFreqDomainSignal.FrequenciesAmplitudes[n] * Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[n]));
                    Complex r = new Complex(Math.Cos(num), Math.Sin(num));
                    harmonic = Complex.Add(harmonic, Complex.Multiply(l, r));
                }
                L.Add((float)harmonic.Real / N);
            }
            OutputTimeDomainSignal = new Signal(L, false);
        }
    }
}
