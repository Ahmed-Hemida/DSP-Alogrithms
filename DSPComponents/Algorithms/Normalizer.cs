using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            int loopCounter = 0;

            loopCounter = InputSignal.Samples.Count();

            List<float> SigSamples = new List<float>(unchecked((int)loopCounter));
            int a= (int)InputSignal.Samples[0];
            int z = (int)InputSignal.Samples[loopCounter - 1];

            for (int i = 0; i < loopCounter; i++)
            {
                //if ((!float.IsNaN(InputSignal.Samples[i])))
                //{
                    float x = (((InputSignal.Samples[i]- a) * (InputMaxRange - InputMinRange)) / (z - a))- Math.Abs(InputMinRange);
                    SigSamples.Add(x);

                //}
            }

            OutputNormalizedSignal = new Signal(SigSamples, InputSignal.Periodic);
        }
    }
}
