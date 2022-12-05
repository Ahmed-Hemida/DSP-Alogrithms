using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float Sum;
            List<float> L = new List<float>();
            int N = InputSignal.Samples.Count();
            for (int k = 0; k <N ; k++)
            {
                Sum = 0;
                for (int n = 0; n < N; n++)
                {
                    Sum += (float)(InputSignal.Samples[n] * (Math.Cos((float)(((2 * n) - 1) * (2 * k - 1) * (Math.PI)) / (4 * N))));
                }
                L.Add((float)(Math.Sqrt(2.0f / InputSignal.Samples.Count())) * Sum);
            }
            OutputSignal = new Signal(L, false);
        }
    }
}
