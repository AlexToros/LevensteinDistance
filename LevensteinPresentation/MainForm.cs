using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevensteinPresentation
{
    public partial class MainForm : Form
    {
        LevenstainMatrix Matrix;
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBuid_Click(object sender, EventArgs e)
        {
            if (tbFirstWord.Text.Length > 0 && tbSecondWord.Text.Length > 0)
            {
                Matrix = new LevenstainMatrix(tbFirstWord.Text, tbSecondWord.Text);
                levensteinGrid1.Build(Matrix);
                infoPanel1.Build(Matrix);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            Matrix?.StepForward();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Matrix?.StepBack();
        }
    }
}
