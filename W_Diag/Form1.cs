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

        private void Painting()
        {
            Graphics g = pictureBox1.CreateGraphics();
            for (int i = 0; i < pictureBox1.Width; i++)
            {
                for (int j = 0; j < pictureBox1.Height; j++)
                {
                    int minValIndx = 0;
                    int minVal = Class1.Distance(p[0], new Point(i, j));
                    for (int k = 1; k < p.Count; k++)
                    {
                        if (Math.Abs(p[k].X - i) < 10 && Math.Abs(p[k].Y - j) < 10) break;
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
            Painting();
        }
    }
}