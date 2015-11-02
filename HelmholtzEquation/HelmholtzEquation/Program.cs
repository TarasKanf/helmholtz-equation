using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelmholtzEquation
{
    class Program
    {
        // для зміни умові задачі потрібно змінити imK,realK,EdgeRadius,ImBoundaryCondition,RealBoundaryCondition
        // Для отримання розвязку на іншій кривій потрібно змінити CurveRadiusToFindSolution
        static double imK= 1,realK =1; 
        static Problem prblm;
        static void Main(string[] args)
        {
            Console.WriteLine("Dirichlet problem for Helmholtz equation \n Enter N (N*2 = number of points):");
            int N = int.Parse(Console.ReadLine());
            prblm = new Problem(EdgeRadius,realK,ImBoundaryCondition,RealBoundaryCondition); // 
            double[] sltn = prblm.Solve(N,CurveRadiusToFindSolution);
            int n = 2 * N;
            Console.WriteLine("\n  Accurate REAL part of solution on some curve:");
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0}  ", RealAccurateSolution(i * Math.PI / N));
            }
            Console.WriteLine("\n Received REAL part of solution on some curve:");
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0}  ", sltn[i]);
            }            
            Console.WriteLine("\n  Accurate IMAGINE part of solution on some curve:");
            for (int i = 0; i < n; i++)
            {
                Console.Write("{0}  ", ImAccurateSolution(i * Math.PI / N));
            }
            Console.WriteLine("\n Received  IMAGINE part of solution on some curve:");
            for (int i = n; i < 2*n; i++)
            {
                Console.Write("{0}  ", sltn[i]);
            }
            Console.WriteLine();
            double maxDeviation = Math.Abs(sltn[0] - RealAccurateSolution(0));
            for (int i = 1; i < n; i++)
            {
                maxDeviation = Math.Max(maxDeviation, Math.Abs(sltn[i] - RealAccurateSolution(i * Math.PI / N)));             
            }
            Console.WriteLine("\n Max deviation of real part of solution = {0}", maxDeviation);
            maxDeviation = Math.Abs(sltn[n] - ImAccurateSolution(0));
            for (int i = n; i < 2*n; i++)
            {
                maxDeviation = Math.Max(maxDeviation, Math.Abs(sltn[i] - RealAccurateSolution((i-n) * Math.PI / N)));
            }
            Console.WriteLine("\n Max deviation of imagine part of solution = {0}", maxDeviation);
            Console.ReadKey();
        }
        // 
        // 
        // функції які потрібно передати в Problem
        // поки що тут функції які я собі вигадав і не знаю який точний розвязок для них повинен бути щоб звірити. 
        static double EdgeRadius(double t)
        {
            return 1; // може бути фукція що задає радіус
        }
        static double ImBoundaryCondition(double t)
        {
            return prblm.H2(t,0.5,0);  // точка y (r = 0.5 , t (кут) = 0) належить обмеженій області D в якій ми НЕ шукаємо розвязок
        }
        static double RealBoundaryCondition(double t)
        {
            return prblm.H1(t, 0.5, 0);  // точка y (r = 0.5 , t (кут) = 0) належить обмеженій області D в якій ми НЕ шукаємо розвязок
        }
        static double CurveRadiusToFindSolution(double t)
        {
            return 2;
        }
        static double ImAccurateSolution(double t)
        {
            return prblm.H2(t, 0.5, 0);
        }
        static double RealAccurateSolution(double t)
        {
            return prblm.H1(t, 0.5, 0);
        }
    }
}
