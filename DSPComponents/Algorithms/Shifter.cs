using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {

            // if periodric>> folding happend >> true >> make shifting *-1
            // if not periodric >>folding not happend>> false>> make shifting *1

            int individualSampleindces;
            List<int> mylistindces = new List<int>();
            List<float> mylistsamples = new List<float>();
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                mylistsamples.Add(InputSignal.Samples[i]);
            }

            if (InputSignal.Periodic == true)  // if periodric>> folding happend
            {
                ShiftingValue = ShiftingValue * -1;
                for (int i = 0; i < InputSignal.Samples.Count(); i++)
                {
                    individualSampleindces = InputSignal.SamplesIndices[i] - ShiftingValue;
                    mylistindces.Add(individualSampleindces);

                }
            }
            else
            {
                ShiftingValue = ShiftingValue * 1;
                for (int i = 0; i < InputSignal.Samples.Count(); i++)
                {
                    individualSampleindces = InputSignal.SamplesIndices[i] - ShiftingValue;
                    mylistindces.Add(individualSampleindces);
                }
            }



            OutputShiftedSignal = new Signal(mylistsamples, mylistindces, !InputSignal.Periodic);

        }
    }
}