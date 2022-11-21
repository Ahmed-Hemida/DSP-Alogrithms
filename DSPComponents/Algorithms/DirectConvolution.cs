using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            Signal X = InputSignal1;
            Signal H = InputSignal2;
            int minX = InputSignal1.SamplesIndices.Min();
            int maxX = InputSignal1.SamplesIndices.Max();
            int minH = InputSignal2.SamplesIndices.Min();
            int maxH = InputSignal2.SamplesIndices.Max();

            List<float> value = new List<float>();
            int shiftH = 0, shiftX = 0, SHIFT;
            if (minH < 0)
            {
                for (int i = 0; i < H.SamplesIndices.Count; i++)
                {
                    H.SamplesIndices[i] -= minH;
                }
                shiftH = minH;
                minH = H.SamplesIndices.Min();
                maxH = H.SamplesIndices.Max();
            }
            if (minX < 0)
            {
                for (int i = 0; i < X.SamplesIndices.Count; i++)
                {
                    X.SamplesIndices[i] -= minX;
                }
                shiftX = minX;
                minX = X.SamplesIndices.Min();
                maxX = X.SamplesIndices.Max();
            }
            SHIFT = shiftX + shiftH;
            int minN = minX + minH;
            int maxN = maxX + maxH;
            for (int n = minN; n <= maxN; n++)
            {
                float sum = 0;
                for (int k = minX; k <= maxX; k++)
                {
                    if (n - k < minH || n - k > maxH) continue;
                    sum += X.Samples[k] * H.Samples[n - k];
                }
                value.Add(sum);
            }
            if (value[value.Count - 1] == 0)
            {
                value.RemoveAt(value.Count - 1);
            }
            OutputConvolvedSignal = new Signal(value, false);
            for (int i = 0; i < OutputConvolvedSignal.SamplesIndices.Count; i++)
            {
                OutputConvolvedSignal.SamplesIndices[i] += SHIFT;
            }
        }
    }
}
