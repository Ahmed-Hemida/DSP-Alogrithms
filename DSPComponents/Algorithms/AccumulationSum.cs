using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            int N = InputSignal.Samples.Count;
            float Sum = 0;
            List<float> values = new List<float>();
            for (int i = 0; i < N; i++)
            {
                Sum += InputSignal.Samples[i];
                values.Add(Sum);
            }
            OutputSignal = new Signal(values, false);
        }
    }
}
