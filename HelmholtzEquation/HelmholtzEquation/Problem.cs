using System;
using System.Collections.Generic;
using HelmholtzEquation.Integrals;
using HelmholtzEquation.Polynomials;

namespace HelmholtzEquation
{
    class Problem
    {
        private Func<double, double> edgeRadius;
        private TrigonPolynomial edgeR;
        // і інші поля якщо потрібно
        public Problem(Func<double,double> _edgeRadius,double imK,double realK,Func<double,double> imBoundaryCondition,Func<double,double> realBoundaryCondition)
        {
            edgeRadius = _edgeRadius;
        }
        public double[] Solve(int n, Func<double,double> radiusToFindSolutionOn)
        {
            double[] solution = new double[n*2]; // крозвязки на замкнутій зірковій кривій з кроком 2*PI/(2*n)
            // Шукаємо розвязок створивши еземпляр класу SystemOfIE (system of integral equations) передавши в нього відповідіні ядра(SmoothCore) 
             // щось в цьому стилі 
            double h = Math.PI / n;                       
            double[] radius = new double[2 * n];
            double temp = 0; 
            for (int i = 0; i < 2 * n; i++)
            {
                radius[i] = edgeRadius(temp);
                temp += h;
            }
            edgeR = new TrigonPolynomial(radius, n);
            SystemOfIE equation = new SystemOfIE(imBoundaryCondition, realBoundaryCondition);
            SmoothCore s1 = new SmoothCore(H);
            SmoothCore s2 = new SmoothCore(H);
            double[] fi = equation.SolveWithSimpleMetodForPFwithWeakAndSmoothCore(s1, s2, n); // розв`язки інтегрального рівняння в точках t[j] = j*PI/N ,  j = 0, 2*N -1   
            solution = FindSolution(fi);
            //
            return solution;
        }
        // TODO
        // Тут повинна бути куча потрібних функцій H які передаватимемо в  SystemOfIE через делегати

        private double[] FindSolution(double[] y)
        {

        }
    }
}
