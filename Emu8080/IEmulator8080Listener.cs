using System;
namespace Emu8080
{
	public interface IEmulator8080Listener
	{
        void UnimplmentedOperationCode(OpCode8080 opCode);
        void DisplayInstruction(int pc, OpCode8080 opCode, byte param1, byte param2);
        void DisplayRegister(Cpu8080 cpu);
        void DisplayStatus(Status8080 status);

    }
}

