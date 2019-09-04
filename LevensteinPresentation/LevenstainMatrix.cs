using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LevensteinPresentation
{
    class LevenstainMatrix : INotifyPropertyChanged
    {
        private bool state = true;
        private int?[,] matrix;
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<int, int, int?> OnCellChanged;
        public event Action<int, int> OnNewCurrentCell;

        public string FirstWord { get; private set; }
        public string SecondWord { get; private set; }

        public int RowCurrentIndex { get; private set; }
        public int ColumnCurrentIndex { get; private set; }

        public int RowCount { get => FirstWord.Length + 1; }
        public int ColumnCount { get => SecondWord.Length + 1; }

        public bool IsFinished { get => RowCurrentIndex == RowCount - 1 && ColumnCurrentIndex == ColumnCount - 1 && !state; }

        public int Cost { get => FirstWord[RowCurrentIndex - 1] == SecondWord[ColumnCurrentIndex - 1] ? 0 : 1; }

        public int Levenstein
        {
            get
            {
                int min1 = matrix[RowCurrentIndex - 1, ColumnCurrentIndex].Value + 1;
                int min2 = matrix[RowCurrentIndex, ColumnCurrentIndex - 1].Value + 1;
                int min3 = matrix[RowCurrentIndex - 1, ColumnCurrentIndex - 1].Value + Cost;
                return Math.Min(Math.Min(min1, min2), min3);
            }
        }

        public int? this[int r, int c] { get => matrix[r, c]; }

        public LevenstainMatrix(string First, string Second)
        {
            RowCurrentIndex = ColumnCurrentIndex = 1;
            FirstWord = First.ToUpper();
            SecondWord = Second.ToUpper();
            matrix = new int?[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
                matrix[i, 0] = i;
            for (int i = 0; i < ColumnCount; i++)
                matrix[0, i] = i;
        }

        public void StepForward()
        {
            if (IsFinished) return;

            if (state)
            {
                matrix[RowCurrentIndex, ColumnCurrentIndex] = Levenstein;
                OnCellChanged?.Invoke(RowCurrentIndex, ColumnCurrentIndex, matrix[RowCurrentIndex, ColumnCurrentIndex]);
            }
            else
            {
                if (ColumnCurrentIndex + 1 == ColumnCount)
                {
                    ColumnCurrentIndex = 1;
                    RowCurrentIndex++;
                }
                else
                    ColumnCurrentIndex++;

                if (IsFinished)
                    OnNewCurrentCell?.Invoke(0, 0);
                else
                    OnNewCurrentCell?.Invoke(RowCurrentIndex, ColumnCurrentIndex);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
            state ^= true;
        }

        public void StepBack()
        {
            if (RowCurrentIndex == 1 && ColumnCurrentIndex == 1 && state) return;

            if (state)
            {
                RowCurrentIndex = ColumnCurrentIndex == 1 ? RowCurrentIndex - 1 : RowCurrentIndex;
                ColumnCurrentIndex = ColumnCurrentIndex == 1 ? ColumnCount - 1 : ColumnCurrentIndex - 1;
                OnNewCurrentCell(RowCurrentIndex, ColumnCurrentIndex);
            }
            else
            {
                matrix[RowCurrentIndex, ColumnCurrentIndex] = null;
                OnCellChanged(RowCurrentIndex, ColumnCurrentIndex, null);
            }
            state ^= true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
        }
    }
}
