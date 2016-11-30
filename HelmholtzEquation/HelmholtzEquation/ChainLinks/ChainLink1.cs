using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation.ChainLinks
{
    class ChainLink1 : IChainLink
    {
        private string stepName;
        //private static int index = 0;

        public Dictionary<string, object> Storage { get; set; }

        public ChainLink1(string _stepName)
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

            // для зміни умові задачі потрібно змінити imK,realK,EdgeRadius,ImBoundaryCondition,RealBoundaryCondition
            // Для отримання розвязку на іншій кривій потрібно змінити CurveRadiusToFindSolution
            double realK = 0.1;
            // double outsidePointR = 1, outsidePointT = 3*Math.PI/2.0; 
            double outsidePointR = 0.5;
            double outsidePointT = 0;

            Problem prblm = new Problem(); // required to call H2 inside ImBoundaryCondition...

            // функції які потрібно передати в Problem
            Func<double, double> edgeRadius = (t) => {
                return 1; // може бути фукція що задає радіус
                          //return Math.Sqrt(1 - 2*Math.Pow(Math.Sin(t),3) + Math.Pow(Math.Sin(t),4));
            };

            Func<double, double> imBoundaryCondition = (t) => {
                //return 0;
                return prblm.H2(t, outsidePointR, outsidePointT);  // точка y (r = 0.5 , t (кут) = 0) належить обмеженій області D в якій ми НЕ шукаємо розвязок
            };

            Func<double, double> realBoundaryCondition = (t) => {
                //return 5;
                return prblm.H1(t, outsidePointR, outsidePointT);  // точка y (r = 0.5 , t (кут) = 0) належить обмеженій області D в якій ми НЕ шукаємо розвязок
            };

            Console.WriteLine("Dirichlet problem for Helmholtz equation \n Enter N (N*2 = number of points):");
            int N = int.Parse(Console.ReadLine());
            prblm = new Problem(edgeRadius, realK, realBoundaryCondition, imBoundaryCondition); // 

            // If the specified key is not found, a get operation throws a 
            // KeyNotFoundException, and a set operation creates a new 
            // element with the specified key.
            // https://msdn.microsoft.com/en-us/library/9tee9ht2(v=vs.110).aspx
            storage["N"] = N;
            storage["prblm"] = prblm;
            storage["outsidePointR"] = outsidePointR;
            storage["outsidePointT"] = outsidePointT;
        }
    }
}