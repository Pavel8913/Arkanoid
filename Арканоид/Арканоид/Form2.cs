using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Арканоид
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            generate_blocks();
        }


        PictureBox[,] pic = new PictureBox[7, 4];
        bool goLeft, goRight = false;
        int platform_speed = 10;
        int score = 0;
        int herox =2;
        int heroy = 2;


        private void restart_game()
        {
            Form2 f = new Form2();
            this.Hide();
            f.Show();
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "Score: " + score;

            if (goLeft == true && platform.Left > 0)
            {
                platform.Left -= platform_speed;
            }

            if (goRight == true && platform.Left < 690)
            {
                platform.Left += platform_speed;
            }

            hero.Left += herox;
            hero.Top += heroy;

            if (hero.Left < 0 || hero.Left > 690)
            {
                herox = -herox;
            }
            if (hero.Top < 0)
            {
                heroy = -heroy;
            }

            if (hero.Bounds.IntersectsWith(platform.Bounds))
            {

                heroy = 3 * -1;

                if (herox < 0)
                {
                    herox = 3 * -1;
                }
                else
                {
                    herox = 3;
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "block")
                {
                    if (hero.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;

                        heroy = -heroy;

                        this.Controls.Remove(x);
                    }
                }
            }

            if (score == 28)
            {
                timer1.Stop();
                MessageBox.Show("Вы выиграли!");
            }
            if (hero.Top > 420)
            {
                timer1.Stop();
                MessageBox.Show("Вы проиграли!" + "\n" + "Попробуйте ещё раз", "Сообщение");
                restart_game();

            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void generate_blocks()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    pic[i, j] = new PictureBox();
                    pic[i, j].Size = new Size(40, 20);
                    pic[i, j].Location = new Point(80 * i + 40, 40 * j + 40);
                    pic[i, j].Tag = "block";
                    pic[i, j].BackColor = Color.White;
                    this.Controls.Add(pic[i, j]);

                }
            }
        }
    }
}
