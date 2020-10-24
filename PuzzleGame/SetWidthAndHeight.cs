using MaterialSkin.Controls;
using System;

namespace PuzzleGame
{
    public partial class SetWidthAndHeight : MaterialForm
    {
        public int Weight = 10;
        public int Height = 10;

        public SetWidthAndHeight(int weight,int height)
        {
            Weight = weight;
            Height = height;

            InitializeComponent();
        }

        private void SetWidthAndHeight_Load(object sender, EventArgs e)
        {
            textBox1.Text = Weight.ToString();
            textBox2.Text = Height.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Weight = Convert.ToInt32(textBox1.Text);
            Height = Convert.ToInt32(textBox2.Text);
            this.Close();
        }

    }
}
