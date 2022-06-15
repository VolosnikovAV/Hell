using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Helltaker
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        Pen pen = new Pen(Color.Black, 1);
        Graphics gr;
        const int n = 10, h = 49, d = 32, x0 = 5, y0 = 5;
        bool draw = false;
        int Death = 20;
        PictureBox[,] map = new PictureBox[n, n];
        int[,] kord ={{0,0,0,0,4,0,0,0,0,0},{ 0,3,0,0,1,0,0,0,3,4},
                            { 0,1,0,1,1,0,3,1,1,0},{ 0,1,1,1,0,0,0,0,1,0},
                            { 0,0,0,3,0,0,0,1,1,0},{ 0,1,1,1,1,0,1,1,0,0},
                            { 0,3,0,0,1,3,1,0,0,0},{ 0,1,1,0,0,1,0,0,1,0},
                            { 0,0,1,1,0,1,1,3,1,0},{ 0,0,0,2,0,0,0,0,0,0}};
        Image Road = new Bitmap(@"D:\Helltaker\road.png");
        Image Player = new Bitmap(@"D:\Helltaker\maxresdefault.png");
        Image Wall = new Bitmap(@"D:\Helltaker\wall.png");
        Image Surpr = new Bitmap(@"D:\Helltaker\prize.png");
        string[] Crystal = { @"D:\Helltaker\maxresdefault.jpg" };
        int Strok = 9, Stolb = 3;
        String line;
        
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)//Тут я хожу
        {
            DialogResult Dr = DialogResult.None;
            // int[,] kord = new int[n, n];
            if (Death > 0)
            {
                if (e.KeyCode == Keys.W)//вверх
                    if (Strok > 0)
                    {
                        if (kord[Strok - 1, Stolb] == 4)
                        {
                            kord[Strok - 1, Stolb] = 2;
                            kord[Strok, Stolb] = 1;
                            Strok--;
                            Stolb = Stolb;
                            Death--;
                            if (kord[Strok, Stolb] == 2)
                            {
                                Dr = MessageBox.Show("Победа!\nХотите начать игру заново?", "Победа",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                        }
                        if ((Strok - 2 != 0/*провверкка на наличии стены*/) && (kord[Strok - 1, Stolb] == 0))
                        {
                            if ((Strok - 2 > -1) && (kord[Strok - 2, Stolb] != 0))
                            {
                                kord[Strok - 2, Stolb] = 0;
                                kord[Strok - 1, Stolb] = 2;
                                kord[Strok, Stolb] = 1;
                                Strok--;
                                Stolb = Stolb;
                                Death--;
                            }

                        }
                        else if (kord[Strok - 1, Stolb] != 0)
                        {
                            kord[Strok - 1, Stolb] = 2;
                            kord[Strok, Stolb] = 1;
                            Strok--;
                            Stolb = Stolb;
                            Death--;
                        }
                    }


                if (e.KeyCode == Keys.S)//вниз
                    if (Strok < n - 1)
                    {
                        if ((Strok + 2 != n/*провверкка на наличии стены*/) && (kord[Strok + 1, Stolb] == 0))
                        {
                            if ((Strok + 2 < n) && (kord[Strok + 2, Stolb] != 0))
                            {
                                kord[Strok + 2, Stolb] = 0;
                                kord[Strok + 1, Stolb] = 2;
                                kord[Strok, Stolb] = 1;
                                Strok++;
                                Stolb = Stolb;
                                Death--;
                            }
                        }
                        else if (kord[Strok + 1, Stolb] != 0)
                        {
                            kord[Strok + 1, Stolb] = 2;
                            kord[Strok, Stolb] = 1;
                            Strok++;
                            Stolb = Stolb;
                            Death--;
                        }
                    }
                if (e.KeyCode == Keys.A)//Влево
                    if (Stolb > 0)
                    {
                        if ((Stolb - 2 != 0/*провверкка на наличии стены*/) && (kord[Strok, Stolb - 1] == 0))
                        {
                            if ((Stolb - 2 > -1) && (kord[Strok, Stolb - 2] != 0))
                            {
                                kord[Strok, Stolb - 2] = 0;
                                kord[Strok, Stolb - 1] = 2;
                                kord[Strok, Stolb] = 1;
                                Stolb--;
                                Death--;
                            }

                        }
                        else if (kord[Strok, Stolb - 1] != 0)
                        {
                            kord[Strok, Stolb - 1] = 2;
                            kord[Strok, Stolb] = 1;
                            Stolb--;
                            Death--;
                        }
                    }
                if (e.KeyCode == Keys.D)//Влево
                    if (Stolb < n - 1)
                    {
                        if ((Stolb + 2 != 0) && (kord[Strok, Stolb + 1] == 0))
                        {
                            if ((Stolb + 2 < n) && (kord[Strok, Stolb + 2] != 0))
                            {
                                kord[Strok, Stolb + 2] = 0;
                                kord[Strok, Stolb + 1] = 2;
                                kord[Strok, Stolb] = 1;
                                Stolb++;
                                Death--;
                            }

                        }
                        else if (kord[Strok, Stolb + 1] != 0)
                        {
                            kord[Strok, Stolb + 1] = 2;
                            kord[Strok, Stolb] = 1;
                            Stolb++;
                            Death--;
                        }
                    }
                draw_map(n, d, Crystal);
            }
            else
            {
                
                Dr = MessageBox.Show("Попроирыш!\nХотите начать игру заново?", "Проигрыш",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if(Dr == DialogResult.Yes)
            {
                int[,] kord1 ={{0,0,0,0,4,0,0,0,0,0},{ 0,3,0,0,1,0,0,0,3,4},
                            { 0,1,0,1,1,0,3,1,1,0},{ 0,1,1,1,0,0,0,0,1,0},
                            { 0,0,0,3,0,0,0,1,1,0},{ 0,1,1,1,1,0,1,1,0,0},
                            { 0,3,0,0,1,3,1,0,0,0},{ 0,1,1,0,0,1,0,0,1,0},
                            { 0,0,1,1,0,1,1,3,1,0},{ 0,0,0,2,0,0,0,0,0,0}};
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        kord[i, j] = kord1[i, j];

                Death = 20;
                draw_map(n, d, Crystal);
                Strok = 9;
                Stolb = 3;
            }
            if (Dr == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            draw_player(n, x0, y0);
            draw_map(n, d, Crystal);
            button1.Enabled = false;
           /* for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    map[i, j].Visible = false;
                }
            }*/
        }

        SolidBrush MyBrush = new SolidBrush(Color.White);
        Pen MyPen = new Pen(Color.Black, 1);
        public Form1()
        {
            InitializeComponent();
            gr = this.CreateGraphics();
            this.Focus();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    map[i, j] = new PictureBox();
                    map[i, j].Left = x0 + j * h;
                    map[i, j].Top = y0 + i * h;
                    map[i, j].Width = h;
                    map[i, j].Height = h;
                    map[i, j].Visible = true;
                    map[i, j].BorderStyle = BorderStyle.Fixed3D;
                    this.Controls.Add(map[i, j]);
                    this.Controls.Add(map[i, j]);
                    draw_map(n, d, Crystal);
                }
            }
          
            
            //ask.AddRange(File.ReadAllLines(@"G:\c\Viktorina\" + n.ToString() + ".txt", Encoding.UTF8));
            for (int i = 0; i < n; i++)
            {
                //onestring = ask[i].Split(' ');
                for (int j = 0; j < n; j++)
                {
                    kord[i, j] = Convert.ToInt32(onestring[j]);
                }
            }*/
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void cleararray(int[,] a, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = 1;
                }
            }
        }
        public void draw_map(int n, int w, string[] str)
        {
          //int[,] kord={{0,0,0,0,4,0,0,0,0,0},{ 0,3,0,0,1,0,0,0,3,4},
          //                  { 0,1,0,1,1,0,3,1,1,0},{ 0,1,1,1,0,0,0,0,1,0},
          //                  { 0,0,0,3,0,0,0,1,1,0},{ 0,1,1,1,1,0,1,1,0,0},
          //                  { 0,3,0,0,1,3,1,0,0,0},{ 0,1,1,0,0,1,0,0,1,0},
          //                  { 0,0,1,1,0,1,1,3,1,0},{ 0,0,0,2,0,0,0,0,0,0}};
            gr.Clear(this.BackColor);
            int  x, y;
            for (int i = 0; i < n; i++)
            {
                y = y0 + i * w;
                for (int j = 0; j < n; j++)
                {
                    x = x0 + j * w;
                    if (kord[i,j]==2)
                    {
                    gr.DrawImage(Player, x, y, w, w);
                    }
                    if (kord[i, j] == 3)
                    {
                        gr.DrawImage(Surpr, x, y, w, w);
                    }
                    if (kord[i,j]==0)
                    {
                        gr.DrawImage(Wall, x, y, w, w);
                    }
                    if (kord[i,j]==1)
                        gr.DrawImage(Road, x, y, w, w);
                    gr.DrawRectangle(pen, x, y, w, w);
                   
                }
            }
            /*
            int x, y;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (kord[i, j] == 1)
                        map[i, j].Image = Player;
            */
        }
        public void draw_cell(int i, int k, Color c1)
        {
            int x, y;
            MyBrush.Color = c1;
            x = x0 + k * h;
            y = y0 + i * h;
            gr.FillRectangle(MyBrush, x, y, h, h);
            gr.DrawRectangle(MyPen, x, y, h, h);

        }

        public void draw_player(int n, int x0, int y0)
        {
            /*
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    kord[i, j] = 0;
                }
            }
            //kord[Strok,Stolb] = 1;
            */
        }
    }
}
