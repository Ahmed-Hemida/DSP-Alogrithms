using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> out_sampel = new List<float>();

            float sum = 0;
            for (int i = 0; i < InputSignal1.Samples.Count + InputSignal2.Samples.Count - 1; i++)
            {
                for (int j = 0; j < InputSignal2.Samples.Count; j++)
                {
                    if (i - j >= 0 && i - j < InputSignal1.Samples.Count)
                    {
                        sum += InputSignal1.Samples[i - j] * InputSignal2.Samples[j];

                    }
                }
                out_sampel.Add(sum);
                sum = 0;

            }
            OutputConvolvedSignal = new Signal(out_sampel, false);

        }
    }
}