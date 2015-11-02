using System;

namespace HelmholtzEquation.Integrals
{
    static class Integral
    {       
        public static double CalculateWithTrapeziumMethod(Func<double,double> f, double a, double b, int n)// for periodiс function on [0, 2*PI]
        {
            int N = 2 * n;
            double temp = 0;
            double h = Math.PI / n;
            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += f(temp);
                temp += h;
            }
            return sum * Math.PI / n;
        }
        public static double CalculateWithTrapeziumMethod(ICore f, int n) // for periodiс function f on [0, 2*PI]
        {
            int N = 2 * n;
            double temp = 0;
            double h = Math.PI / n;
            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += f.GetValue(temp);
                temp += h;
            }
            return sum * Math.PI / n;
        }        

        public static double CalculateWithWeakSingularCore(SmoothCore f, int n)
        {
            int N = 2 * n;
            double temp = 0;
            double h = Math.PI / n;
            double sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += f.GetValue(temp) * CoefficientForWeakSingular(f.Param, i, n, temp);
                temp += h;
            }
            return sum * 2.0 * Math.PI;
        }// for periodiс cores with smooth part f on [0, 2*PI]x[0, 2*PI]
        public static double CoefficientForWeakSingular(double s, int k, int n, double sk)
        {
            double sum = 0;
            for (int i = 1; i < n; i++)
            {
                sum += Math.Cos(i * (s - sk)) / i;
            }
            sum *= 2.0;
            sum += 1;
            sum += Math.Cos((s - sk)) / n;
            sum *= -1.0 / (2.0 * n);
            return sum;
        }        
    }
}
