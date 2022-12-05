using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }
        public float normalize()
        {

            int N = (InputSignal1.Samples.Count());
            float s1 = 0; float s2 = 0;
            float res;
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                s1 += (InputSignal1.Samples[i] * InputSignal1.Samples[i]);
            }
            for (int i = 0; i < InputSignal2.Samples.Count; i++)
            {
                s2 += (InputSignal2.Samples[i] * InputSignal2.Samples[i]);
            }
            res = (float)(Math.Sqrt((s1 * s2))) / N;
            return res;
        }
        public override void Run()
        {
            OutputNormalizedCorrelation = new List<float>();
            if (InputSignal2 == null)
            {
                List<float> l = new List<float>();
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    l.Add(InputSignal1.Samples[i]);
                }
                InputSignal2 = new Signal(l, false);
            }
            int N = (InputSignal1.Samples.Count());
            float norm = normalize();
            DiscreteFourierTransform d1 = new DiscreteFourierTransform();
            DiscreteFourierTransform d2 = new DiscreteFourierTransform();
            d1.InputTimeDomainSignal = InputSignal1;
            d2.InputTimeDomainSignal = InputSignal2;
            d1.Run();
            d2.Run();
            List<Complex> L1 = new List<Complex>();
            List<Complex> L2 = new List<Complex>();
            List<Complex> RES = new List<Complex>();
            List<float> l1 = new List<float>();
            List<float> l2 = new List<float>();
            for (int i = 0; i < N; i++)
            {
                L1.Add(Complex.FromPolarCoordinates(d1.OutputFreqDomainSignal.FrequenciesAmplitudes[i], d1.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
            }
            for (int i = 0; i < N; i++)
            {
                L2.Add(Complex.FromPolarCoordinates(d2.OutputFreqDomainSignal.FrequenciesAmplitudes[i], d2.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
            }
            for (int i = 0; i < N; i++)
            {
                L1[i] = Complex.Conjugate(L1[i]);
            }
            for (int i = 0; i < N; i++)
            {
                RES.Add(Complex.Multiply(L1[i], L2[i]) / N);
                l1.Add((float)RES[i].Magnitude);
                l2.Add((float)RES[i].Phase);
            }
            Signal s = new Signal(false, d1.OutputFreqDomainSignal.Frequencies, l1, l2);
            InverseDiscreteFourierTransform id = new InverseDiscreteFourierTransform();
            id.InputFreqDomainSignal = s;
            id.Run();
            OutputNonNormalizedCorrelation = id.OutputTimeDomainSignal.Samples;
            for (int i = 0; i < N; i++)
            {
                OutputNormalizedCorrelation.Add(OutputNonNormalizedCorrelation[i] / norm);
            }
        }
    }
}
