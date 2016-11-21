using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SynchronousMultipleCapture
{
    static public class GlobalConst
    {
        /// <summary> ��������� ������ </summary>
        static public int M = 2;
        /// <summary> ��������� ���������� ���������� U </summary>
        static public int U = 0;
        /// <summary> ��� ������� A(t) </summary>
        static public int A = 0;
        /// <summary> ���� ���������� </summary>
        static public Brush ColorPointEvader = Brushes.Red;
        /// <summary> ���� ������� �������� ���������� </summary>
        static public Pen ColorVelocityEvader = new Pen(Color.Red, 3);

        /// <summary> ���� ��������������� </summary>
        static public Brush ColorPointPersecutors = Brushes.DarkBlue;
        /// <summary> ���� �������� ��������� ��������������� </summary>
        static public Pen ColorVelocityPersecutors = new Pen(Color.DarkBlue, 3);

        /// <summary> ��������� (������ � �.�.) </summary>
        static public void Init()
        {
            ColorVelocityEvader.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            ColorVelocityPersecutors.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }
    }
}
