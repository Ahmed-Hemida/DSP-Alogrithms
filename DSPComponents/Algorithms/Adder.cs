using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            Signal a1 = this.InputSignals[0];
            Signal a2 = this.InputSignals[1];
            bool Periodic;
            int loopCounter = 0;
            if (a1.Samples.Count() > a2.Samples.Count()) 
            {
                loopCounter = a1.Samples.Count();
                Periodic = a1.Periodic;

            } else 
            {
                loopCounter = a2.Samples.Count();
                Periodic = a2.Periodic;
            }


            List<float> SigSamples = new List<float>(unchecked((int)loopCounter));


            for (int i = 0; i < loopCounter ; i++)
            {
                if ((!float.IsNaN(a1.Samples[1]) && !float.IsNaN(a2.Samples[i])))
                {
                    SigSamples.Add(a1.Samples[i]+ a2.Samples[i]);
                }
                else if ((float.IsNaN(a1.Samples[i]) && !float.IsNaN(a2.Samples[i])))
                {
                    SigSamples.Add( a2.Samples[i]);
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