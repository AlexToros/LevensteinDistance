using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevensteinPresentation
{
    public partial class InfoPanel : UserControl
    {
        private LevenstainMatrix LevMatrix;
        public InfoPanel()
        {
            InitializeComponent();
        }

        public void Build(LevenstainMatrix levenstainMatrix)
        {
            BindingContext context = new BindingContext();
            LevMatrix = levenstainMatrix;
            SecondWord.DataBindings.Clear();
            SecondWord.DataBindings.Add("Text", LevMatrix, "SecondWord");
            FirstWord.DataBindings.Clear();
            FirstWord.DataBindings.Add("Text", LevMatrix, "FirstWord");
            Cost.DataBindings.Clear();
            Cost.DataBindings.Add("Text", LevMatrix, "Cost");
            LevResult.DataBindings.Clear();
            LevResult.DataBindings.Add("Text", LevMatrix, "Levenstein");
        }
    }
}
