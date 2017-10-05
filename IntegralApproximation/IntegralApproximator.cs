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

        double?[] solutions = new double?[6];
        int currentIndex = -1;

        MathFunction func = new PowerFunction(1.0d, new XFunction(1.0d), 1);

        private readonly double width = 3;
        private readonly double alpha = 0.5;

        public IntegralApproximator()
        {
            InitializeComponent();
        }

        private void IntegralApproximator_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "F(x) = " + func.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GraphicRepresentation frm = new GraphicRepresentation();

            GraphicPainter painter = new GraphicPainter();
            painter.DrawElement = frm.pictureBoxToDraw;

            ConfigurePainter(ref painter, currentIndex);

            frm.CurrentPainter = painter;

            frm.Show();
        }

        private void CalculateSolution(IntegralMethod method, out double? solution)
        {
            try
            {
                double a = Convert.ToDouble(textBox1.Text),
                       b = Convert.ToDouble(textBox2.Text);
                int n = Convert.ToInt32(numericUpDown1.Value);

                solution = func.CalculateDeterminedIntegral(method, a, b, n);
            }
            catch (InvalidCastException exp)
            {
                solution = null;
                MessageBox.Show(exp.Message);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)
                return;

            currentIndex = 0;
            CalculateSolution(new RectangularMethod(RectangularMethodType.Left), out solutions[0]);
            ShowSolution(solutions[0]);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)
                return;

            currentIndex = 1;
            CalculateSolution(new RectangularMethod(RectangularMethodType.Right), out solutions[1]);
            ShowSolution(solutions[1]);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton3.Checked)
                return;

            currentIndex = 2;
            CalculateSolution(new RectangularMethod(RectangularMethodType.Central), out solutions[2]);
            ShowSolution(solutions[2]);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton4.Checked)
                return;

            currentIndex = 3;
            CalculateSolution(new TrapezoidMethod(), out solutions[3]);
            ShowSolution(solutions[3]);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton5.Checked)
                return;

            currentIndex = 4;
            CalculateSolution(new SimpsonMethod(), out solutions[4]);
            ShowSolution(solutions[4]);
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton6.Checked)
                return;

            currentIndex = 5;
            CalculateSolution(new GaussIntegralMethod(), out solutions[5]);
            ShowSolution(solutions[5]);
        }

        private void ConfigurePainter(ref GraphicPainter painter, int currentIndex)
        {
            try
            {
                double a = Convert.ToDouble(textBox1.Text),
                       b = Convert.ToDouble(textBox2.Text);
                int n = Convert.ToInt32(numericUpDown1.Value);

                ConfigureBounds(ref painter, a, b);
                painter.Draw(func, GetRandColor(), (float)width);
                DrawCurrentRegion(ref painter, currentIndex, a, b, n);
            } 
            catch (InvalidCastException exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ConfigureBounds(ref GraphicPainter painter, double a, double b)
        {
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
        private void DrawCurrentRegion(ref GraphicPainter painter, int currentIndex, double a, double b, int n)
        {
            switch (currentIndex)
            {
                case 0:
                    painter.DrawPath(painter.PathForRectangularMethod(func, RectangularMethodType.Left, a, b, n), 
                        GetRandColor(), (float)width, GetRandColor(), alpha);
                    break;
                case 1:
                    painter.DrawPath(painter.PathForRectangularMethod(func, RectangularMethodType.Right, a, b, n),
                        GetRandColor(), (float)width, GetRandColor(), alpha);
                    break;
                case 2:
                    painter.DrawPath(painter.PathForRectangularMethod(func, RectangularMethodType.Central, a, b, n),
                        GetRandColor(), (float)width, GetRandColor(), alpha);
                    break;
                case 3:
                    painter.DrawPath(painter.PathForTrapezoidMethod(func, a, b, n),
                        GetRandColor(), (float)width, GetRandColor(), alpha);
                    break;
                case 4:
                    painter.DrawPath(painter.PathForSimpsonMethod(func, a, b, n),
                        GetRandColor(), (float)width, GetRandColor(), alpha);
                    break;
                case 6:
                    painter.DrawPath(painter.PathForFunction(func, a, b),
                        GetRandColor(), (float)width, GetRandColor(), alpha);
                    break;
                default:
                    break;
            }
        }

        private Color GetRandColor()
        {
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        private void ShowSolution(double? solution)
        {
            richTextBox2.Text = "";

            Bitmap myBitmap = new Bitmap(Image.FromFile("LIT-small.jpg"));
            Clipboard.SetDataObject(myBitmap);
            DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);
            if (richTextBox2.CanPaste(format))
                richTextBox2.Paste(format);

            richTextBox2.Text += " = " + solution != null ? solution.ToString() : "undefined solution";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GraphicRepresentation frm = new GraphicRepresentation();

            GraphicPainter painter = new GraphicPainter();
            painter.DrawElement = frm.pictureBoxToDraw;

            ConfigurePainter(ref painter, 6);

            frm.CurrentPainter = painter;

            frm.Show();
        }
    }
}
