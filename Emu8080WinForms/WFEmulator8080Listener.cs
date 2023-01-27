using Emu8080;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emu8080WinForms
{
    internal class WFEmulator8080Listener : IEmulator8080Listener
    {
        public void DisplayInstruction(int pc, OpCode8080 opCode, byte param1, byte param2)
        {
        }

        public void DisplayRegister(Cpu8080 cpu)
        {
        }

        public void DisplayStatus(Status8080 status)
        {
        }

        public void FrameBufferRefresh(byte[] frameBuffer)
        {
        }

        public void InterruptGenerated()
        {
        }

        public void UnimplmentedOperationCode(OpCode8080 opCode)
        {
        }
    }
}
