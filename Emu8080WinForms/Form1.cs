using Emu8080;

namespace Emu8080WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Emulator8080 emulator = new Emulator8080(new WFEmulator8080Listener());
            emulator.LoadFileInMemoryAt(0, "C:\\Users\\Manuel DESTOUESSE\\Development\\PERSO\\emu_8080_net\\Emu8080\\invaders.bin");
            emulator.Run();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen p = new Pen(Color.Blue);
            int radius = 200;
            int x = Width / 2;
            int y = Height / 2;


            int first_point1 = (int)(Math.Cos(0) * radius + x);
            int first_point2 = (int)(Math.Sin(0) * radius + y);

            Point p1 = new Point(first_point1, first_point2);
            for (int i = 1; i < 500; i++)
            {
                int dx = (int)(Math.Cos(i) * radius + x);
                int dy = (int)(Math.Sin(i) * radius + y);
                Point p2 = new Point(dx, dy);
                g.DrawLine(p, p1, p2);
                p1 = p2;
            }
        }
    }
}