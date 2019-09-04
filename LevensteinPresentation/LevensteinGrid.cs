using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LevensteinPresentation
{
    class LevensteinGrid : DataGridView
    {
        private LevenstainMatrix LevMatrix;

        public LevensteinGrid() : base() { }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ResumeLayout(false);

            AllowUserToAddRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            EditMode = DataGridViewEditMode.EditProgrammatically;
            SelectionMode = DataGridViewSelectionMode.CellSelect;
            MultiSelect = false;

            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            RowHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            RowHeadersWidth = 50;

            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DefaultCellStyle.SelectionBackColor = Color.White;
            DefaultCellStyle.SelectionForeColor = Color.Black;
            DoubleBuffered = true;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (LevMatrix != null)
            {
                if (!LevMatrix.IsFinished)
                {
                    foreach (DataGridViewRow Row in Rows)
                        foreach (DataGridViewCell Cell in Row.Cells)
                        {
                            if (Row.Index == LevMatrix.RowCurrentIndex - 1 && Cell.ColumnIndex == LevMatrix.ColumnCurrentIndex ||
                                Row.Index == LevMatrix.RowCurrentIndex && Cell.ColumnIndex == LevMatrix.ColumnCurrentIndex - 1)
                                Cell.Style.BackColor = Color.LightBlue;
                            else if (Row.Index == LevMatrix.RowCurrentIndex - 1 && Cell.ColumnIndex == LevMatrix.ColumnCurrentIndex - 1)
                                Cell.Style.BackColor = Color.DarkCyan;
                            else
                                Cell.Style.BackColor = Color.White;
                        }
                }
                else
                {
                    foreach (DataGridViewRow Row in Rows)
                        foreach (DataGridViewCell Cell in Row.Cells)
                            Cell.Style.BackColor = Color.White;
                    Rows[LevMatrix.RowCount - 1].Cells[LevMatrix.ColumnCount - 1].Style.BackColor = Color.LightGreen;
                }
            }
        }

        public void Build(LevenstainMatrix levMatrix)
        {
            InitializeComponent();
            Rows.Clear();
            LevMatrix = levMatrix;

            LevMatrix.OnCellChanged += LevMatrix_OnCellChanged;
            LevMatrix.OnNewCurrentCell += LevMatrix_OnNewCurrentCell;
            RowCount = LevMatrix.RowCount;
            ColumnCount = LevMatrix.ColumnCount;

            FillInitial(levMatrix);
            CurrentCell = Rows[1].Cells[1];
        }

        private void FillInitial(LevenstainMatrix levMatrix)
        {
            Rows[0].Cells[0].Value = LevMatrix[0, 0];
            for (int i = 1; i < RowCount; i++)
            {
                Rows[i].Cells[0].Value = LevMatrix[i, 0];
                Rows[i].HeaderCell.Value = LevMatrix.FirstWord[i - 1].ToString();
            }
            for (int i = 1; i < ColumnCount; i++)
            {
                Rows[0].Cells[i].Value = LevMatrix[0, i];
                Columns[i].HeaderCell.Value = LevMatrix.SecondWord[i - 1].ToString();
            }
            Rows[0].HeaderCell.Value = Columns[0].HeaderCell.Value = "*";
        }

        private void LevMatrix_OnNewCurrentCell(int rowIndx, int colIndx)
        {
            CurrentCell = Rows[rowIndx].Cells[colIndx];
        }

        private void LevMatrix_OnCellChanged(int rowIndx, int colIndx, int? Val)
        {
            Rows[rowIndx].Cells[colIndx].Value = Val;
        }
    }
    
}
