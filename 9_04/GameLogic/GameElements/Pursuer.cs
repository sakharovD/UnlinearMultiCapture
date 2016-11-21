using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using SynchronousMultipleCapture.GameLogic.Games;

namespace SynchronousMultipleCapture.GameLogic.GameElements
{
    public class Pursuer
    {
        public Vector coordinates;
        /// <summary>
        /// управление
        /// </summary>
        public Vector u;
        /// <summary> lambda в формуле u = v - lambda * z^0 </summary>
        public double l;
        /// <summary> если догнал, то lambda = 0 в формуле u = v - lambda * z^0 </summary>
        public bool capture;
        public Vector Z;
        /// <summary> Первая координата z^0 в формуле u = v - lambda * z^0 </summary>
       // public double Z1;
        /// <summary> Вторая координата z^0 в формуле u = v - lambda * z^0 </summary>
       // public double Z2;
        /// <summary> Время до поимки по формуле u = v - lambda * z^0 </summary>
        public double TimeBeforeCapture;
        /// <summary> Время до строгой одновременной многократной поимки по формуле u = v - lambda * rho * z^0 </summary>
        static public double TimeBeforeSynchronousCapture;
        /// <summary> rho в формуле u = v - lambda * rho * z^0 </summary>
        public double r;

        public void setl(double l)
        {
            this.l = l;
        }

        //public Pursuer(double coord1, double coord2, double z1, double z2, bool captured)
        public Pursuer(Vector coordinates, Vector Z, bool captured)
        {
            //this.coordinates = new Vector(coord1, coord2);
            this.coordinates = coordinates;
            /*this.Z1 = z1;
            this.Z2 = z2;*/
            this.Z = Z;
            this.capture = captured;
        }
        
       /* /// <summary> Вычисление l по управлению убегающего v1, v2 </summary> <param name="v1"> v1 </param> <param name="v2"> v2 </param>
        public void Calc_l(double v1, double v2)
        {
            if (capture)
            {
                l = 0;
            }
            else
            {
                l = ((Z1 * v1 + Z2 * v2) + 
                    Math.Sqrt((Z1 * v1 + Z2 * v2) * (Z1 * v1 + Z2 * v2) +
                    (Z1 * Z1 + Z2 * Z2) * (1 - (v1 * v1 + v2 * v2)))) /
                    (Z1 * Z1 + Z2 * Z2);
            }         
        }*/

        /// <summary> Вычисление TimeBeforeCapture по координатам убегающего Ecoor1, Ecoor2 </summary> <param name="Ecoor1"> Ecoor1 </param> <param name="Ecoor2"> Ecoor2 </param>
        public void Calc_TimeBeforeCapture(double Ecoor1, double Ecoor2)
        {
            if (l == 0)
            {                
                TimeBeforeCapture = -1;
            }
            else
            {
                TimeBeforeCapture = Math.Sqrt((coordinates.X - Ecoor1) * (coordinates.X - Ecoor1) +
                    (coordinates.Y - Ecoor2) * (coordinates.Y - Ecoor2)) / 
                    (l * Math.Sqrt(Z.X * Z.X + Z.Y * Z.Y));
            }
        }

        /// <summary> Вычисление времени до строгой одновременной многократной поимки </summary> <param name="Times"> Индивидуальные конечные времена поимки </param>
        static public void Calc_TimeBeforeSynchronousCapture(List<double> Times , int m)
        {
            //Прямая сортировка по возрастанию (потом взять M-й элемент)
            double min;
            int num;
            for (int i = 0; i < Times.Count; i++ )
            {
                num = i;
                min = Times[i];
                for (int j = i + 1; j < Times.Count; j++ )
                {
                    if (min > Times[j])
                    {
                        min = Times[j];
                        num = j;
                    }
                }
                if (i != num)
                {
                    Times[num] = Times[i];
                    Times[i] = min;
                }
            }
            //Взять M-й элемент
            if (Times.Count > m - 1)
            {
                TimeBeforeSynchronousCapture = Times[m - 1];
            }
        }

        /// <summary> Вычисление rho в формуле u = v - lambda * rho * z^0, если lambda > 0 </summary>
        public void Calc_r()
        {
            if (l > 0)
            {
                if (TimeBeforeCapture < TimeBeforeSynchronousCapture)
                {
                    r = TimeBeforeCapture / TimeBeforeSynchronousCapture;
                }
                else
                    r = 1;
                    
            }
                else
            {
                r = 1;
            }            
        }

        /// <summary> Вычисление u_1, u_2 по управлению убегающего v1, v2 </summary> <param name="v1"> v1 </param> <param name="v2"> v2 </param>
        public void Calc_u1_u2(double v1, double v2, int A, int U, Game game)
        {            
            //u.X = v1 - l * r * Z1;
            //u.Y = v2 - l * r * Z2;

            u.X = v1 - game.Calc_lambda(v1, v2, Z.X, Z.Y, TimeBeforeCapture, A, U, capture) * game.Phi(Z.X, Z.Y, TimeBeforeCapture, A).X;
            u.Y = v2 - game.Calc_lambda(v1, v2, Z.X, Z.Y, TimeBeforeCapture, A, U, capture) * game.Phi(Z.X, Z.Y, TimeBeforeCapture, A).Y;
        }       

    }
}
