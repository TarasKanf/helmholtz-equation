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
        // r задаватиме радіус кривої на якій знахоться точка x .
        //При розвязуванні системи r = edgeR, 
        //а при пошуку розвязку ( розвязок - функція u(x)) на деякій кривій чи в деякій точці r задаватиме радіус тої кривої чи точки
        private TrigonPolynomial r;
        private Func<double, double> imBoundaryCondition, realBoundaryCondition;
        private double realK;
        // константа до якої обчислюватимуться нескіченні суми. Відповідає за точність обчислення функцій J0(), Y0()
        private const int accuracyN = 30;
        private const double gamma = 0.57721566490153;
        // і інші поля якщо потрібно
        public Problem(Func<double,double> _edgeRadius,double _realK,Func<double,double> _imBoundaryCondition,Func<double,double> _realBoundaryCondition)
        {
            edgeRadius = _edgeRadius;
            imBoundaryCondition = _imBoundaryCondition;
            realBoundaryCondition = _realBoundaryCondition;            
            realK = _realK;
        }
        public double[] Solve(int _n, Func<double,double> radiusToFindSolutionOn)
        {
            int n = _n; // кількість точок рівна 2*n
            double[] solution;// розвязки на замкнутій зірковій кривій з кроком 2*PI/(2*n)
            // Шукаємо розвязок створивши еземпляр класу SystemOfIE (system of integral equations) передавши в нього відповідіні ядра(SmoothCore) 
             // щось в цьому стилі 
            double h = Math.PI / n;                       
            double[] radius = new double[2 * n];
            double temp = 0; 
            for (int i = 0; i < 2 * n; i++)
            {
                temp = i * h; // щоб похибка не накопичувалась
                radius[i] = edgeRadius(temp);                
            }
            edgeR = new TrigonPolynomial(radius, n);
            r = edgeR; // означає що x знаходиться на межі
            SmoothCore coreH11 = new SmoothCore(H1_1);
            SmoothCore coreH12 = new SmoothCore(H1_2);
            SmoothCore coreH2 = new SmoothCore(H2);
            SystemOfIE equation = new SystemOfIE(coreH11,coreH12,coreH2,imBoundaryCondition, realBoundaryCondition); 
            // перша половина fi -  реальна частина, друга уявна
            double[] fi = equation.SolveWithSimpleMetodForPFwithWeakAndSmoothCore(n); // розв`язки інтегрального рівняння в точках t[j] = j*PI/N ,  j = 0, 2*N -1  
            // міняємо розташування точок x. Тобто шукатимемо розвязок на кривій з радіальною функцією radiusToFindSolutionOn
            temp = 0;
            for (int i = 0; i < 2 * n; i++)
            {
                temp = i * h;
                radius[i] = radiusToFindSolutionOn(temp);                
            }
            r = new TrigonPolynomial(radius, n); // означає що x знаходиться на кривій з рад. ф-єю  radiusToFindSolutionOn
            // шукаємо розвязок на заданій кривій
            solution = FindSolution(fi,n); // перша половина solution це реальна частина розвязку, а друга уявна
            return solution;
        }
        private double[] FindSolution(double[] y, int n)
        {
            double h = Math.PI / n;
            double[] solution = new double[4 * n];
            double t =0, tau =0; // t - відповідає за точку x в якій знаходимо розвязки, по змінній tau - інтегруємо
            double sum = 0;
            //обчислюємо реальну частину
            for (int i = 0; i < 2*n; i++)
            {
                sum = 0;
                tau = 0;
                t = i * h;
                for (int j = 0; j < 2 * n; j++)
                {
                    tau = j*h;
                    sum += y[j] * H1(t, tau);                    
                }
                tau = 0;
                for (int j = 2*n; j < 4*n; j++)
                {
                    tau = j * h;
                    sum -= y[j] * H2(t, tau);                    
                }
                solution[i] = sum*Math.PI / n;                   
            }
            // обчислюємо уявну частину
            t = 0;
            for (int i = 2*n; i < 4*n; i++)
            {
                sum = 0;
                tau = 0;
                for (int j = 0; j < 2 * n; j++)
                {
                    sum += y[j] * H2(t, tau);
                    tau += h;
                }
                tau = 0;
                for (int j = 2 * n; j < 4 * n; j++)
                {
                    sum += y[j] * H1(t, tau);
                    tau += h;
                }
                solution[i] = sum * Math.PI / n;
                t += h;
            }
            return solution;
        }
        private double H1_1(double t, double tau)
        {
            double rx = r.Value(t);
            double ry = edgeR.Value(tau);
            return -J0(Zfunc(rx,t,ry,tau)) / (4.0 * Math.PI);
        }
        private double H1_2(double t, double tau)
        {            
            double rx = r.Value(t);
            double ry = edgeR.Value(tau);;
            double z = Zfunc(rx,t,ry,tau);
            double result = 0;
            if (Math.Abs(t - tau)>1e-7)
            {
                result = (S(z) + J0(z) * Math.Log(4.0 * Math.Pow(Math.Sin((t - tau) / 2.0), 2)
                / (Math.E * realK * realK * (rx * rx + ry * ry - 2.0 * rx * ry * Math.Cos(t - tau)))) / 2.0)
                / (Math.PI * 2.0);
            }
            else
            {
                z = ((rx - ry)<1e-10)?0:z;
                result = (S(z) + J0(z) *
                    Math.Log(1.0 / (Math.E * realK * realK * Math.Pow(edgeR.Derivative(t), 2))) / 2.0)
                    /(Math.PI*2.0);
            }
            return result;
        }
        // використовуватиметься також щоб побудувати розвязок 
        private double H2(double t, double tau)
        {
            return J0(Zfunc(r.Value(t), t, edgeR.Value(tau),tau))/4.0;
        }
        private double J0(double z)
        {
            double result = 0;
            int i = 1; // i = {-1,1}
            double fаctorial = 1;
            // першу ітерацію роблю окремо щоб коректно надалі рахувався факторіал  // (0)! = 1
            result += i;
            i *= -1;
            z = z * z / 4.0;
            // 
            for (int k = 1; (k < accuracyN) && (k < 40); k++)
            {
                fаctorial *= k;                
                result += i * Math.Pow(z,k) / Math.Pow(fаctorial, 2);
                i *= -1;
            }            
            return result;
        }
        private double Zfunc(double rx, double tx, double ry,double ty)
        {
            return realK * Math.Sqrt(rx*rx + ry*ry - 2.0*rx*ry*Math.Cos(tx-ty));
        }
        private double S(double z)
        {
            return J0(z)*(Math.Log(2) - gamma) - Math.PI*L(z)/2.0;
        }
        private double L(double z)
        {
            double result = 0;
            double sum = 0;
            int i = 1;
            double factorial = 1 ;
            z = z * z / 4.0;
            for (int k = 1; (k < accuracyN)&&(k < 40); k++)
            {
                sum += 1.0 / k;
                factorial *= k;
                result += i * sum * Math.Pow(z, k) / Math.Pow(factorial, 2);
                i *= -1;                
            }
            return result*2.0/Math.PI; // перевірити чи саме цей множник має бути
        }
        // використовуватиметься тільки щоб побудувати розвязок 
        private double H1(double t, double tau)
        {            
            double rx = r.Value(t);
            double ry = edgeR.Value(tau);
            double z = Zfunc(rx, t, ry, tau);
            return -(2*(Math.Log(z/2.0) + gamma)*J0(z)/Math.PI + L(z))/4.0;
        }
        // цей варіант функції H1 (реальна частина фундаметального розвязку) 
        //використовуватиметься для перевірики отриманого розвязку в основній програмі, тобто задає уявне значення на межі
        public double H1(double t, double ry, double tau)
        {
            double rx = edgeR.Value(t);            
            double z = Zfunc(rx, t, ry, tau);
            return -(2 * (Math.Log(z / 2.0) + gamma) * J0(z) / Math.PI + L(z)) / 4.0;
        }
        // цей варіант функції H2 (уявна частина фундаметального розвязку) 
        //використовуватиметься для перевірики отриманого розвязку в основній програмі, тобто задає уявне значення на межі        
        public double H2(double t,double ry, double tau)
        {
            return J0(Zfunc(edgeR.Value(t), t, ry, tau)) / 4.0;
        }
    }
}
