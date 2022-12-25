using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {

            //throw new NotImplementedException();
            List<float> output = new List<float>();
            float N = InputSignal.Samples.Count;
            for (int k = 0; k < N; k++)
            {
                double sum = 0.0f;
                for (int n = 0; n < InputSignal.Samples.Count; n++)
                {
                    sum += InputSignal.Samples[n] * Math.Cos((Math.PI / (4 * N)) * (2 * n - 1) * (2 * k - 1));
                }
                double result;
                result = Math.Sqrt(2 / N) * sum;
                output.Add((float)result);

            }
            OutputSignal = new Signal(output, InputSignal.Periodic);
        }
    }
}
