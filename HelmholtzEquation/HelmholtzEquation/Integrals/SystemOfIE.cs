using System;
using System.Collections.Generic;
using HelmholtzEquation.LinearSystem;

namespace HelmholtzEquation.Integrals
{
    class SystemOfIE
    {       
        public int N { get; private set; }
        private double a =0 , b= 2.0* Math.PI, h; // [0,2PI] - область визначення функції , H крок
        private double[,] A; // матриця 
        private double[] g; // права частина
        private SmoothCore H11, H12, H2;
        private Func<double,double> g1,g2; // реальна і уявна частина значень на границі Г
        public SystemOfIE(SmoothCore _H11, SmoothCore _H12,SmoothCore _H2,Func<double,double> _g1,Func<double,double> _g2)
        {
            H11 = _H11;
            H12 = _H12;
            H2 = _H2;
            g1 = _g1;
            g2 = _g2;
        }
        public double[] SolveWithSimpleMetodForPFwithWeakAndSmoothCore(int _n) 
        {
            N = _n;
            //формуємо вектор що задає праву чатину рівняння праву чатину рівння
            g = new double[4 * N]; // 2*N значення з ффункцією g1 і  2*N значення з ффункцією g2
            double temp =0;
            h = (b - a) / (2*N); 
            for (int i = 0; i < 2 * N; i++)
            {
                g[i] = g1(temp);
                g[i + 2 * N] = g2(temp);
                temp += h;
            }
            // формуємо матрицю A, тобто дискретний вигляд системи інтегральних рівнянь
            A = new double[4 * N, 4 * N];
            double ti = 0, tauj = 0;
            // визначаємо кокфіцієнти що відповідатиметь на miu1(tauj)
            for (int i = 0; i < 2 * N; i++)
            {
                H12.Prepare(ti);
                H11.Prepare(ti);
                tauj = 0;
                // коефіцієнти будуються за тригонометричними квадратурними формулами
                for (int j = 0; j < 2 * N; j++)
                {
                    A[i, j] = Math.PI * H12.GetValue(tauj) / N;
                    // перевірити чи треба передавати саме j
                    A[i, j] = A[i, j] + H11.GetValue(tauj) * 2.0 * Math.PI * Integral.CoefficientForWeakSingular(H11.Param, j, N, tauj);
                    tauj += h;
                }
                ti += h;
            }
            ti = 0;
            for (int i = 2 * N; i < 4 * N; i++)
            {
                H2.Prepare(ti);
                tauj = 0;
                for (int j = 0; j < 2 * N; j++)
                {
                    A[i, j] = Math.PI * H2.GetValue(tauj) / N;
                    tauj += h;
                }
                ti += h;
            }
            // визначаємо кокфіцієнти що відповідатиметь на miu2(tauj)
            ti = 0;
            for (int i = 0; i < 2 * N; i++)
            {
                H2.Prepare(ti);
                tauj = 0;
                for (int j = 2*N; j < 4 * N; j++)
                {
                    // тут мінус бо в першому рівнянні перед другим інтегралом стоїть "-" 
                    A[i, j] = -Math.PI * H2.GetValue(tauj) / N;
                    tauj += h;
                }
                ti += h;
            }
            ti = 0;
            for (int i = 2*N; i < 4 * N; i++)
            {
                H12.Prepare(ti);
                H11.Prepare(ti);
                tauj = 0;
                // коефіцієнти будуються за тригонометричними квадратурними формулами
                for (int j = 2*N; j < 4 * N; j++)
                {
                    A[i, j] = Math.PI * H12.GetValue(tauj) / N;
                    // перевірити чи треба передавати саме j
                    A[i, j] = A[i, j] + H11.GetValue(tauj) * 2.0 * Math.PI * Integral.CoefficientForWeakSingular(H11.Param, j-2*N, N, tauj);
                    tauj += h;
                }
                ti += h;
            }
            return SLAE.SolveWithLU(A,g,4*N);
        }
    }
}
