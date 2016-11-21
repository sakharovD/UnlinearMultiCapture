using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SynchronousMultipleCapture.View
{
    public class GameStyleFabric
    {
        /// <summary> Цвет убегающего </summary>
        public Brush ColorPointEvader = Brushes.Red;
        /// <summary> Цвет вектора скорости убегающего </summary>
        public Pen ColorVelocityEvader = new Pen(Color.Red, 3);
        /// <summary> Цвет преследователей </summary>
        public Brush ColorPointPersecutors = Brushes.DarkBlue;
        /// <summary> Цвет векторов скоростей преследователей </summary>
        public Pen ColorVelocityPersecutors = new Pen(Color.DarkBlue, 3);
        public int gameElementsSize = 10;
        public int evaderArrowSize = 40;
        public Pen coorPenPersecutor = new Pen(Color.Black, 1);
        public GameStyleFabric()
        {
            ColorVelocityEvader.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            ColorVelocityPersecutors.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            coorPenPersecutor.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
        }
    }
}
