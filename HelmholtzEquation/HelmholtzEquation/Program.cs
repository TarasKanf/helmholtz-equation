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
        static void Main(string[] args)
        {
            Console.WriteLine("Dirichlet problem for Helmholtz equation \n Enter N (N*2 = number of points):");
            int N = int.Parse(Console.ReadLine());
            Problem pr = new Problem(EdgeRadius,imK,realK,ImBoundaryCondition,RealBoundaryCondition); // 
            pr.Solve(N,CurveRadiusToFindSolution);
            int n = 2 * N;
            Console.WriteLine("\n  Accurate solution on some curve:");
            for (int i = 0; i < n; i++)
            {
                //Console.Write("{0}  ", AccurateSolution(i * Math.PI / N));
            }
            Console.WriteLine("\n Received solution on some curve:");
            for (int i = 0; i < n; i++)
            {
                //Console.Write("{0}  ", pr.ui[i]);
            }
            double maxDeviation = 0;//Math.Abs(pr.ui[0] - AccurateSolution(0));
            for (int i = 1; i < 2 * N; i++)
            {
               // maxDeviation = Math.Max(maxDeviation, Math.Abs(pr.ui[i] - u_ForFoundedSolution(i * Math.PI / N)));             
            }
            Console.WriteLine("\n Max deviation of solution = {0}", maxDeviation);
            Console.ReadKey();
        }
        // 
        // TODO
        // функції які потрібно передати в Problem
        static double EdgeRadius(double t)
        {
            return 1; // може бути фукція що задає радіус
        }
        static double ImBoundaryCondition(double t)
        {
            return 0;
        }
        static double RealBoundaryCondition(double t)
        {
            return 0;
        }
        static double CurveRadiusToFindSolution(double t)
        {
            return 2;
        }
    }
}
