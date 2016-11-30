using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation.ChainLinks
{
    class ChainLink3 : IChainLink
    {
        private string stepName;
        //private static int index = 0;

        public Dictionary<string, object> Storage { get; set; }

        public ChainLink3(string _stepName)
        {
            stepName = _stepName;
        }

        public void Execute(Dictionary<string, object> storage)
        {
            //storage.Add("key" + index.ToString(), "aloha");
            //object value;
            //storage.TryGetValue("key1", out value);
            //Console.WriteLine((string)value);
            //index++;

            double outsidePointR = (double)storage["outsidePointR"];
            double outsidePointT = (double)storage["outsidePointT"];
            int N                = (int)storage["N"];
            Problem prblm        = (Problem)storage["prblm"];
            double[] sltn        = (double[])storage["sltn"];

            Func<double, double> imAccurateSolution = (t) => {
                return prblm.H2(t, outsidePointR, outsidePointT);
            };

            Func<double, double> realAccurateSolution = (t) => {
                return prblm.H1(t, outsidePointR, outsidePointT);
            };

            int n = 2 * N;

            Console.WriteLine("\n  REAL part of solution on some curve: \n Accurate       \t Received ");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0} \t {1}", realAccurateSolution(i * Math.PI / N), sltn[i]);
            }

            Console.WriteLine("\n  IMAGINE part of solution on some curve: \n Accurate        \t  Received ");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0} \t {1} ", imAccurateSolution(i * Math.PI / N), sltn[i + n]);
            }

            Console.WriteLine();
            double maxDeviation = Math.Abs(sltn[0] - realAccurateSolution(0));
            for (int i = 1; i < n; i++)
            {
                maxDeviation = Math.Max(maxDeviation, Math.Abs(sltn[i] - realAccurateSolution(i * Math.PI / N)));
            }

            Console.WriteLine("\n Max deviation of real part of solution = {0}", maxDeviation);
            maxDeviation = Math.Abs(sltn[n] - imAccurateSolution(0));
            for (int i = n; i < 2 * n; i++)
            {
                maxDeviation = Math.Max(maxDeviation, Math.Abs(sltn[i] - imAccurateSolution((i - n) * Math.PI / N)));
            }

            Console.WriteLine("\n Max deviation of imagine part of solution = {0}", maxDeviation);

            // TODO !!!
            Console.ReadKey();
        }
    }
}
