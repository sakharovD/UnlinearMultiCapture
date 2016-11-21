using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SynchronousMultipleCapture.GameLogic.GameElements
{
    public class Evader
    {
        public Vector coordinates = new Vector();
        public Vector v = new Vector();
        public Evader(double X, double Y)
        {
            coordinates.X = X;
            coordinates.Y = Y;
        }
    }
}
