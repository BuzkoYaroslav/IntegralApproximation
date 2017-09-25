using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using library;

namespace IntegralApproximation
{
    public partial class IntegralApproximator : Form
    {
        Random rnd = new Random();

        GraphicPainter[] painters = new GraphicPainter[7];
        double[] solutions = new double[6];

        MathFunction func = new PowerFunction(1.0d, new XFunction(1.0d), 3);

        private readonly double a = 0;
        private readonly double b = 2.3;

        private readonly double width = 3;
        private readonly int n = 10;

        public IntegralApproximator()
        {
            InitializeComponent();
        }

        private void IntegralApproximator_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(func.ToString());
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)
                return;

            if (painters[0] != null)
            {
                painters[0].UpdateUI();
                richTextBox1.Text = solutions[0].ToString();
                return;
            }

            ConfigurePainter(out painters[0]);
            painters[0].Draw(func, GetRandColor(), (float)width);
            painters[0].DrawPath(painters[0].PathForRectangularMethod(func, RectangularMethodType.Left, a, b, n), GetRandColor(), (float)width, GetRandColor(), 0.5);

            solutions[0] = func.CalculateDeterminedIntegral(new RectangularMethod(RectangularMethodType.Left), a, b, n);
            richTextBox1.Text = solutions[0].ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)
                return;

            if (painters[1] != null)
            {
                painters[1].UpdateUI();
                richTextBox1.Text = solutions[1].ToString();
                return;
            }

            ConfigurePainter(out painters[1]);
            painters[1].Draw(func, GetRandColor(), (float)width);
            painters[1].DrawPath(painters[1].PathForRectangularMethod(func, RectangularMethodType.Right, a, b, n), GetRandColor(), (float)width, GetRandColor(), 0.5);

            solutions[1] = func.CalculateDeterminedIntegral(new RectangularMethod(RectangularMethodType.Right), a, b, n);
            richTextBox1.Text = solutions[1].ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton3.Checked)
                return;

            if (painters[2] != null)
            {
                painters[2].UpdateUI();
                richTextBox1.Text = solutions[2].ToString();
                return;
            }

            ConfigurePainter(out painters[2]);
            painters[2].Draw(func, GetRandColor(), (float)width);
            painters[2].DrawPath(painters[2].PathForRectangularMethod(func, RectangularMethodType.Central, a, b, n), GetRandColor(), (float)width, GetRandColor(), 0.5);

            solutions[2] = func.CalculateDeterminedIntegral(new RectangularMethod(RectangularMethodType.Central), a, b, n);
            richTextBox1.Text = solutions[2].ToString();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton4.Checked)
                return;

            if (painters[3] != null)
            {
                painters[3].UpdateUI();
                richTextBox1.Text = solutions[3].ToString();
                return;
            }

            ConfigurePainter(out painters[3]);
            painters[3].Draw(func, GetRandColor(), (float)width);
            painters[3].DrawPath(painters[3].PathForTrapezoidMethod(func, a, b, n), GetRandColor(), (float)width, GetRandColor(), 0.5);

            solutions[3] = func.CalculateDeterminedIntegral(new TrapezoidMethod(), a, b, n);
            richTextBox1.Text = solutions[3].ToString();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton5.Checked)
                return;

            if (painters[4] != null)
            {
                painters[4].UpdateUI();
                richTextBox1.Text = solutions[4].ToString();
                return;
            }

            ConfigurePainter(out painters[4]);
            painters[4].Draw(func, GetRandColor(), (float)width);
            painters[4].DrawPath(painters[4].PathForSimpsonMethod(func, a, b, n), GetRandColor(), (float)width, GetRandColor(), 0.5);

            solutions[4] = func.CalculateDeterminedIntegral(new SimpsonMethod(), a, b, n);
            richTextBox1.Text = solutions[4].ToString();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton6.Checked)
                return;

            if (painters[5] != null)
            {
                painters[5].UpdateUI();
                richTextBox1.Text = solutions[5].ToString();
                return;
            }

            ConfigurePainter(out painters[5]);
        }

        private void ConfigurePainter(out GraphicPainter painter)
        {
            painter = new GraphicPainter(pictureBox1);

            double maxF = func.MaxValue(a, b, false),
                   minF = func.MinValue(a, b, false);

            double x, y, x1, y1;

            if (a < 0)
                x = a - 1;
            else
                x = -1;
            if (b < 0)
                x1 = 1;
            else
                x1 = b + 1;

            if (maxF < 0)
                y1 = 1;
            else
                y1 = maxF + 1;

            if (minF > 0)
                y = -1;
            else
                y = minF - 1;

            painter.XBounds = new Point((int)x - 1, (int)x1 + 1);
            painter.YBounds = new Point((int)y - 1, (int)y1 + 1);
        }
        private Color GetRandColor()
        {
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton7.Checked)
                return;

            if (painters[6] != null)
            {
                painters[6].UpdateUI();
                //richTextBox1.Text = solutions[6].ToString();
                return;
            }

            ConfigurePainter(out painters[6]);
            painters[6].Draw(func, GetRandColor(), (float)width);
            painters[6].DrawPath(painters[6].PathForFunction(func, a, b), GetRandColor(), (float)width, GetRandColor(), 0.5);
        }
    }
}
