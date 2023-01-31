using Emu8080;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Emu8080WinForms
{
    public partial class Form1 : Form
    {
        private Emulator8080 emulator;
        private Thread trd;
        private Bitmap bmap;

        public Form1()
        {
            InitializeComponent();
            trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();
        }

        
        private void ThreadTask()
        {
            emulator = new Emulator8080(new Form1.InnerListener(this));
            emulator.LoadFileInMemoryAt(0, "C:\\Users\\Manuel DESTOUESSE\\Development\\PERSO\\emu_8080_net\\Emu8080\\invaders.bin");
            emulator.Run();
        }

        public class InnerListener : IEmulator8080Listener
        {
            private Form1 form;
            private Bitmap bmap;

            public InnerListener(Form1 form)
            {
                this.form = form;
            }
            public void UnimplmentedOperationCode(OpCode8080 opCode)
            {
                Console.WriteLine(" ===> operation code not implemented :" + opCode.code.ToString("x2"));
                throw new InvalidOperationException("Operation code not implemented:" + opCode.code.ToString("x2"));
            }

            public void DisplayInstruction(int pc, OpCode8080 opCode, byte param1, byte param2)
            {

            }

            public void DisplayRegister(Cpu8080 cpu)
            {
                
            }

            public void DisplayStatus(Status8080 status)
            {
                
            }

            public void InterruptGenerated()
            {

            }

            public void FrameBufferRefresh(byte[] frameBuffer)
            {
                this.form.pictureBox1.Invoke((MethodInvoker)(() => {
                    var ram = frameBuffer;
                    Array.Reverse(ram);
                    GCHandle handle = GCHandle.Alloc(ram, GCHandleType.Pinned);
                    IntPtr ptr = Marshal.UnsafeAddrOfPinnedArrayElement(ram, 0);
                    bmap = new Bitmap(256, 224, 32, PixelFormat.Format1bppIndexed, ptr);
                    handle.Free();
                    bmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    bmap = new Bitmap(bmap,1024, 896);
                    this.form.pictureBox1.Image = bmap;
                    this.form.pictureBox1.Refresh();
                }));
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void form1_keydown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.C:
                    emulator.KeyDown(Keys8080.KEY_COIN);
                    break;
                case Keys.S:
                    emulator.KeyDown(Keys8080.KEY_P1_START);
                    break;
                case Keys.A:
                    emulator.KeyDown(Keys8080.KEY_P1_LEFT);
                    break;
                case Keys.D:
                    emulator.KeyDown(Keys8080.KEY_P1_RIGHT);
                    break;
                case Keys.Space:
                    emulator.KeyDown(Keys8080.KEY_P1_FIRE);
                    break;
            }
        }

        private void form1_keyup(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.C:
                    emulator.KeyUp(Keys8080.KEY_COIN);
                    break;
                case Keys.S:
                    emulator.KeyUp(Keys8080.KEY_P1_START);
                    break;
                case Keys.A:
                    emulator.KeyUp(Keys8080.KEY_P1_LEFT);
                    break;
                case Keys.D:
                    emulator.KeyUp(Keys8080.KEY_P1_RIGHT);
                    break;
                case Keys.Space:
                    emulator.KeyUp(Keys8080.KEY_P1_FIRE);
                    break;
            }


        }
    }
}