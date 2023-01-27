using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Emu8080
{
	public class LiteConsoleEmulator8080Listener : IEmulator8080Listener
	{
		public LiteConsoleEmulator8080Listener()
		{
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
            Console.WriteLine(" ===> interrupt generated");
        }

        public void FrameBufferRefresh(byte[] frameBuffer)
        {
            Console.WriteLine(" ===> refresh display:" + frameBuffer.Length);
        }
    }
}

