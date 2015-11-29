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
        private Func<double,double> gReal,gIm; // реальна і уявна частина значень на границі Г
        public SystemOfIE(SmoothCore _H11, SmoothCore _H12,SmoothCore _H2,Func<double,double> _gReal,Func<double,double> _gIm)
        {
            H11 = _H11;
            H12 = _H12;
            H2 = _H2;
            gReal = _gReal;
            gIm = _gIm;
        }
        public double[] SolveWithSimpleMetodForPFwithWeakAndSmoothCore(int _n) 
        {
            N = _n;
            //формуємо вектор що задає праву чатину рівняння праву чатину рівняння
            g = new double[4 * N]; // 2*N значення з функцією gReal і  2*N значення з функцією gIm
            double temp =0;
            h = (b - a)/(2.0 * N);
            for (int i = 0; i < 2 * N; i++)
            {
                temp = i*h;
                g[i] = gReal(temp);
                g[i + 2 * N] = gIm(temp);                
            }
            // формуємо матрицю A, тобто дискретний вигляд системи інтегральних рівнянь
            A = new double[4 * N, 4 * N];
            double ti = 0, tauj = 0;
            // визначаємо кокфіцієнти що відповідатиметь на miu1(tauj)
            for (int i = 0; i < 2 * N; i++)
            {
                ti = i * h;
                H11.Prepare(ti);
                H12.Prepare(ti);
                H2.Prepare(ti);               
                // коефіцієнти будуються за тригонометричними квадратурними формулами
                for (int j = 0; j < 2 * N; j++)
                {
                    tauj = j * h;
                    // перше рівняння
                    A[i, j] = A[i + 2 * N, j + 2 * N] =
                        Math.PI * H12.GetValue(tauj) / N +
                        H11.GetValue(tauj) * 2.0 * Math.PI * Integral.CoefficientForWeakSingular(H11.Param, N, tauj);
                    A[i, j + 2 * N] = -Math.PI * H2.GetValue(tauj) / N;
                    // друге рівняння
                    A[i + 2 * N, j] = Math.PI * H2.GetValue(tauj) / N;
                }               
            }           
            return SLAE.LU_methodSolving(A,g,4*N);
        }
    }
}
