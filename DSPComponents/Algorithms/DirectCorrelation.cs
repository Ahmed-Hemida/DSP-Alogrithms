using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            int c = InputSignal1.Samples.Count;
            if (InputSignal2 == null)
            {
                List<float> sampels = new List<float>();

                InputSignal2 = new Signal(sampels, false);
                for (int i = 0; i < c; i++)
                    InputSignal2.Samples.Add(InputSignal1.Samples[i]);
            }
            double norm = 0;
            double sum1 = 0, sum2 = 0;
            for (int i = 0; i < c; i++)
            {
                sum1 += Math.Pow(InputSignal1.Samples[i], 2);
                sum2 += Math.Pow(InputSignal2.Samples[i], 2);

            }
            norm = Math.Sqrt(sum1 * sum2) / c;
            int co;
            List<float> non_norm_s = new List<float>();
            List<float> norm_s = new List<float>();
            double temp2 = 1;

            for (int i = 0; i < c; i++)
            {
                co = i;
                double temp = 0;
                temp2 = 1;

                for (int j = 0; j < c; j++)
                {
                    if (co >= c)
                    {
                        if (InputSignal1.Periodic != true)
                        {
                            temp2 = 0;
                        }
                        co = 0;
                    }
                    temp += InputSignal1.Samples[j] * InputSignal2.Samples[co] * temp2;

                    co++;



                }
                non_norm_s.Add((float)temp / c);
                norm_s.Add(non_norm_s[i] / (float)norm);
            }

            OutputNonNormalizedCorrelation = non_norm_s;
            OutputNormalizedCorrelation = norm_s;
        }
    }
}