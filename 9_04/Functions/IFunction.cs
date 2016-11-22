using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchronousMultipleCapture.Functions
{
    public interface IFunction
    {
        double GetValueAt(double t);
    }
}
