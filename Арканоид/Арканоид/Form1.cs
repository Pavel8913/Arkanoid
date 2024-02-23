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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
            generate_blocks();
        }

        PictureBox[,] pic =new PictureBox[5,3];
        bool goLeft, goRight = false;
        int  platform_speed = 10;    
        int score = 0;
        int herox = 2;
        int heroy = 2;
        private void generate_blocks()
        {
            for(int i=0;i<5;i++)
            {
                for(int j=0;j<3;j++)
                {
                    pic[i, j] = new PictureBox();
                    pic[i, j].Size = new Size(40,20);
                    pic[i, j].Location = new Point(80*i+40,40*j+40);
                    pic[i, j].Tag = "block";                    
                    pic[i,j].BackColor= Color.White;
                    this.Controls.Add(pic[i, j]);

                }
            }
        }

        

        private void restart_game()
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "Score: " + score;

            if (goLeft == true && platform.Left > 0)
            {
                platform.Left -= platform_speed;
            }

            if (goRight == true && platform.Left < 440)
            {
                platform.Left += platform_speed;
            }

            hero.Left += herox;
            hero.Top += heroy;

            if (hero.Left < 0 || hero.Left > 440)
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

            if (score == 15)
            {
                timer1.Stop();
                MessageBox.Show("Вы выиграли!");      
                Form2 form2= new Form2();
                this.Hide();
                form2.Show();
            }
            if (hero.Top > 335)
            {
                timer1.Stop();
                MessageBox.Show("Вы проиграли!" + "\n" + "Попробуйте ещё раз", "Сообщение");
                restart_game();
                
            }
        }
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
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
    }
}
