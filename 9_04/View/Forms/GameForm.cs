using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using SynchronousMultipleCapture.GameLogic.Games;
using SynchronousMultipleCapture.GameLogic.GameElements;
using Point = System.Windows.Point;

namespace SynchronousMultipleCapture
{
    public partial class GameForm : Form
    {
        private Game game;
        public double timeStep = 1;

        public GameForm(int M, int U, int A)
        {
            // инициализация преследователей
            double scale = 100; // масштаб
            Evader evader = new Evader(0, 0);

            if (U == 2)
            {
                int amount = 4 * M;
                game = new SquareGame(M, U, A, scale, evader);
            }
            if ((U == 1) || (U == 0) || (U == 3) || (U == 4))
            {
                int amount = 3 * M; // кол-во преследователей
                game = new TriangleGame(M, U, A, scale, evader);
            }

            InitializeComponent();
        }

        /// <summary>
        ///  Рисует по тику таймера текущую позицию игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            float coordToGraphichScale = 1;
            // количество догоняющих в одной группе
            // какой то непонятный расчет скалирования координат
            int persuersInSet = game.pursuers.Count / game.M;
            /*for (int i = 1; i <= game.pursuers.Count; i++)
            {
                int endOfseries = 0;
                if (i % persuersInSet == 0) { endOfseries = 1; }
                int j = i + 1 - endOfseries * persuersInSet;
                Vector pers1Coord = game.pursuers[i - 1].coordinates;
                Vector pers2Coord = game.pursuers[j - 1].coordinates;

                if (Math.Abs(pers1Coord.X) > scal) { scal = pers1Coord.X; }
                if (Math.Abs(pers1Coord.Y) > scal) { scal = pers1Coord.Y; }
                if (Math.Abs(pers2Coord.X) > scal) { scal = pers2Coord.X; }
                if (Math.Abs(pers2Coord.Y) > scal) { scal = pers2Coord.Y; }
            }

            if (game.A == 0) { scal = 200; }*/

            Graphics g = e.Graphics;
            float dx = this.Size.Width/2;
            float dy = this.Size.Height/2;
            g.TranslateTransform(dx, dy); // изменить начало координат обьекта graphics

            // рисовать объект - убегающий
            var size = game.style.gameElementsSize;
            Vector newcoords = game.evader.coordinates - new Vector(size / 2, size / 2);
            var x = (float)newcoords.X * coordToGraphichScale;
            var y = (float)newcoords.Y * coordToGraphichScale;
            g.FillEllipse(game.style.ColorPointEvader, x, y, size, size);

            // рисовать обьект - стрелочка убегающего
            Vector evaderCoord = game.evader.coordinates;
            Vector endLineCoord = game.evader.coordinates +  game.style.evaderArrowSize* game.evader.v;
            var p1 = vectorToPoint(evaderCoord);
            var p2 = vectorToPoint(endLineCoord);
            g.DrawLine(game.style.ColorVelocityEvader, p1, p2);

            // отрисовать линии между догоняющими
            /* for (int i = 0; i < game.pursuers.Count; i++)
             {
                 int j = 0;
                 if (game.M + i < game.pursuers.Count)
                     j = game.M + i;
                 else
                     j = game.M + i - game.pursuers.Count;

                 Vector pers1Coord = game.pursuers[i].coordinates;
                 Vector pers2Coord = game.pursuers[j].coordinates;
                 System.Drawing.Point pers1 = vectorToPoint(pers1Coord);
                 System.Drawing.Point pers2 = vectorToPoint(pers2Coord);
                 g.DrawLine(game.style.coorPenPersecutor, pers1, pers2);
             }*/
             
                        
            // линии соединяющие догоняющих
            for (int i = 1; i <= game.pursuers.Count; i++)
            {
                int endOfseries = 0;
                if (i % persuersInSet == 0) endOfseries = 1;
                int j = i + 1 - endOfseries * persuersInSet;
                Vector pers1Coord = game.pursuers[i - 1].coordinates;
                Vector pers2Coord = game.pursuers[j - 1].coordinates;

                var pers1 = vectorToPoint(pers1Coord * coordToGraphichScale);
                var pers2 = vectorToPoint(pers2Coord * coordToGraphichScale);
                g.DrawLine(game.style.coorPenPersecutor, pers1, pers2);
            }


            // отрисовать все обьекты - догоняющие
            for (int i = 0; i < game.pursuers.Count; i++)
            {
                var size1 = game.style.gameElementsSize;
                Vector newcoords1 = game.pursuers[i].coordinates - new Vector(size1 / 2, size1 / 2);
                int x1 = (int)Math.Round(newcoords1.X * coordToGraphichScale);
                int y1 = (int)Math.Round(newcoords1.Y * coordToGraphichScale);
                g.FillEllipse(game.style.ColorPointPersecutors, x1, y1, size1, size1);

                // рисовать обьекты - стрелочки для догоняющих
                Vector persCoord = game.pursuers[i].coordinates;
                Vector persEndCoord = game.pursuers[i].coordinates + game.style.evaderArrowSize * game.pursuers[i].u;
                var pers3 = vectorToPoint(persCoord * coordToGraphichScale);
                var pers4 = vectorToPoint(persEndCoord * coordToGraphichScale);
                g.DrawLine(game.style.ColorVelocityPersecutors, pers3, pers4);
            }

        }

