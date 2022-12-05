using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
namespace DSPAlgorithms.Algorithms
{

    public class FastConvolution : Algorithm
    {

        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }


        public override void Run()
        {
            int N = InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1;
            for (int i = 0; i < N; i++)
            {
                if (i >= InputSignal1.Samples.Count)
                {
                    InputSignal1.Samples.Add(0);
                }
            }
            for (int i = 0; i < N; i++)
            {
                if (i >= InputSignal2.Samples.Count)
                {
                    InputSignal2.Samples.Add(0);
                }
            }
            //DFT  X[k]= n∑ x[n]e^(−j2πkn/N).
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
                //confart (x,y) to (r,θ) this is From Polar Coordinates
                L1.Add(Complex.FromPolarCoordinates(d1.OutputFreqDomainSignal.FrequenciesAmplitudes[i], d1.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
            }
            for (int i = 0; i < N; i++)
            {
                //confart (x,y) to (r,θ) this is From Polar Coordinates
                L2.Add(Complex.FromPolarCoordinates(d2.OutputFreqDomainSignal.FrequenciesAmplitudes[i], d2.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));
            }
            for (int i = 0; i < N; i++)
            {
                // Complex number = a+bJ image number
                RES.Add(Complex.Multiply(L1[i], L2[i]));
                l1.Add((float)RES[i].Magnitude);
                l2.Add((float)RES[i].Phase);
            }
            Signal s = new Signal(false, d1.OutputFreqDomainSignal.Frequencies, l1, l2);
            InverseDiscreteFourierTransform idft = new InverseDiscreteFourierTransform();
            idft.InputFreqDomainSignal = s;
            idft.Run();
            OutputConvolvedSignal = new Signal(idft.OutputTimeDomainSignal.Samples, false);
        }
    }
}
