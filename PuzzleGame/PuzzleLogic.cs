using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleGame
{
    public class PuzzleLogic : IPuzzleLogic
    {
        #region Property
        Image image = null;
        int weight;
        int height;
        //-----------------
        int CountOfBlocks = 0;
        int Step = 0;
        int countWeight, countHeight;
        int sizeWeightBlock ;
        int sizeHeightBlock ;
        private PictureBox mainPicture;
        private object[] Tokens;
        private System.Collections.Hashtable TokensTable;
        PictureBox TokensBlocOne;
        PictureBox TokensBlockTwo;

        #endregion

       
        public void Setup(FormStartPosition StartPosition, TabPage tab2,int Width,int Height,Image image)
        {

            SetWidthAndHeight setter = new SetWidthAndHeight(10, 10);
            setter.ShowDialog();
            this.image = image;
            weight = setter.Weight;
            height = setter.Height;
            sizeWeightBlock = tab2.Width / this.weight;
            sizeHeightBlock = tab2.Height / this.height;

            //-----------------------------

            StartPosition = FormStartPosition.WindowsDefaultLocation;
            CountOfBlocks = weight*height;
            countWeight = sizeWeightBlock * weight;
            countHeight = sizeHeightBlock * height;
            Tokens = new object[CountOfBlocks];
            TokensTable = new System.Collections.Hashtable((CountOfBlocks));

            CreateTokens();
            GetTokens();
            SolvePicture(tab2);
            ShufflePicutres(tab2);
        }

        public PuzzleLogic()
        {
        }
        public void CreateTokens()
        {
            int index = 0;
            for (int i = 0; i < Math.Max(weight,height); i++)
            {
                for (int j = 0; j < Math.Min(weight, height); j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(sizeWeightBlock, sizeHeightBlock);
                    if (index == ((CountOfBlocks) - 1))
                    {
                        pic.BorderStyle = BorderStyle.None;
                        pic.BackColor = Color.Transparent;
                    }
                    else
                    {
                        pic.BorderStyle = BorderStyle.FixedSingle;
                        pic.BackColor = Color.Transparent;
                    }
                    pic.Name = string.Format("token{0}", index);
                    pic.Click += new EventHandler(PictureClick);
                    pic.Tag = index;
                    Tokens[index] = pic;
                    index++;
                }
            }
        }
        public void SwapTag(ref PictureBox A, ref PictureBox B)
        {
            object keeptag = null;
            keeptag = TokensTable[A.Tag];
            TokensTable[A.Tag] = TokensTable[B.Tag];
            TokensTable[B.Tag] = keeptag;
            PictureBox pb = new PictureBox();
            pb.Location = A.Location;
            A.Location = B.Location;
            B.Location = pb.Location;
            A.BorderStyle = BorderStyle.FixedSingle;
            B.BorderStyle = BorderStyle.FixedSingle;
        }
        public void PictureClick(object sender, EventArgs e)
        {
            if (Step == 0)
            {
                Step = 1;
                TokensBlocOne = (PictureBox)sender;
                TokensBlocOne.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                TokensBlockTwo = (PictureBox)sender;
                SwapTag(ref TokensBlocOne, ref TokensBlockTwo);
                Step = 0;
                CheckSolve();
            }
        }
        public void CheckSolve()
        {

            bool lose = false;
            for (int i = 0; i < (CountOfBlocks); i++)
            {
                PictureBox tokenpic = (PictureBox)Tokens[i];
                if (Convert.ToInt32(TokensTable[i]) != i)
                    lose = true;
            }
            if (lose == false)
                MessageBox.Show("Congratulations, You Win !");
        }
        public void ShufflePicutres(TabPage tab)
        {
            TokensTable.Clear();
            tab.Controls.Clear();
            int left = 0;
            int top = 0;
            Random rnd = new Random();
            System.Collections.Hashtable gen = new System.Collections.Hashtable((CountOfBlocks));
            for (int i = 0; i < (CountOfBlocks); i++)
            {

                int newtoken = rnd.Next(0, (CountOfBlocks));
                while (gen.Contains(newtoken))
                {
                    newtoken = rnd.Next(0, (CountOfBlocks ));
                }
                PictureBox pic = (PictureBox)Tokens[newtoken];
                pic.Location = new Point(left, top);
                tab.Controls.Add(pic);
                TokensTable.Add(pic.Tag, i);
                gen.Add(newtoken, newtoken);
                left += sizeWeightBlock;
                if ((i + 1) % weight == 0)
                {
                    left = 0;
                    top += sizeHeightBlock;
                }
            }
        }
        public void SolvePicture(TabPage tab)
        {
            TokensTable.Clear();
            tab.Controls.Clear();
            int left = 0;
            int top = 0;
            Random rnd = new Random();
            System.Collections.Hashtable gen = new System.Collections.Hashtable((CountOfBlocks ));
            for (int i = 0; i < (CountOfBlocks ); i++)
            {
                PictureBox pic = (PictureBox)Tokens[i];
                pic.Location = new Point(left, top);
                tab.Controls.Add(pic);
                TokensTable.Add(pic.Tag, i);
                gen.Add(i, i);
                left += sizeWeightBlock;
                if ((i + 1) % weight == 0)
                {
                    left = 0;
                    top += sizeHeightBlock;
                }
            }
        }
        public void GetTokens()
        {
            mainPicture = new PictureBox();
            mainPicture.Size = new Size(countWeight, countHeight);
            mainPicture.Location = new Point(0, 0);
            Image img = image;
            Bitmap bm = new Bitmap(img, countWeight, countHeight);
            mainPicture.Image = bm;
            mainPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            int top = 0;
            int left = 0;
            int k = 0;
            Bitmap bmp = new Bitmap(mainPicture.Image);
            for (int i = 0; i < Math.Max(weight,height); i++)
            {
                for (int j = 0; j < Math.Min(weight, height); j++)
                {
                    PictureBox tokenpic = (PictureBox)Tokens[k];
                    tokenpic.Image = bmp.Clone(new Rectangle(left, top, sizeWeightBlock, sizeHeightBlock), System.Drawing.Imaging.PixelFormat.DontCare);
                    left += sizeWeightBlock;
                    k++;
                }
                left = 0;
                top += sizeHeightBlock;
            }
        }

    }

}
