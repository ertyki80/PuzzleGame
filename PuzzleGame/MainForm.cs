using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public partial class MainForm : MaterialForm
    {
        #region Property
        Image image;
        OpenFileDialog openFile;
        PuzzleLogic puzzleLogic;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void setToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puzzleLogic = new PuzzleLogic();
            puzzleLogic.Setup(StartPosition,tabPage2,this.Width,this.Height,image);
        
        }



        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

      

        private void loadPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (openFile == null)
            {
                openFile = new OpenFileDialog();
                openFile.ShowDialog();
            }

            image = Image.FromFile(openFile.FileName);


        }

        private void getSolvedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puzzleLogic.SolvePicture(tabPage2);
        }
    }
}
