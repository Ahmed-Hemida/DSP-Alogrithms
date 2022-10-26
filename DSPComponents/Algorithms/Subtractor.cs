using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            Signal a1 = InputSignal1;
            Signal a2 = InputSignal2;
            bool Periodic;
            int loopCounter = 0;
            if (a1.Samples.Count() > a2.Samples.Count())
            {
                loopCounter = a1.Samples.Count();
                Periodic = a1.Periodic;

            }
            else
            {
                loopCounter = a2.Samples.Count();
                Periodic = a2.Periodic;
            }


            List<float> SigSamples = new List<float>(unchecked((int)loopCounter));


            for (int i = 0; i < loopCounter; i++)
            {
                if ((!float.IsNaN(a1.Samples[1]) && !float.IsNaN(a2.Samples[i])))
                {
                
                    SigSamples.Add(a1.Samples[i] - a2.Samples[i]);
                }
                else if ((float.IsNaN(a1.Samples[i]) && !float.IsNaN(a2.Samples[i])))
                {
                    SigSamples.Add(a2.Samples[i]);
                }
                else if ((!float.IsNaN(a1.Samples[i]) && float.IsNaN(a2.Samples[i])))
                {
                    SigSamples.Add(a1.Samples[i]);
                }


            }

            OutputSignal = new Signal(SigSamples, Periodic);
        }
    }
}