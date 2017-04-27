using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingProject
{
    public partial class Form1 : Form
    {
        public double[,] stars = new double[108, 10]; //0 - X, 1 - Y, 2 - класс звезды(7-1 O B A F G K M), 3 - масса, 4 - радиус, 5 - T(кельвины), 6 - максимальный радиус обитаемой зоны, 7 - минимальный радиус обитаемый, 8 - кол-во свзяей, 9 - светимость
        public double[,] svyazi = new double[324, 3]; //Группы звёзд
        public int select_star = 0, current_star = 0, current_map_menu = 1;
        public Bitmap starmap = new Bitmap(640, 480);
        public Bitmap strelki = new Bitmap(640, 480);
        public Image point = Image.FromFile("Point.png");

        public void img_updt()
        {
            Bitmap all = new Bitmap(640, 480);
            Graphics g = Graphics.FromImage(all);
            g.DrawImage(starmap, 0, 0);
            g.DrawImage(strelki, 0, 0);
            pictureBox1.Image = all;
            pictureBox1.Refresh();
        }

        public void rachet(double max_m, double min_m, double max_r, double min_r, double max_t)
        {
            Random rand = new Random();
                double mass, radius, tem, max_zl,zl , min_zl, br = 0;
                if (stars[current_star, 2] == 7)
                {
                    mass = Convert.ToDouble(rand.Next(180000001, 600000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(70000001, 150000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(300000001, 600000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(200000001, 1400000000)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
        }

        public string label7_text_star(int i)
        {
            string klass = "";
            if (stars[i, 2] == 1)
            {
                klass = "M";
            }
            else if (stars[i, 2] == 2)
            {
                klass = "K";
            }
            else if (stars[i, 2] == 3)
            {
                klass = "G";
            }
            else if (stars[i, 2] == 4)
            {
                klass = "F";
            }
            else if (stars[i, 2] == 5)
            {
                klass = "A";
            }
            else if (stars[i, 2] == 6)
            {
                klass = "B";
            }
            else
            {
                klass = "O";
            }
            return klass;
        }
        public double[] ways()
        {
            double[] ways = new double[324];
            for(int i = 0; i <324; i++)
            {
                if (svyazi[i, 0] == current_star)
                {
                    ways[i] = 1;
                }
                else if (svyazi[i, 1] == current_star)
                {
                    ways[i] = 1;
                }
                else if (svyazi[i, 2] == current_star)
                {
                    ways[i] = 1;
                }
            }
            return ways;
        }
        public int[] stars_around()
        {
            double[] ways1 = ways();
            int[] around_stars_all = new int[ways1.Length*3];
            int b = 1;
            for (int i = 0; i < ways1.Length; i++)
            {
                if(ways1[i] == 1)
                {
                        if (svyazi[i, 0] != current_star && b < around_stars_all.Length)
                        {
                            around_stars_all[b] = Convert.ToInt32(svyazi[i, 0]);
                            b++;

                        }
                        if (svyazi[i, 1] != current_star  && b < around_stars_all.Length)
                        {
                            around_stars_all[b] = Convert.ToInt32(svyazi[i, 1]);
                            b++;
                        }
                        if (svyazi[i, 2] != current_star && b < around_stars_all.Length)
                        {
                            around_stars_all[b] = Convert.ToInt32(svyazi[i, 2]);
                            b++;
                        }
                    
                }
            }
            around_stars_all[(ways1.Length * 3)-1] = current_star;
            Array.Sort(around_stars_all);
            int numb = 0;
            int[] arnd_strs_cent = new int[around_stars_all.Length];
            for(int i = 0; i < ways1.Length * 3; i++)
            {
                if(around_stars_all[i] != 0)
                {
                    if(around_stars_all[i] >=2 && around_stars_all[i-1] != around_stars_all[i])
                    {
                        arnd_strs_cent[numb] = around_stars_all[i];
                        numb++;
                    }
                }
            }
            int[] around_stars = new int[numb+2];
            for(int i = 1; i < around_stars.Length-1; i++)
            {
                around_stars[i] = arnd_strs_cent[i-1];
            }
            around_stars[0] = 123456;
            around_stars[around_stars.Length - 1] = 654321;
            return around_stars;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(starmap);
            Graphics g1 = Graphics.FromImage(strelki);
            Random rand = new Random();
            g.Clear(Color.Black);
            int start = 0; current_map_menu = 1;
            Pen whitee = new Pen(System.Drawing.ColorTranslator.FromHtml("#2E2E2E"), 1);
            for (int i = 0; i < 107; i++)
            {
                stars[i, 0] = rand.Next(1, 639);
                stars[i, 1] = rand.Next(1, 479);
            }
            for (int i = 0; i < 108; i++)
            {
                double[] distance = new double[108];
                svyazi[i, 0] = i;
                distance[i] = 99999999999;
                double n1 = 0, n2 = 0;
                for (int b = 0; b < 108; b++)
                {
                    if(b != i)
                    {
                        distance[b] = Math.Sqrt(Math.Pow(stars[i, 0] - stars[b, 0], 2) + Math.Pow(stars[i, 1] - stars[b, 1], 2));
                    }
                }
                for (int c = 0; c < 3; c++)
                {
                    for (int k = 0; k < 108; k++)
                    {
                        if (distance[k] == distance.Min())
                        {
                            if (n1 == 0)
                            {
                                n1 = k;
                                svyazi[i, 1] = k;
                                distance[k] = 999999999;

                            }
                            else if (n2 == 0)
                            {
                                n2 = k;
                                svyazi[i, 2] = k;
                                distance[k] = 999999999;
                            }
                        }
                        
                    }
                }
                g.DrawLine(whitee, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), Convert.ToInt32(stars[Convert.ToInt32(n1), 0]), Convert.ToInt32(stars[Convert.ToInt32(n1), 1]));
                g.DrawLine(whitee, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), Convert.ToInt32(stars[Convert.ToInt32(n2), 0]), Convert.ToInt32(stars[Convert.ToInt32(n2), 1]));
                double klass = Convert.ToDouble(rand.Next(1000000000)) / 10000000;
                if (klass <= 76.4563)
                {
                    stars[i, 2] = 1;
                    Pen M = new Pen(System.Drawing.ColorTranslator.FromHtml("#ff6060"), 1);
                    g.DrawRectangle(M, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), 1, 1);
                }
                else if (klass > 76.4563)
                {
                    if (klass <= 88.5922)
                    {
                        stars[i, 2] = 2;
                        Pen K = new Pen(System.Drawing.ColorTranslator.FromHtml("#ffc46f"), 1);
                        g.DrawRectangle(K, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), 1, 1);
                    }
                    else
                    {
                        if (klass <= 96.2378)
                        {
                            stars[i, 2] = 3;
                            Pen G = new Pen(System.Drawing.ColorTranslator.FromHtml("#fff2a1"), 1);
                            g.DrawRectangle(G, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), 1, 1);
                        }
                        else
                        {
                            if (klass <= 99.27178)
                            {
                                stars[i, 2] = 4;
                                Pen F = new Pen(System.Drawing.ColorTranslator.FromHtml("#fff4ea"), 1);
                                g.DrawRectangle(F, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), 1, 1);
                            }
                            else
                            {
                                if (klass <= 99.87858)
                                {
                                    stars[i, 2] = 5;
                                    Pen A = new Pen(System.Drawing.ColorTranslator.FromHtml("#f8f7ff"), 1);
                                    g.DrawRectangle(A, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), 1, 1);
                                }
                                else
                                {
                                    if (klass <= 99.99998)
                                    {
                                        stars[i, 2] = 6;
                                        Pen B = new Pen(System.Drawing.ColorTranslator.FromHtml("#cad7ff"), 1);
                                        g.DrawRectangle(B, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), 1, 1);
                                    }
                                    else
                                    {
                                        stars[i, 2] = 7;
                                        Pen O = new Pen(System.Drawing.ColorTranslator.FromHtml("#9aafff"), 1);
                                        g.DrawRectangle(O, Convert.ToInt32(stars[i, 0]), Convert.ToInt32(stars[i, 1]), 1, 1);
                                    }
                                }
                            }
                        }
                    }

                }
                n1 = 0;
                n2 = 0;
            }
            start = rand.Next(0, 107);
            select_star = start;
            current_star = start;
            g1.Clear(Color.FromArgb(0, 0, 0, 0));
            g1.DrawImage(point, Convert.ToInt32(stars[start, 0]-4), Convert.ToInt32(stars[start, 1])-16);
            label7.Text = label7_text_star(start); label8.Text = "???"; label9.Text = "???"; label10.Text = "???"; label11.Text = "???"; label12.Text = "???";
            previous_button.Enabled = true;
            next_button.Enabled = true;
            select_button.Enabled = true;
            current_button.Enabled = true;
            img_updt();
        }

        private void previous_button_Click(object sender, EventArgs e)
        {
            if (current_map_menu == 1)
            {
                int[] around_stars = stars_around();
                int star = 0;
                Graphics g = Graphics.FromImage(strelki);
                g.Clear(Color.FromArgb(0, 0, 0, 0));
                int number_of_selected_star = 0;
                for(int i = 0; i < around_stars.Length; i++)
                {
                    if(around_stars[i] == select_star)
                    {
                        number_of_selected_star = i;
                        i = 9999;
                    }
                }
                if(number_of_selected_star == 1)
                {
                    select_star = Convert.ToInt32(around_stars[around_stars.Length-2]);
                }
                else
                {
                    select_star = Convert.ToInt32(around_stars[number_of_selected_star - 1]);
                }
                star = select_star;
                g.DrawImage(point, Convert.ToInt32(stars[select_star, 0] - 4), Convert.ToInt32(stars[select_star, 1]) - 16);
                label7.Text = label7_text_star(select_star); label8.Text = "???"; label9.Text = "???"; label10.Text = "???"; label11.Text = "???"; label12.Text = "???";
                img_updt();
            }
        }

        private void current_button_Click(object sender, EventArgs e)
        {
            if (current_map_menu == 1)
            {
                Graphics g = Graphics.FromImage(strelki);
                g.Clear(Color.FromArgb(0, 0, 0, 0));
                select_star = current_star;
                g.DrawImage(point, Convert.ToInt32(stars[select_star, 0] - 4), Convert.ToInt32(stars[select_star, 1]) - 16);
                label7.Text = label7_text_star(select_star); label8.Text = "???"; label9.Text = "???"; label10.Text = "???"; label11.Text = "???"; label12.Text = "???";
                img_updt();
            }
        }

        private void next_button_Click(object sender, EventArgs e)
        {
            if (current_map_menu == 1)
            {
                int[] around_stars = stars_around();
                int star = 0;
                Graphics g = Graphics.FromImage(strelki);
                g.Clear(Color.FromArgb(0, 0, 0, 0));
                int number_of_selected_star = 0;
                for (int i = 0; i < around_stars.Length; i++)
                {
                    if (around_stars[i] == select_star)
                    {
                        number_of_selected_star = i;
                        i = 9999;
                    }
                }
                if (around_stars[number_of_selected_star + 1] == 654321)
                {
                    select_star = Convert.ToInt32(around_stars[1]);
                }
                else
                {
                    select_star = Convert.ToInt32(around_stars[number_of_selected_star + 1]);
                }
                star = select_star;
                g.DrawImage(point, Convert.ToInt32(stars[select_star, 0] - 4), Convert.ToInt32(stars[select_star, 1]) - 16);
                label7.Text = label7_text_star(select_star); label8.Text = "???"; label9.Text = "???"; label10.Text = "???"; label11.Text = "???"; label12.Text = "???";
                img_updt();
            }
        }

        private void select_button_Click(object sender, EventArgs e)
        {
            if (current_map_menu == 1)
            {
                current_star = select_star;
                Graphics g = Graphics.FromImage(strelki);
                g.Clear(Color.FromArgb(0, 0, 0, 0));
                g.DrawImage(point, Convert.ToInt32(stars[select_star, 0] - 4), Convert.ToInt32(stars[select_star, 1]) - 16);
                label7.Text = label7_text_star(select_star); label8.Text = "???"; label9.Text = "???"; label10.Text = "???"; label11.Text = "???"; label12.Text = "???";
                img_updt();
            }
        }

        private void go_button_Click(object sender, EventArgs e)
        {
            if (current_map_menu == 1)
            {
                Random rand = new Random();
                double mass, radius, tem, max_zl,zl , min_zl, br = 0;
                if (stars[current_star, 2] == 7)
                {
                    mass = Convert.ToDouble(rand.Next(180000001, 600000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(70000001, 150000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(300000001, 600000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(200000001, 1400000000)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
                }
                else if(stars[current_star, 2] == 6)
                {
                    mass = Convert.ToDouble(rand.Next(31000001, 180000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(21000001, 70000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(100000001, 300000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(800001, 200000000)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
                }
                else if(stars[current_star, 2] == 5)
                {
                    mass = Convert.ToDouble(rand.Next(17000001, 31000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(13000001, 21000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(75000001, 100000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(60001, 800000)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
                }
                else if(stars[current_star, 2] == 4)
                {
                    mass = Convert.ToDouble(rand.Next(11000001, 17000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(11000001, 13000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(60000001, 75000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(12001, 60000)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
                }
                else if(stars[current_star, 2] == 3)
                {
                    mass = Convert.ToDouble(rand.Next(8000001, 11000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(9000001, 11000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(50000001, 60000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(4001, 12000)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
                }
                else if(stars[current_star, 2] == 2)
                {
                    mass = Convert.ToDouble(rand.Next(3000001, 8000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(4000001, 9000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(35000001, 50000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(401, 4000)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
                }
                else if(stars[current_star, 2] == 1)
                {
                    mass = Convert.ToDouble(rand.Next(1000001, 3000000)) / 10000000;
                    radius = Convert.ToDouble(rand.Next(1000001, 4000000)) / 10000000;
                    tem = Convert.ToDouble(rand.Next(20000001, 35000000)) / 10000;
                    br = Convert.ToDouble(rand.Next(101, 400)) / 1000;
                    zl = Math.Sqrt(br);
                    max_zl = zl * 1.15;
                    min_zl = zl * 0.85;
                    stars[current_star, 3] = mass;
                    stars[current_star, 4] = radius;
                    stars[current_star, 5] = tem;
                    stars[current_star, 6] = max_zl;
                    stars[current_star, 7] = min_zl;
                    stars[current_star, 9] = br;
                    label8.Text = Convert.ToString(mass);
                    label9.Text = Convert.ToString(radius);
                    label10.Text = Convert.ToString(tem);
                    label11.Text = Convert.ToString(min_zl);
                    label12.Text = Convert.ToString(max_zl);
                }
            }
        }
    }
}
