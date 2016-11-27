using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SynchronousMultipleCapture.GameLogic.GameElements;
using System.Windows;

namespace SynchronousMultipleCapture.GameLogic.Games
{
    class TriangleGame : Game // случай треугольника
    {
        public TriangleGame(int m, int u, int a, double scale, Evader E)
        {
            this.M = m;
            this.U = u;
            this.A = a;

            this.evader = E;
            Random md = new Random();

            // начальные положения преследователей
            int persuersSetCount = m;
            for (int persuersSet = 1; persuersSet <= persuersSetCount; persuersSet++)
            {
               // for (int persuerNumber = 0; persuerNumber < 3; persuerNumber++)
               // {
                    int r1 = md.Next(1, (int)scale/2);
                    int r2 = md.Next(1, (int)scale/2);
                    double coord1 = r1;
                    double coord2 = scale * persuersSet + r2;
                    Vector coordinates = new Vector(coord1, coord2);
                    double Z1 = coord1 - E.coordinates.X;
                    double Z2 = coord2 - E.coordinates.Y;
                    Vector Z = new Vector(Z1, Z2);
                    pursuers.Add(new Pursuer(coordinates, Z, false));

                    r1 = md.Next(1, (int)scale/2);
                    r2 = md.Next(1, (int)scale/2);
                    coord1 = scale * persuersSet + r1;
                    coord2 = -scale * persuersSet + r2;
                    coordinates = new Vector(coord1, coord2);
                    Z1 = coord1 - E.coordinates.X;
                    Z2 = coord2 - E.coordinates.Y;
                    Z = new Vector(Z1, Z2);
                    pursuers.Add(new Pursuer(coordinates, Z, false));

                    r1 = md.Next(1, (int)scale/2);
                    r2 = md.Next(1, (int)scale/2);
                    coord1 = -scale * persuersSet + r1;
                    coord2 = -scale * persuersSet + r2;
                    coordinates = new Vector(coord1, coord2);
                    Z1 = coord1 - E.coordinates.X;
                    Z2 = coord2 - E.coordinates.Y;
                    Z = new Vector(Z1, Z2);
                    pursuers.Add(new Pursuer(coordinates, Z, false)); 
               // }
            }

          /*  for (int i = 1; i <= m; i++) // начальные положения преследователей
            {
                double coord1 = 0;
                double coord2 = scale * i;
                Vector coordinates = new Vector(coord1, coord2);
                double Z1 = coord1 - E.coordinates.X;
                double Z2 = coord2 - E.coordinates.Y;
                Vector Z = new Vector(Z1, Z2);
                pursuers.Add(new Pursuer(coordinates, Z, false));
            }

            for (int i = m + 1; i <= 2 * m; i++)
            {
                double coord1 = scale * i;
                double coord2 = -scale * i;
                Vector coordinates = new Vector(coord1, coord2);
                double Z1 = coord1 - E.coordinates.X;
                double Z2 = coord2 - E.coordinates.Y;
                Vector Z = new Vector(Z1, Z2);
                pursuers.Add(new Pursuer(coordinates, Z, false));

            }

            for (int i = 2 * m + 1; i <= 3 * m; i++)
            {
                double coord1 = -scale * i;
                double coord2 = -scale * i;
                Vector coordinates = new Vector(coord1, coord2);
                double Z1 = coord1 - E.coordinates.X;
                double Z2 = coord2 - E.coordinates.Y;
                Vector Z = new Vector(Z1, Z2);
                pursuers.Add(new Pursuer(coordinates, Z, false));

            }*/
        }
    }
}
