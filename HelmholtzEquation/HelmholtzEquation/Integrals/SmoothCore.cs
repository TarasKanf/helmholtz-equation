using System;

namespace HelmholtzEquation.Integrals
{
    class SmoothCore : ICore
    {
         public double Param { get; private set; }
        private  Func<double,double,double> F;  
        public SmoothCore(Func<double, double, double> f)
        {
            F = f;
        }
        public void Prepare(double par)
        {
            Param = par;
        }
        override public double GetValue(double t)
        {
            return F(Param,t);
        }
    }
}
