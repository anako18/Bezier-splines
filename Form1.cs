using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bezier
{
    public partial class Form1 : Form
    {
        Graphics g;

        Font drawFont = new Font("Arial", 10);
        SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
        Pen pen = new Pen(Color.Black);

        bool moving = false;

       //List<Point> points;

        Dictionary<char, Point> points;

        char c = 'A';

        public Form1()
        {
            InitializeComponent();

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(bmp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            points = new Dictionary<char, Point>();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Visible = false;
            if (moving)
            {
                points[(char)listBox1.SelectedItem] = new Point(e.X, e.Y);
                redraw();
                moving = false;
            }
            else
            {
                g.DrawString(c.ToString(), drawFont, drawBrush, e.X, e.Y);
                g.DrawEllipse(pen, new Rectangle(e.X, e.Y, 1, 1));
                points.Add(c, new Point(e.X, e.Y));
                listBox1.Items.Add(c);
                c++;
                pictureBox1.Refresh();
            }
        }

        //draw bezier
        private void button1_Click(object sender, EventArgs e)
        {
            if (points.Count < 4)
            {
                label1.Visible = true;
                return;
            } 
            g.DrawBezier(pen, points.Values.ElementAt(0), points.Values.ElementAt(1), points.Values.ElementAt(2), points.Values.ElementAt(3));
            pictureBox1.Refresh();
        }

        //delete point
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
                return;
            points.Remove((char)listBox1.SelectedItem);
            listBox1.Items.Remove(listBox1.SelectedItem);
            redraw();
           // listBox1.SelectedItem
        }

        //redraw
        private void redraw()
        {
            g.Clear(System.Drawing.Color.White);
            
            for (int i = 0; i<points.Count; i++)
            {
                g.DrawEllipse(pen, new Rectangle(points.Values.ElementAt(i).X, points.Values.ElementAt(i).Y, 1, 1));
                g.DrawString(points.Keys.ElementAt(i).ToString(), drawFont, drawBrush, points.Values.ElementAt(i).X, points.Values.ElementAt(i).Y);
            }
            pictureBox1.Refresh();
        }

        //move
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
                return;
            moving = true;
        }

        //clear
        private void button4_Click(object sender, EventArgs e)
        {
            points.Clear();
            listBox1.Items.Clear();
            g.Clear(System.Drawing.Color.White);
            pictureBox1.Refresh();
        }
    }
}
