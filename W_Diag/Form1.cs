namespace W_Diag
{
    public partial class Form1 : Form
    {
        static List<Point> p = new List<Point>();
        static List<Color> c = new List<Color>() {Color.Red,Color.Blue,Color.Green,Color.Gold,Color.Gray,Color.Azure};
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
            g.DrawEllipse(new Pen(Color.Black,5), x, y, 10, 10);
            g.FillEllipse(new SolidBrush(c[count%c.Count]), x, y, 10, 10);
            count++;
            p.Add(new Point(x,y));
        }
    }
}