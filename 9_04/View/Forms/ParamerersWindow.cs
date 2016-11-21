using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SynchronousMultipleCapture.GameLogic;

namespace SynchronousMultipleCapture
{
    public partial class ParamerersWindow : Form
    {

        public ParamerersWindow()
        {
            InitializeComponent();

            comboBox1.DataSource = Enum.GetValues(typeof(GameParameters));
            comboBox2.DataSource = Enum.GetValues(typeof(CaptureCount));
            comboBox3.DataSource = Enum.GetValues(typeof(A));
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            int U = (int)comboBox1.SelectedItem; 
            U = (U == -1) ? 2 : U;// дефолтное значение 2 
            int M = (int)comboBox2.SelectedItem; 
            M = (M == -1) ? 0 : M;// дефолтное значение 0 
            int A = (int)comboBox3.SelectedItem;
            A = (A == -1) ? 0 : A;// дефолтное значение 0 

            var gameWindow = new GameForm(M, U, A);
            gameWindow.Show();
        }

    }
}
