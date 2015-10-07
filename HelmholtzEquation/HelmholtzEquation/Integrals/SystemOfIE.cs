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
        public SystemOfIE()
        {

        }
        public double[] SolveWithSimpleMetodForPFwithWeakAndSmoothCore(SmoothCore C1, SmoothCore C2, int n1) // параметри ІРинко я ще не виправляв , повинна бути інша кількість
        {
            // TODO 
            // формуємо матрицю і розвязуємо за допомогою класу SLAE
        }
    }
}
