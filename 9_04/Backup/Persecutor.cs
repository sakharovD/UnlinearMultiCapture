using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronousMultipleCapture
{
    public class Persecutor
    {
        /// <summary> ������ ���������� �������������� </summary>
        public double coor1;
        /// <summary> ������ ���������� �������������� </summary>
        public double coor2;
        /// <summary> ������ ���������� ���������� �������������� </summary>
        public double u1;
        /// <summary> ������ ���������� ���������� �������������� </summary>
        public double u2;
        /// <summary> lambda � ������� u = v - lambda * z^0 </summary>
        public double l;
        /// <summary> ���� ������, �� lambda = 0 � ������� u = v - lambda * z^0 </summary>
        public bool capture;
        /// <summary> ������ ���������� z^0 � ������� u = v - lambda * z^0 </summary>
        public double Z1;
        /// <summary> ������ ���������� z^0 � ������� u = v - lambda * z^0 </summary>
        public double Z2;
        /// <summary> ����� �� ������ �� ������� u = v - lambda * z^0 </summary>
        public double TimeBeforeCapture;
        /// <summary> ����� �� ������� ������������� ������������ ������ �� ������� u = v - lambda * rho * z^0 </summary>
        static public double TimeBeforeSynchronousCapture;
        /// <summary> rho � ������� u = v - lambda * rho * z^0 </summary>
        public double r;

        /// <summary> ���������� l �� ���������� ���������� v1, v2 </summary> <param name="v1"> v1 </param> <param name="v2"> v2 </param>
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
        }

        /// <summary> ���������� TimeBeforeCapture �� ����������� ���������� Ecoor1, Ecoor2 </summary> <param name="Ecoor1"> Ecoor1 </param> <param name="Ecoor2"> Ecoor2 </param>
        public void Calc_TimeBeforeCapture(double Ecoor1, double Ecoor2)
        {
            if (l == 0)
            {                
                TimeBeforeCapture = -1;
            }
            else
            {
                TimeBeforeCapture = Math.Sqrt((coor1 - Ecoor1) * (coor1 - Ecoor1) +
                    (coor2 - Ecoor2) * (coor2 - Ecoor2)) / 
                    (l * Math.Sqrt(Z1 * Z1 + Z2 * Z2));
            }
        }

        /// <summary> ���������� ������� �� ������� ������������� ������������ ������ </summary> <param name="Times"> �������������� �������� ������� ������ </param>
        static public void Calc_TimeBeforeSynchronousCapture(List<double> Times)
        {
            //������ ���������� �� ����������� (����� ����� M-� �������)
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
            //����� M-� �������
            if (Times.Count > GlobalConst.M - 1)
            {
                TimeBeforeSynchronousCapture = Times[GlobalConst.M - 1];
            }
        }

        /// <summary> ���������� rho � ������� u = v - lambda * rho * z^0, ���� lambda > 0 </summary>
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

        /// <summary> ���������� u_1, u_2 �� ���������� ���������� v1, v2 </summary> <param name="v1"> v1 </param> <param name="v2"> v2 </param>
        public void Calc_u1_u2(double v1, double v2)
        {            
            u1 = v1 - l * r * Z1 ;
            u2 = v2 - l * r * Z2;
        }       
    }
}
