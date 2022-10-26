using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            int loopCounter = 0;

                loopCounter = InputSignal.Samples.Count();

            List<float> SigSamples = new List<float>(unchecked((int)loopCounter));

            for (int i = 0; i < loopCounter; i++)
                    {
                        if ((!float.IsNaN(InputSignal.Samples[i]) ))
                        {
                            SigSamples.Add(InputSignal.Samples[i] * InputConstant);

                        }
                      }

            OutputMultipliedSignal = new Signal(SigSamples, InputSignal.Periodic);
        }
    }
}
