namespace W_Diag
{
    public partial class Form1 : Form
    {
        static List<Point> p = new List<Point>();
        static List<Color> c = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Gold, Color.Gray, Color.Azure };
        int count = 0;
        public Form1()
        {
            InitializeComponent();
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Graphics g = pictureBox1.CreateGraphics();
            g.DrawEllipse(new Pen(Color.Black, 5), x, y, 10, 10);
            g.FillEllipse(new SolidBrush(c[count % c.Count]), x, y, 10, 10);
            count++;
            p.Add(new Point(x, y));
        }

        private void Painting(int x, int y)
        {
            int stepX = pictureBox1.Width / 3;
            int stepY = pictureBox1.Height / 2;
            int lx = x * stepX;
            int rx = (x + 1) * stepX;
            int ly = y * stepY;
            int ry = (y + 1) * stepY;
            if (x>2 || x<0 || y>1 || y<0)
            {
                lx = 0;
                rx = pictureBox1.Width;
                ly = 0;
                ry = pictureBox1.Height;
            }
            Graphics g = pictureBox1.CreateGraphics();
            for (int i = lx; i < rx; i++)
            {
                for (int j = ly; j < ry; j++)
                {
                    int minValIndx = 0;
                    int minVal = Class1.Distance(p[0], new Point(i, j));
                    for (int k = 1; k < p.Count; k++)
                    {
                        //if (Math.Abs(p[k].X - i) < 10 && Math.Abs(p[k].Y - j) < 10) break;
                        int newVal = Class1.Distance(p[k], new Point(i, j));
                        if (newVal < minVal)
                        {
                            minVal = newVal;
                            minValIndx = k;
                        }
                    }
                    g.FillRectangle(new SolidBrush(c[minValIndx % c.Count]), i, j, 1, 1);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Painting(-1, -1);
            for (int i = 0; i < p.Count; i++)
            {
                g.DrawEllipse(new Pen(Color.Black, 5), p[i].X, p[i].Y, 10, 10);
                g.FillEllipse(new SolidBrush(c[i % c.Count]), p[i].X, p[i].Y, 10, 10);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            p = new List<Point>();
            count = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0;i<3;i++)
            {
                for(int j = 0;j<2;j++)
                {
                    new Task(()=>Painting(i,j)).Start();
                }
            }
            Graphics g = pictureBox1.CreateGraphics();
            for (int i = 0; i < p.Count; i++)
            {
                g.DrawEllipse(new Pen(Color.Black, 5), p[i].X, p[i].Y, 10, 10);
                g.FillEllipse(new SolidBrush(c[i % c.Count]), p[i].X, p[i].Y, 10, 10);
            }
        }
    }
}