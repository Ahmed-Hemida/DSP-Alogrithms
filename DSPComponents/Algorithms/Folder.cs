using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {



            int individualSampleindces;
            List<int> mylistindces = new List<int>();
            int counter = 0;

            float individualSample;
            List<float> mylistsamples = new List<float>();
            for (int i = InputSignal.Samples.Count() - 1; i >= 0; i--)
            {
                individualSampleindces = InputSignal.SamplesIndices[i] * -1;
                mylistindces.Add(individualSampleindces);
                individualSample = InputSignal.Samples[i];
                mylistsamples.Add(individualSample);
            }

            OutputFoldedSignal = new Signal(mylistsamples, mylistindces, !InputSignal.Periodic);
        }
    }
}