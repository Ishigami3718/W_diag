using System.Diagnostics;
using System.Threading;

namespace W_Diag
{
    public partial class Form1 : Form
    {
        static List<Point> p = new List<Point>();
        static List<Color> c = new List<Color>() { Color.Red, Color.Blue, Color.Green, Color.Gold, Color.Gray, Color.Azure,
        Color.Aquamarine,Color.DeepPink,Color.DarkSalmon,Color.Magenta,Color.Brown,Color.Coral,Color.Crimson,Color.SeaGreen};
        int count = 0;
        Color[,] pix;
        Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
            pix = new Color[pictureBox1.Width, pictureBox1.Height];
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
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
            if (x > 2 || x < 0 || y > 1 || y < 0)
            {
                lx = 0;
                rx = pictureBox1.Width;
                ly = 0;
                ry = pictureBox1.Height;
            }
            //Graphics g = pictureBox1.CreateGraphics();
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
                    //g.FillRectangle(new SolidBrush(c[minValIndx % c.Count]), i, j, 1, 1);
                    pix[i, j] = c[minValIndx % c.Count];
                    //bmp.SetPixel(i, j, c[minValIndx % c.Count]);
                }
            }
        }

        private void Filling()
        {
            Graphics g = pictureBox1.CreateGraphics();
            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    //g.FillRectangle(new SolidBrush(pix[i, j]), i, j, 1, 1);
                    bmp.SetPixel(i, j, pix[i,j]);
                }
            }
            pictureBox1.Image = bmp;
            for (int i = 0; i < p.Count; i++)
            {
                g.DrawEllipse(new Pen(Color.Black, 5), p[i].X, p[i].Y, 10, 10);
                g.FillEllipse(new SolidBrush(c[i % c.Count]), p[i].X, p[i].Y, 10, 10);
            }
            /*pictureBox1.Image = bmp;
            for (int i = 0; i < p.Count; i++)
            {
                g.DrawEllipse(new Pen(Color.Black, 5), p[i].X, p[i].Y, 10, 10);
                g.FillEllipse(new SolidBrush(c[i % c.Count]), p[i].X, p[i].Y, 10, 10);
            }*/
        }

        private void PixInBmp()
        {
            /*for(int i = 0;i<pictureBox1.Width;i++)
            {
                for(int j = 0;j < pictureBox1.Height;j++)
                {
                    bmp.SetPixel(i, j, pix[i,j]);
                }
            }*/
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var process = Process.GetCurrentProcess();
            Stopwatch sw = new Stopwatch();
            TimeSpan cpuStart = process.TotalProcessorTime;
            sw.Start();
            Painting(-1,-1);
            Filling();
            sw.Stop();
            TimeSpan cpuEnd = process.TotalProcessorTime;
            TimeSpan cpuUsed = cpuEnd - cpuStart;
            label1.Text = $"Real time:\n{sw.ElapsedMilliseconds}\nCPU time:\n{cpuUsed.TotalMilliseconds}";
            //Filling();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
            p = new List<Point>();
            count = 0;
            pix = new Color[pictureBox1.Width, pictureBox1.Height];
            //bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var process = Process.GetCurrentProcess();
            Stopwatch sw = new Stopwatch();
            TimeSpan cpuStart = process.TotalProcessorTime;
            sw.Start();
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int ii = i;
                    int jj = j;
                  Thread newThread =  new Thread(() => Painting(ii, jj));
                    threads.Add(newThread);
                    newThread.Start();
                }
            }
            foreach (Thread thread in threads) { thread.Join(); }
            Filling();
            sw.Stop();
            TimeSpan cpuEnd = process.TotalProcessorTime;
            TimeSpan cpuUsed = cpuEnd - cpuStart;
            label1.Text = $"Real time:\n{sw.ElapsedMilliseconds}\nCPU time:\n{cpuUsed.TotalMilliseconds}";
            //Filling();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(0, pictureBox1.Width);
                int y = rnd.Next(0, pictureBox1.Height);
                p.Add(new Point(x, y));
                g.DrawEllipse(new Pen(Color.Black, 5), x, y, 10, 10);
                g.FillEllipse(new SolidBrush(c[count % c.Count]), x, y, 10, 10);
                count++;
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
             Color[,] lastPix = (Color[,])pix.Clone();
             pix = new Color[pictureBox1.Width, pictureBox1.Height];
             for(int i = 0;i < lastPix.GetLength(0); i++)
             {
                 for(int j = 0;j < lastPix.GetLength(1); j++)
                 {
                     pix[i, j] = lastPix[i, j];
                 }
             }
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
    }
}