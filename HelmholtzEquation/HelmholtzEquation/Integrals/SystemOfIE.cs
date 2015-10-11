using System;
using System.Collections.Generic;
using HelmholtzEquation.LinearSystem;

namespace HelmholtzEquation.Integrals
{
    class SystemOfIE
    {
        private int n;
        public int N { get; private set; }
        private double a, b, H; // [0,2PI] - область визначення функції , H крок
        private double[,] A; // матриця 
        private double[] g; // права частина
        private SmoothCore H11, H12, H2;
        private Func<double,double> g1,g2;
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
            // TODO 
            // формуємо матрицю і розвязуємо за допомогою класу SLAE
            return new double[2 * N];
        }
    }
}
