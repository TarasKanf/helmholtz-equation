using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation.ChainLinks
{
    class ChainLink2 : IChainLink
    {
        private string stepName;
        //private static int index = 0;

        public Dictionary<string, object> Storage { get; set; }

        public ChainLink2(string _stepName)
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

            Func<double, double> curveRadiusToFindSolution = (t) => {
                return 4;
            };

            int N         = (int)storage["N"];
            Problem prblm = (Problem)storage["prblm"];

            double[] sltn = prblm.Solve(N, curveRadiusToFindSolution);

            storage["sltn"] = sltn;
        }
    }
}
