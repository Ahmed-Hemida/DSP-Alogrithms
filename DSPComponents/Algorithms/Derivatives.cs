using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {

            // First Derivative = x(n) - x(n-1)
            Shifter right = new Shifter();
            right.InputSignal = InputSignal;
            right.ShiftingValue = -1;
            right.Run();
            List<float> r = new List<float>();
            for (int i = 1; i < InputSignal.Samples.Count - 1; i++)
            {
                r.Add(InputSignal.Samples[i + 1] - right.OutputShiftedSignal.Samples[i]);
            }
            FirstDerivative = new Signal(r, false);

            // Second Derivative = x(n+1) - x(n) - first der 
            //x(n+1) - 2x(n) - x(n-1)  
            Shifter left = new Shifter();
            left.InputSignal = InputSignal;
            left.ShiftingValue = 1;
            left.Run();
            List<float> l = new List<float>();
            for (int i = 1; i < InputSignal.Samples.Count; i++)
            {
                l.Add(left.OutputShiftedSignal.Samples[i] - InputSignal.Samples[i - 1] - FirstDerivative.Samples[i - 1]);
            }
            SecondDerivative = new Signal(l, false);

        }
    }
}
