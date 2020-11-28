﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace frogs2048
{
    public partial class Form1 : Form
    {
        public int[,] map = new int[4, 4];
        public Label[,] labels = new Label[4, 4];
        public PictureBox[,] pics = new PictureBox[4, 4];
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(_keyboardEvent);
            map[0, 0] = 1;
            map[0, 1] = 1;
            createMap();
            createPics();
            generateNewPic();

        }


        private void createMap()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(29 + 53 * j, 106 + 53 * i);
                    pic.BackColor = Color.PaleGreen;
                    pic.Size = new Size(50, 50);
                    this.Controls.Add(pic);
                }
            }
        }

        private void generateNewPic()
        {
            Random rnd = new Random();
            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);
            while (pics[a, b] != null)
            {
                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }

            map[a, b] = 1;
            pics[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(29 + b * 53, 106 + a * 53);
            pics[a, b].BackColor = Color.Lime;
            pics[a, b].Size = new Size(50, 50);
            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }

        private void createPics()
        {
            pics[0, 0] = new PictureBox();
            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(50, 50);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[0, 0].Controls.Add(labels[0, 0]);
            pics[0, 0].Location = new Point(29, 106);
            pics[0, 0].BackColor = Color.Lime;
            pics[0, 0].Size = new Size(50, 50);
            this.Controls.Add(pics[0, 0]);
            pics[0, 0].BringToFront();

            pics[0, 1] = new PictureBox();
            labels[0, 1] = new Label();
            labels[0, 1].Text = "2";
            labels[0, 1].Size = new Size(50, 50);
            labels[0, 1].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 1].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[0, 1].Controls.Add(labels[0, 1]);
            pics[0, 1].Location = new Point(82, 106);
            pics[0, 1].BackColor = Color.Lime;
            pics[0, 1].Size = new Size(50, 50);
            this.Controls.Add(pics[0, 1]);
            pics[0, 1].BringToFront();
        }

        private void changeColor(int sum, int k, int j)
        {
            if (sum % 1024 == 0) pics[k, j].BackColor = Color.LightGreen;
            else if (sum % 512 == 0) pics[k, j].BackColor = Color.GreenYellow;
            else if (sum % 256 == 0) pics[k, j].BackColor = Color.DarkOliveGreen;
            else if (sum % 128 == 0) pics[k, j].BackColor = Color.DarkKhaki;
            else if (sum % 64 == 0) pics[k, j].BackColor = Color.SeaGreen;
            else if (sum % 32 == 0) pics[k, j].BackColor = Color.SpringGreen;
            else if (sum % 16 == 0) pics[k, j].BackColor = Color.LimeGreen;
            else if (sum % 8 == 0) pics[k, j].BackColor = Color.ForestGreen;
            else if (sum % 4 == 0) pics[k, j].BackColor = Color.YellowGreen;
        }
        private void _keyboardEvent(object sender, KeyEventArgs e)
        {
            bool ifPicsMasMoved = false;

            switch (e.KeyCode.ToString())
            {
                case "Right":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 2; l >= 0; l--)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l + 1; j < 4; j++)
                                {


                                    if (map[k, j] == 0)
                                    {
                                        ifPicsMasMoved = true;
                                        map[k, j - 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j - 1];
                                        pics[k, j - 1] = null;
                                        labels[k, j] = labels[k, j - 1];
                                        labels[k, j - 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X + 53, pics[k, j].Location.Y);


                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j - 1].Text);
                                        if (a == b)
                                        {
                                            ifPicsMasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            score += a + b;
                                            changeColor(a + b, k, j);
                                            label1.Text = "Очки: " + score;
                                            map[k, j - 1] = 0;
                                            this.Controls.Remove(pics[k, j - 1]);
                                            this.Controls.Remove(labels[k, j - 1]);
                                            pics[k, j - 1] = null;
                                            labels[k, j - 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                case "Left":
                    for (int k = 0; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l - 1; j >= 0; j--)
                                {
                                    if (map[k, j] == 0)
                                    {
                                        ifPicsMasMoved = true;
                                        map[k, j + 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j + 1];
                                        pics[k, j + 1] = null;
                                        labels[k, j] = labels[k, j + 1];
                                        labels[k, j + 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X - 53, pics[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j + 1].Text);
                                        if (a == b)
                                        {
                                            ifPicsMasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            map[k, j + 1] = 0;
                                            score += a + b;
                                            changeColor(a + b, k, j);
                                            label1.Text = "Очки: " + score;
                                            this.Controls.Remove(pics[k, j + 1]);
                                            this.Controls.Remove(labels[k, j + 1]);
                                            pics[k, j + 1] = null;
                                            labels[k, j + 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                case "Down":
                    for (int k = 2; k >= 0; k--)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k + 1; j < 4; j++)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicsMasMoved = true;
                                        map[j - 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j - 1, l];
                                        pics[j - 1, l] = null;
                                        labels[j, l] = labels[j - 1, l];
                                        labels[j - 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 53);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j - 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicsMasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            map[j - 1, l] = 0;
                                            score += a + b;
                                            changeColor(a + b, j, l);
                                            label1.Text = "Очки: " + score;
                                            this.Controls.Remove(pics[j - 1, l]);
                                            this.Controls.Remove(labels[j - 1, l]);
                                            pics[j - 1, l] = null;
                                            labels[j - 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;

                case "Up":
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k - 1; j >= 0; j--)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicsMasMoved = true;
                                        map[j + 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j + 1, l];
                                        pics[j + 1, l] = null;
                                        labels[j, l] = labels[j + 1, l];
                                        labels[j + 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y - 53);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j + 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicsMasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            map[j + 1, l] = 0;
                                            score += a + b;
                                            changeColor(a + b, j, l);
                                            label1.Text = "Очки: " + score;
                                            this.Controls.Remove(pics[j + 1, l]);
                                            this.Controls.Remove(labels[j + 1, l]);
                                            pics[j + 1, l] = null;
                                            labels[j + 1, l] = null;
                                        }

                                    }

                                }

                            }

                        }

                    }
                    break;

            }
            if (ifPicsMasMoved)
                generateNewPic();
        }
    }
}
