using System;
using System.Collections.Generic;
using HelmholtzEquation.Integrals;
using HelmholtzEquation.Helpers;

namespace HelmholtzEquation
{
    class Problem
    {
        private Func<double, double> edgeRadius;
        private TrigonPolynomial edgeR;
        private Func<double, double> imBoundaryCondition, realBoundaryCondition;
        private double realK;
        // константа до якої обчислюватимуться нескіченні суми. Відповідає за точність обчислення функцій J0(), Y0()
        private const int accuracyN = 20;
        // і інші поля якщо потрібно
        public Problem(Func<double,double> _edgeRadius,double _realK,Func<double,double> _imBoundaryCondition,Func<double,double> _realBoundaryCondition)
        {
            edgeRadius = _edgeRadius;
            imBoundaryCondition = _imBoundaryCondition;
            realBoundaryCondition = _realBoundaryCondition;            
            realK = _realK;
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
            SmoothCore coreH11 = new SmoothCore(H1_1);
            SmoothCore coreH12 = new SmoothCore(H1_2);
            SmoothCore coreH2 = new SmoothCore(H2);
            SystemOfIE equation = new SystemOfIE(coreH11,coreH12,coreH2,imBoundaryCondition, realBoundaryCondition);            
            double[] fi = equation.SolveWithSimpleMetodForPFwithWeakAndSmoothCore(n); // розв`язки інтегрального рівняння в точках t[j] = j*PI/N ,  j = 0, 2*N -1   
            solution = FindSolution(fi,n,radiusToFindSolutionOn); // перша половина solution це реальна частина розвязку, а друга уявна
            //
            return solution;
        }
        // TODO
        // Тут повинна бути куча потрібних функцій H які передаватимемо в  SystemOfIE через делегати

        private double[] FindSolution(double[] y,int n,Func<double,double> rTFSO)
        {
            double h = Math.PI/n;
            double[] solution = new double[4 * n];
            // TODO
            return solution;
        }
        private double H1_1(double t, double tau)
        {
            // -J0()/(4.0*Math.PI)
            return 0;
        }
        private double H1_2(double t, double tau)
        {
            // TODD
            return 0;
        }
        private double H2(double t, double tau)
        {
            // TODD
            return 0;
        }
        private double J0(double rx,double tx, double ry, double ty)
        {
            double result = 0;
            int i = 1; // i = {-1,1}
            int fuctorial = 1;
            // першу ітерацію роблю окремо щоб коректно надалі рахувався факторіал  // (0)! = 1
            result += i;
            i *= -1;
            // 
            for (int k = 1; k < accuracyN; k++)
            {
                fuctorial = fuctorial * k;
                result += i * Math.Pow(Zfunc(rx,tx, ry,ty) / 2.0, 2 * k) / Math.Pow(fuctorial, 2);
                i *= -1;
            }            
            return result;
        }
        private double Zfunc(double rx, double tx, double ry,double ty)
        {
            return realK * Math.Sqrt(rx*rx + ry*ry - 2.0*rx*ry*Math.Cos(tx-ty));
        }
    }
}