        /// <summary>
        /// Таймер шага игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timEps_Tick(object sender, EventArgs e)
        {
            game.time += game.timeDelta;

            setEvaderDirectionByCursorePosition();

            recalculatingPersuersDirections();

            game.makeMove(timeStep, game);
            if (game.isGameEnded()) { timEps.Enabled = false; }

            this.label2.Text = game.time.ToString();// лог времени
            Invalidate(); // обновление экрана
        }

        private void recalculatingPersuersDirections()
        {
            List<double> Times = new List<double>(); // времена до поимки, если по одиночке
            // Пересчет управления преследователей
            for (int i = 0; i < game.pursuers.Count; i++)
            {
                var evaderX = game.evader.v.X;
                var evaderY = game.evader.v.Y;
                var persuerX = game.pursuers[i].Z.X;
                var persuerY = game.pursuers[i].Z.Y;

                var lambda = game.Calc_lambda(evaderX, evaderY, persuerX, persuerY, game.time, game.A, game.U, game.pursuers[i].capture);

                game.pursuers[i].setl(lambda);
                game.pursuers[i].Calc_TimeBeforeCapture(game.evader.coordinates.X, game.evader.coordinates.Y);

                if (game.pursuers[i].TimeBeforeCapture > 0)
                {
                    Times.Add(game.pursuers[i].TimeBeforeCapture);
                }
            }
            Pursuer.Calc_TimeBeforeSynchronousCapture(Times, game.M);
            for (int i = 0; i < game.pursuers.Count; i++)
            {
                game.pursuers[i].Calc_u1_u2(game.evader.v.X, game.evader.v.Y, game.A, game.U, game);
            }
        }

        private void setEvaderDirectionByCursorePosition()
        {
            // определить направление движения убегающего по мышке
            var centerPoint = new Point(0, 0);
            var cursorePoint = new Point(
                Cursor.Position.X - Screen.PrimaryScreen.Bounds.Width / 2,
                Screen.PrimaryScreen.Bounds.Height / 2 - Cursor.Position.Y);
            var angle = GetAngleFromPoint(cursorePoint, centerPoint);
            var angleRad = (Math.PI / 180) * angle;
            game.evader.v = new Vector(Math.Cos(angleRad), -Math.Sin(angleRad));
        }

        /// <summary> Изменение управления убегающего</summary> 
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (isControlKeyPressed(e))
            {
                double sqrt2_2 = 1 / (Math.Sqrt(2));
                // определить направление движения убегающего по кнопкам
                if (e.KeyCode == Keys.NumPad1)                                  game.evader.v = new Vector(-sqrt2_2, sqrt2_2);
                if ((e.KeyCode == Keys.NumPad2) || (e.KeyCode == Keys.Down))    game.evader.v = new Vector(0, 1);
                if (e.KeyCode == Keys.NumPad3)                                  game.evader.v = new Vector(sqrt2_2, sqrt2_2);
                if ((e.KeyCode == Keys.NumPad4) || (e.KeyCode == Keys.Left))    game.evader.v = new Vector(-1, 0);
                if (e.KeyCode == Keys.NumPad5)                                  game.evader.v = new Vector(0, 0);
                if ((e.KeyCode == Keys.NumPad6) || (e.KeyCode == Keys.Right))   game.evader.v = new Vector(1, 0);
                if (e.KeyCode == Keys.NumPad7)                                  game.evader.v = new Vector(-sqrt2_2, -sqrt2_2);
                if ((e.KeyCode == Keys.NumPad8) || (e.KeyCode == Keys.Up))      game.evader.v = new Vector(0, -1);
                if (e.KeyCode == Keys.NumPad9)                                  game.evader.v = new Vector(sqrt2_2, -sqrt2_2);
            }
        }

        private static bool isControlKeyPressed(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right) ||
                            (e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down) ||
                            (e.KeyCode == Keys.NumPad1) || (e.KeyCode == Keys.NumPad2) ||
                            (e.KeyCode == Keys.NumPad3) || (e.KeyCode == Keys.NumPad4) ||
                            (e.KeyCode == Keys.NumPad5) || (e.KeyCode == Keys.NumPad6) ||
                            (e.KeyCode == Keys.NumPad7) || (e.KeyCode == Keys.NumPad8) ||
                            (e.KeyCode == Keys.NumPad9);
        }



        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!(game.isGameEnded()))
            {
                timEps.Enabled = true;
                game.time = 0;
                //label2.Text = game.time.ToString(); ;
            }
        }

        private System.Drawing.Point vectorToPoint(Vector vector)
        {
            int translatedEvaderCoordinateX = (int)Math.Round(vector.X);
            int translatedEvaderCoordinateY = (int)Math.Round(vector.Y);
            System.Drawing.Point point = new System.Drawing.Point(translatedEvaderCoordinateX, translatedEvaderCoordinateY);
            return point;
        }

        private double GetAngleFromPoint(Point point, Point centerPoint)
        {
            double dx = (point.Y - centerPoint.Y);
            double dy = (point.X - centerPoint.X);
            double theta = Math.Atan2(dy, dx);
            double angle = (90 - ((theta * 180) / Math.PI)) % 360;
            return angle;
        }
    }
}