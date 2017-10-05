using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegralApproximation
{
    public partial class GraphicRepresentation : Form
    {
        private GraphicPainter currentPainter;

        internal GraphicPainter CurrentPainter
        {
            get
            {
                return currentPainter;
            }
            set
            {
                currentPainter = value;
                currentPainter.DrawElement = pictureBoxToDraw;
            }
        }

        internal GraphicRepresentation()
        {
            InitializeComponent();
        }

        private void GraphicRepresentation_Load(object sender, EventArgs e)
        {
        }
    }
}
