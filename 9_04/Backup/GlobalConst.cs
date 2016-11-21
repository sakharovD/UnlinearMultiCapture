using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SynchronousMultipleCapture
{
    static public class GlobalConst
    {
        /// <summary> Кратность поимки </summary>
        static public int M = 2;
        /// <summary> Множество допустимыъ управлений U </summary>
        static public int U = 0;
        /// <summary> Вид системы A(t) </summary>
        static public int A = 0;
        /// <summary> Цвет убегающего </summary>
        static public Brush ColorPointEvader = Brushes.Red;
        /// <summary> Цвет вектора скорости убегающего </summary>
        static public Pen ColorVelocityEvader = new Pen(Color.Red, 3);

        /// <summary> Цвет преследователей </summary>
        static public Brush ColorPointPersecutors = Brushes.DarkBlue;
        /// <summary> Цвет векторов скоростей преследователей </summary>
        static public Pen ColorVelocityPersecutors = new Pen(Color.DarkBlue, 3);

        /// <summary> Настройки (стилей и т.д.) </summary>
        static public void Init()
        {
            ColorVelocityEvader.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            ColorVelocityPersecutors.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }
    }
}
