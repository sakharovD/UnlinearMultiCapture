using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchronousMultipleCapture.Functions
{
    class Function1 : IFunction
    {
        public double GetValueAt(double t)
        {
            return Math.Exp(Math.Cos(t - 1));
        }
    }
}
