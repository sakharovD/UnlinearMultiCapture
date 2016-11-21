using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SynchronousMultipleCapture
{
    public partial class MainForm : Form
    {                
        public MainForm()
        {
            InitializeComponent();
            //int amount = 1 + 2 * GlobalConst.M; // кол-во преследователей
            //Loading();
            //GlobalConst.Init();
        }

     private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(350, 350);            
            Pen coorPen = new Pen(Color.Black,1);
            coorPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;           

            //g.FillEllipse(GlobalConst.ColorPointEvader, 
              //(float)(E.coor1 - 5), (float)(E.coor2 - 5), 10, 10);            
            
         
          g.DrawLine(GlobalConst.ColorVelocityEvader, 
                (float)(E.coor1), (float)(E.coor2),
               (float)(E.coor1 + 40 * E.v1), (float)(E.coor2 + 40 * E.v2));

            for (int i = 0; i < P.Count; i++)
            {
                int j = 0;
                if (GlobalConst.M + i < P.Count)
                    j = GlobalConst.M + i;
                else
                    j = GlobalConst.M + i - P.Count;
               // g.DrawLine(coorPen, (float)(P[i].coor1), (float)(P[i].coor2), 
                 //   (float)(P[j].coor1), (float)(P[j].coor2));
            }            
            
            for (int i = 0; i < P.Count; i++ )
            {
                g.FillEllipse(GlobalConst.ColorPointPersecutors, 
                    (float)(P[i].coor1 - 5), (float)(P[i].coor2 - 5), 10, 10);
                g.DrawLine(GlobalConst.ColorVelocityPersecutors,
                (float)(P[i].coor1), (float)(P[i].coor2),
                (float)(P[i].coor1 + 40 * P[i].u1), (float)(P[i].coor2 + 40 * P[i].u2));
            }
        }         

        /// <summary> »нициализаци€ группы преследователей </summary>
     public void Loading()
     {
         E.coor1 = 0; E.coor2 = 0;
         double scale = 200; // масштаб
         int amount = 3 * GlobalConst.M; // кол-во преследователей 

         if (GlobalConst.U == 0)
         {
             //amount = 4 * GlobalConst.M;
             for (int i = 1; i <= GlobalConst.M; i++) // начальные положени€ преследователей
             {
                 P.Add(new Persecutor());
                 P[i].coor1 = scale * i;
                 P[i].coor2 = scale * i;
             }
             for (int i = 1; i <= GlobalConst.M; i++)
             {
                 P[i].Z1 = P[i].coor1 - E.coor1;
                 P[i].Z2 = P[i].coor2 - E.coor2;
                 P[i].capture = false;
             }
             for (int i = GlobalConst.M + 1; i <= 2 * GlobalConst.M; i++) 
             {
                 P.Add(new Persecutor());
                 P[i].coor1 = scale * i;
                 P[i].coor2 = - scale * i;
             }
             for (int i = GlobalConst.M + 1; i <= 2 * GlobalConst.M; i++)
             {
                 P[i].Z1 = P[i].coor1 - E.coor1;
                 P[i].Z2 = P[i].coor2 - E.coor2;
                 P[i].capture = false;
             }
             for (int i = 2 * GlobalConst.M + 1; i <= 3 * GlobalConst.M; i++) 
             {
                 P.Add(new Persecutor());
                 P[i].coor1 = - scale * i;
                 P[i].coor2 = scale * i;
             }
             for (int i = 2 * GlobalConst.M + 1; i <= 3 * GlobalConst.M; i++)
             {
                 P[i].Z1 = P[i].coor1 - E.coor1;
                 P[i].Z2 = P[i].coor2 - E.coor2;
                 P[i].capture = false;
             }
             for (int i = 3 * GlobalConst.M + 1; i <= 4 * GlobalConst.M; i++) 
             {
                 P.Add(new Persecutor());
                 P[i].coor1 = - scale * i;
                 P[i].coor2 = - scale * i;
             }
             for (int i = 3 * GlobalConst.M + 1; i <= 4 * GlobalConst.M; i++)
             {
                 P[i].Z1 = P[i].coor1 - E.coor1;
                 P[i].Z2 = P[i].coor2 - E.coor2;
                 P[i].capture = false;
             }
         }

         if ((GlobalConst.U == 1) || (GlobalConst.U == 2))
         {
             for (int i = 1; i <= GlobalConst.M; i++) // начальные положени€ преследователей
             {
                 P.Add(new Persecutor());
                 P[i].coor1 = 0;
                 P[i].coor2 = scale * i;
             }
             for (int i = 1; i <= GlobalConst.M; i++)
             {
                 P[i].Z1 = P[i].coor1 - E.coor1;
                 P[i].Z2 = P[i].coor2 - E.coor2;
                 P[i].capture = false;
             }
             for (int i = GlobalConst.M + 1; i <= 2 * GlobalConst.M; i++)
             {
                 P.Add(new Persecutor());
                 P[i].coor1 = scale * i;
                 P[i].coor2 = - scale * i;
             }
             for (int i = GlobalConst.M + 1; i <= 2 * GlobalConst.M; i++)
             {
                 P[i].Z1 = P[i].coor1 - E.coor1;
                 P[i].Z2 = P[i].coor2 - E.coor2;
                 P[i].capture = false;
             }
             for (int i = 2 * GlobalConst.M + 1; i <= 3 * GlobalConst.M; i++)
             {
                 P.Add(new Persecutor());
                 P[i].coor1 = - scale * i;
                 P[i].coor2 = - scale * i;
             }
             for (int i = 2 * GlobalConst.M + 1; i <= 3 * P.Count; i++)
             {
                 P[i].Z1 = P[i].coor1 - E.coor1;
                 P[i].Z2 = P[i].coor2 - E.coor2;
                 P[i].capture = false;
             }
         }
     }

        /// <summary> √руппа преследователей </summary>
        public List<Persecutor> P = new List<Persecutor>();

        /// <summary> ”бегающий </summary>
        public Evader E = new Evader();

        /// <summary> Ўаг по времени </summary>
        public double eps = 1;

        /// <summary> –асчет </summary>        
        private void timEps_Tick(object sender, EventArgs e)
        {
            E.coor1 = E.coor1 + E.v1 * eps;
            E.coor2 = E.coor2 + E.v2 * eps;

            int kol_vo = 0;
            for (int i = 0; i < P.Count; i++)
            {
                P[i].coor1 = P[i].coor1 + P[i].u1 * eps;
                P[i].coor2 = P[i].coor2 + P[i].u2 * eps;
                if ((!(P[i].capture)) &&
                    ((Math.Abs(P[i].coor1 - E.coor1) < 2*eps) &&
                    ((Math.Abs(P[i].coor1 - E.coor1) < 2*eps))))                
                {
                    kol_vo = kol_vo + 1;                                                            
                }                
            }
            if (kol_vo >= GlobalConst.M)
            {
                for (int i = 0; i < P.Count; i++)
                    if ((!(P[i].capture)) &&
                        ((Math.Abs(P[i].coor1 - E.coor1) < 2 * eps) &&
                        ((Math.Abs(P[i].coor1 - E.coor1) < 2 * eps))))
                    {
                        P[i].capture = true;
                        //P[i].Calc_l(E.v1, E.v2);
                        //P[i].Calc_u1_u2(E.v1, E.v2);
                        //M = M + 1;
                        //P[i].coor1 = E.coor1 - M; // уравниваем координаты
                        //P[i].coor2 = E.coor2 - M; // чтобы выгл€дело, как точна€ поимка
                    }
            }
            if (kol_vo >= GlobalConst.M)
            {
                timEps.Enabled = false; // произошла M-кратна€ (не одновременна€) поимка
            }
            Invalidate(); // обновление экрана
        }

        /// <summary> Ќачинаем преследование, когда трогаетс€ убегающий (клавиши NumPad1-9 + курсор) </summary>
        bool StartPersecution = false;
        /// <summary> »зменение управлени€ убегающего</summary> 
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Left)    || (e.KeyCode == Keys.Right)   ||
                (e.KeyCode == Keys.Up)      || (e.KeyCode == Keys.Down)    ||
                (e.KeyCode == Keys.NumPad1) || (e.KeyCode == Keys.NumPad2) ||
                (e.KeyCode == Keys.NumPad3) || (e.KeyCode == Keys.NumPad4) ||
                (e.KeyCode == Keys.NumPad5) || (e.KeyCode == Keys.NumPad6) ||
                (e.KeyCode == Keys.NumPad7) || (e.KeyCode == Keys.NumPad8) ||
                (e.KeyCode == Keys.NumPad9))                    
            {                
                double sqrt2_2 = 1 / (Math.Sqrt(2));
                if (e.KeyCode == Keys.NumPad1) // лево-низ 
                {
                    E.v1 = -sqrt2_2;
                    E.v2 = sqrt2_2;
                }
                if ((e.KeyCode == Keys.NumPad2) || (e.KeyCode == Keys.Down)) // низ
                {
                    E.v1 = 0;
                    E.v2 = 1;
                }
                if (e.KeyCode == Keys.NumPad3) // право-низ
                {
                    E.v1 = sqrt2_2;
                    E.v2 = sqrt2_2;
                }
                if ((e.KeyCode == Keys.NumPad4) || (e.KeyCode == Keys.Left)) // лево
                {
                    E.v1 = -1;
                    E.v2 = 0;
                }
                if (e.KeyCode == Keys.NumPad5) // стоим
                {
                    E.v1 = 0;
                    E.v2 = 0;
                }
                if ((e.KeyCode == Keys.NumPad6) || (e.KeyCode == Keys.Right)) // право
                {
                    E.v1 = 1;
                    E.v2 = 0;
                }
                if (e.KeyCode == Keys.NumPad7) // лево-верх 
                {
                    E.v1 = -sqrt2_2;
                    E.v2 = -sqrt2_2;
                }
                if ((e.KeyCode == Keys.NumPad8) || (e.KeyCode == Keys.Up)) // верх
                {
                    E.v1 = 0;
                    E.v2 = -1;
                }
                if (e.KeyCode == Keys.NumPad9) // право-верх
                {
                    E.v1 = sqrt2_2;
                    E.v2 = -sqrt2_2;
                }

                List<double> Times = new List<double>(); // времена до поимки, если по одиночке
                // ѕересчет управлени€ преследователей
                for (int i = 0; i < P.Count; i++)
                {
                    P[i].Calc_l(E.v1, E.v2);
                    P[i].Calc_TimeBeforeCapture(E.coor1, E.coor2);
                    if (P[i].TimeBeforeCapture > 0)
                        Times.Add(P[i].TimeBeforeCapture);
                }
                Persecutor.Calc_TimeBeforeSynchronousCapture(Times);
                for (int i = 0; i < P.Count; i++)
                {
                    P[i].Calc_r();
                    P[i].Calc_u1_u2(E.v1, E.v2);
                }

                // если преследование еще не началось - 
                //провер€ем нажата ли люба€ клавиша управлени€
                if (!(StartPersecution))
                {
                        StartPersecution = true;
                        timEps.Enabled = true;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalConst.Init();
            GlobalConst.M = comboBox1.SelectedIndex;
            GlobalConst.U = comboBox2.SelectedIndex;
            GlobalConst.A = comboBox3.SelectedIndex;
            Loading();
            
        }
     
    }
}