using Emu8080;
using System;
using System.Threading;
using System.Reflection.Emit;
using Xamarin.Forms;
using System.IO;

namespace Emu8080XamarinForms
{
	public class MainPageViewModel : BaseViewModel
	{
        private Emulator8080 emulator;
        private Thread trd;

        public byte[] _FrameBuffer;
        public byte[] FrameBuffer
        {
            get
            {
                return _FrameBuffer;
            }
            set
            {
                if (_FrameBuffer != value)
                {
                    _FrameBuffer = value;
                    OnPropertyChanged("FrameBuffer");
                }
            }
        }


        public MainPageViewModel()
		{
            Title = "Space Invaders";
            
		}

        public void startEmulator()
        {
            trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();
        }

        private void ThreadTask()
        {
            emulator = new Emulator8080(new MainPageViewModel.InnerListener(this));
            emulator.LoadBufferInMemoryAt(0, App.ByteFile.getBuffer());
            emulator.Run();
        }


        public class InnerListener : IEmulator8080Listener
        {
            MainPageViewModel ViewModel;

            public InnerListener(MainPageViewModel viewModel)
            {
                this.ViewModel = viewModel;
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
                ViewModel.FrameBuffer = frameBuffer;
            }
        }

    }
}

