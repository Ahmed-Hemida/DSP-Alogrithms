using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {

            // First Derivative = x(n) - x(n-1)

            List<float> st = new List<float>();

            for (int i = 1; i < InputSignal.Samples.Count; i++)
            {
                st.Add(InputSignal.Samples[i] - InputSignal.Samples[i - 1]);
            }
            FirstDerivative = new Signal(st, false);

            // Second Derivative  
            //Y(n)= x(n+1)-2x(n)+x(n-1)  
            List<float> nd = new List<float>();

            for (int i = 1; i < InputSignal.Samples.Count - 1; i++)
            {
                nd.Add(InputSignal.Samples[i + 1] - 2 * (InputSignal.Samples[i]) + InputSignal.Samples[i - 1]);
            }
            SecondDerivative = new Signal(nd, false);
        }
    }
}