using System;

namespace HelmholtzEquation.Polynomials
{
    class TrigonPolynomial
    {
         public int N { get { return n; } }
        private int n;
        public double[] A 
        {
            get { return a_index; }
        }
        public double[] B
        {
            get { return b_index; }
        }
        private double[] a_index;
        private double[] b_index;
        public TrigonPolynomial(double[] y, int _n) // y - array with length 2xn
        {            
            n = _n;
            a_index = new double[n + 1];
            b_index = new double[n - 1];
            for (int k = 0; k <= n; k++)
            {
                a_index[k] = 0;
                for (int j = 0; j < 2 * n; j++)
                {
                    a_index[k] += y[j] * Math.Cos((Math.PI * j * k) / n);
                }
                a_index[k] /= (double)n;
            }
            for (int k = 0; k < n - 1; k++)
            {
                b_index[k] = 0;
                for (int j = 0; j < 2 * n; j++)
                {
                    b_index[k] += y[j] * Math.Sin((Math.PI * j * (k + 1)) / (double)n);
                }
                b_index[k] /= (double)n;
            } 
        }
        public double Value(double x)
        {
            double suma = 0;
            suma += a_index[0] / 2.0 + (a_index[n]*Math.Cos(((double)n)*x))/2.0;
            for(int k = 1;k<n;k++)
            {
                suma += a_index[k] * Math.Cos(k * x) + b_index[k - 1] * Math.Sin(k * x);
            }
            return suma;
        }
        public bool Add(TrigonPolynomial tp)
        {
            if (tp.N != n)
            {
                return false;
            }
            double[] r = new double[2 * n];
            double h = Math.PI / n;
            double temp = 0;
            for (int i = 0; i < 2 * n; i++)
            {
                r[i] = this.Value(temp) + tp.Value(temp);
                temp += h;
            }
            TrigonPolynomial newpol = new TrigonPolynomial(r, n);
            for (int k = 0; k <= n; k++)
            {
                a_index[k] = newpol.A[k];
            }
            for (int k = 0; k < n - 1; k++)
            {
               b_index[k] = newpol.B[k];
            } 
            return true;
        }
        public bool Add(double[] c)
        {
            if (c.Length != 2*n)
            {
                return false;
            }
            double[] r = new double[2 * n];
            double h = Math.PI / n;
            double temp = 0;
            for (int i = 0; i < 2 * n; i++)
            {
                r[i] = this.Value(temp) + c[i];
                temp += h;
            }
            TrigonPolynomial newpol = new TrigonPolynomial(r, n);
            for (int k = 0; k <= n; k++)
            {
                a_index[k] = newpol.A[k];
            }
            for (int k = 0; k < n - 1; k++)
            {
                b_index[k] = newpol.B[k];
            }
            return true;
        }
        public double Derivative(double t)
        {
            double dev = 0;
            for (int i = 1; i < n; i++)
            {
                dev += -i * a_index[i] * Math.Sin(i * t) + i * b_index[i-1] * Math.Cos(i * t); 
            }
            dev -= a_index[n] * n * Math.Sin(n*t)/2.0;
            return dev;
        }
    }
}
