using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using SynchronousMultipleCapture.GameLogic.GameElements;
using SynchronousMultipleCapture.View;

namespace SynchronousMultipleCapture.GameLogic.Games
{
    public abstract class Game
    {
        /// <summary> Количество преследователей (форма области) </summary>
        public int M;
        /// <summary> Множество допустимых управлений U </summary>
        public int U;
        /// <summary> Вид системы A(t) </summary>
        public int A;
        public Boolean Stand;
        public double time = 0;
        public double timeDelta = 1;
        /// <summary> Убегающий </summary>
        public Evader evader;
        /// <summary> Группа преследователей </summary>
        public List<Pursuer> pursuers = new List<Pursuer>();
        public GameStyleFabric style = new GameStyleFabric();
        int captureCount = 0;
        public List<double> Integrals;
        // функция от времени.
        public delegate double Function(double t);

        public double Integral(double t1, double t2, int pointsCount, Function f)
        {
            double sum = 0;
            double delta = (t2 - t1) / pointsCount;

            //if (t2 < 2 * GameForm.timeStep)
            //{
                for (int k = 0; k < pointsCount; k++)
                {
                    double t = t1 + k * delta;
                    sum += f(t) * delta;
                }
                //Integrals.Add(sum);
                return sum;
            //}

            /*else
            {
                for (int k = 0; k < razb; k++)
                {
                    double t = t1 + k * delta;
                    sum += f(t) * delta;
                }

                return sum + Integrals[Math.Truncate(t2/GameForm.timeStep)];
            }*/
        }

        public void makeMove(double timeStep, Game game)
        {
            // подынтегральные функции
            Function f1 = (t) => Math.Exp(Math.Cos(t - 1));
            Function f2 = (t) => t;

            // делаем шаг убегающего
            switch (A)
            {
                case 0:
                    {
                        evader.coordinates += evader.v * timeStep;
                        break;
                    }
                case 1: 
                    {
                        var variable1 = Math.Exp(1 - Math.Cos(game.time));
                        var variable2 = Math.Sin(game.time);
                        var integralValue = Integral(0, game.time, 1000, f1);
                        var variable3 = evader.v.X * integralValue;
                        // старные координаты
                        var X = evader.coordinates.X;
                        var Y = evader.coordinates.Y;
                        // новые координаты
                        evader.coordinates.Y = /*Y * variable1 +*/ X * variable2 * variable1;
                        evader.coordinates.X = /*X * variable1 +*/ variable1 * variable3;
                        break; 
                    }
                case 2: 
                    {
                       // var v2 = Math.Sin(game.time);
                       // var v3 = Math.Cos(game.time);
                       // var X = evader.coordinates.X;
                       // var Y = evader.coordinates.Y;
                       // evader.coordinates.X = /*X - Y * v2 +*/ Int(game, 3, timeStep) - v2 * Int(game, 4, timeStep);
                       // evader.coordinates.Y = /*X * v2 + Y * v3 * v3 +*/ v2 * Int(game, 3, timeStep) + v3 * v3 * Int(game, 4, timeStep);
                        break;
                    }
            }
            
            captureCount = 0;
            for (int i = 0; i < pursuers.Count; i++)
            {
                // делаем шаг догоняющих
                pursuers[i].coordinates += pursuers[i].u * timeStep;
                double distance = (pursuers[i].coordinates - evader.coordinates).Length;
                // проверям условия поимки
                if ((!(pursuers[i].capture)) && (distance < 2 * timeStep))
                {
                    captureCount = captureCount + 1;
                }
            }

            if (captureCount >= M)
            {
                for (int i = 0; i < pursuers.Count; i++)
                {
                    // проверям условия поимки
                    double distance = (pursuers[i].coordinates - evader.coordinates).Length;
                    if ((!(pursuers[i].capture)) && (distance < 2 * timeStep))
                    {
                        pursuers[i].capture = true;
                    }
                }
            }
        }

        public bool isGameEnded()
        {
            return captureCount >= M; // произошла M-кратная (не одновременная) поимка
        }

        // фундаментальная матрица в случае А 
        public Vector Phi(double Z1, double Z2, double time, int A)
        {
            switch (A)
            {
                case 0: return new Vector(Z1, Z2);
                case 1: return new Vector(
                     Z1 * Math.Exp(1 - Math.Cos(time)),
                     Z1 * Math.Sin(time) * Math.Exp(1 - Math.Cos(time)) + Z2 * Math.Exp(1 - Math.Cos(time))
                                    );
                case 2: return new Vector(
                     Z1 - Z2 * Math.Sin(time),
                     Z1 * Math.Sin(time) + Z2 * Math.Cos(time) * Math.Cos(time)
                                    );
            }
            return new Vector(0,0);
        }
      
        public bool IsInU(double x, double y, int U) // проверяет, лежит ли вектор внутри множества управлений U
        {
            switch (U)
            {
                case 0: if (Math.Sqrt(x) + Math.Sqrt(y) > 1)
                        return false; break;
                case 1: if ((y > Math.Sqrt(3) * x + 1) || (y < 1 - Math.Sqrt(3)) || (y > 1 - Math.Sqrt(3) * x))
                        return false; break;
                case 2: if ((x > 2) || (x < -2) || (y > 1) || (y < -1))
                        return false; break;
                case 3: if ((y > Math.Sqrt(3) / 2) || (y < -Math.Sqrt(3) / 2) || (y > -Math.Sqrt(3) * x + Math.Sqrt(3)) ||
                    (y < Math.Sqrt(3) * x - Math.Sqrt(3)) || (y > Math.Sqrt(3) * x + Math.Sqrt(3)) || (y < -Math.Sqrt(3) * x - Math.Sqrt(3)))
                        return false; break;
                case 4: if ((y < Math.Sqrt(3) * x - 1) || (y < - Math.Sqrt(3) * x - 1) || (Math.Sqrt(y + 1) + Math.Sqrt(x) > 1))
                        return false; break;
            }
            return true;
        }

        /// <summary> Вычисление l по управлению убегающего v1, v2 </summary> <param name="v1"> v1 </param> <param name="v2"> v2 </param>
        public double Calc_lambda(double v1, double v2, double Z1, double Z2, double time, int A, int U, bool capture)
        {
            if (capture) return 0;

            if ((A == 0) && (U == 0))
            {
                return ((Z1 * v1 + Z2 * v2) +
                       Math.Sqrt((Z1 * v1 + Z2 * v2) * (Z1 * v1 + Z2 * v2) +
                       (Z1 * Z1 + Z2 * Z2) * (1 - (v1 * v1 + v2 * v2)))) /
                       (Z1 * Z1 + Z2 * Z2);
            }

            double lambda = 0.01d;

            while (lambda < 1d)
            {
                if (IsInU(v1 - lambda * Phi(Z1, Z2, time, A).X, v2 - lambda * Phi(Z1, Z2, time, A).Y, U))
                    lambda += 0.01d;
                else break;
            }; 
            return lambda;
            
        }
    }
}
