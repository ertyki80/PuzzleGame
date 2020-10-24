using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public interface IPuzzleLogic
    {
        void CreateTokens();
        void PictureClick(object sender, EventArgs e);
        void CheckSolve();
        void SwapTag(ref PictureBox A, ref PictureBox B);
        void ShufflePicutres(TabPage tab);
        void SolvePicture(TabPage tab);
    }
}
