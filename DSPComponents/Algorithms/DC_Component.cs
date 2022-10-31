using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> res = new List<float>();
            float avg = Queryable.Average(InputSignal.Samples.AsQueryable());
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                res.Add(InputSignal.Samples[i] - avg);
            }
            OutputSignal = new Signal(res, false);
        }
    }
}
