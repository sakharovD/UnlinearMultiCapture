using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SynchronousMultipleCapture.GameLogic.GameElements;
using System.Windows;

namespace SynchronousMultipleCapture.GameLogic.Games
{
    class SquareGame : Game // прямоугольник
    {
        public SquareGame(int m, int u, int a, double scale, Evader E)
        {
            this.M = m;
            this.U = u;
            this.A = a;

            this.evader = E;

            int persuersSetCount = m;
            Random randonNumber = new Random();

            // начальные положения преследователей
            for (int persuersSet = 1; persuersSet <= persuersSetCount; persuersSet++)
            {
                for(int persuerNumber = 0; persuerNumber< 4; persuerNumber++)
                {
                    int r1 = randonNumber.Next(- (int) scale, (int) scale);
                    int r2 = randonNumber.Next(- (int) scale, (int) scale);
                    double coord1 = Math.Pow(-1, persuerNumber / 2) * scale * persuersSet + r1 /*- scale*/;
                    double coord2 = Math.Pow(-1, (persuerNumber + 1) % 3) * scale * persuersSet + r2 /*- scale*/;
                    Vector coordinates = new Vector(coord1, coord2);
                    double Z1 = coord1 - E.coordinates.X;
                    double Z2 = coord2 - E.coordinates.Y;
                    Vector Z = new Vector(Z1, Z2);
                    pursuers.Add(new Pursuer(coordinates, Z, false));
                }
            }

           /* for (int i = 1; i <= m; i++) // начальные положения преследователей
            {
                double coord1 = scale * i;
                double coord2 = scale * i;
                double Z1 = coord1 - E.coordinates.X;
                double Z2 = coord2 - E.coordinates.Y;
                pursuers.Add(new Pursuer(coord1, coord2, Z1, Z2, false));
            }

            for (int i = m + 1; i <= 2 * m; i++)
            {
                double coord1 = scale * i;
                double coord2 = -scale * i;
                double Z1 = coord1 - E.coordinates.X;
                double Z2 = coord2 - E.coordinates.Y;
                pursuers.Add(new Pursuer(coord1, coord2, Z1, Z2, false));
            }

            for (int i = 2 * m + 1; i <= 3 * m; i++)
            {
                double coord1 = -scale * i;
                double coord2 = scale * i;
                double Z1 = coord1 - E.coordinates.X;
                double Z2 = coord2 - E.coordinates.Y;
                pursuers.Add(new Pursuer(coord1, coord2, Z1, Z2, false));
            }

            for (int i = 3 * m + 1; i <= 4 * m; i++)
            {
                double coord1 = -scale * i;
                double coord2 = -scale * i;
                double Z1 = coord1 - E.coordinates.X;
                double Z2 = coord2 - E.coordinates.Y;
                pursuers.Add(new Pursuer(coord1, coord2, Z1, Z2, false));
            }*/
        }
    }
}
