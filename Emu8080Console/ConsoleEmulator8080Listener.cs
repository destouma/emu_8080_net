using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Emu8080
{
	public class ConsoleEmulator8080Listener : IEmulator8080Listener
	{
		public ConsoleEmulator8080Listener()
		{
		}

        public void UnimplmentedOperationCode(OpCode8080 opCode)
        {
            Console.WriteLine(" ===> operation code not implemented :" + opCode.code.ToString("x2"));
        }

        public void DisplayInstruction(int pc, OpCode8080 opCode, byte param1, byte param2)
        {
            byte[] displayOneParam = new byte[1];
            displayOneParam[0] = param1;

            byte[] displayTwoParams = new byte[2];
            displayTwoParams[0] = param2;
            displayTwoParams[1] = param1;

            switch (opCode.size)
            {
                case 1:
                    Console.WriteLine(
                        " " + pc.ToString("x8") +
                        " " + opCode.code.ToString("x2") +
                        "       " + opCode.label
                        ); ;
                    break;

                case 2:
                    Console.WriteLine(
                        " " + pc.ToString("x8") +
                        " " + opCode.code.ToString("x2") +
                        " " + param1.ToString("x2") +
                        "    " + opCode.label + Convert.ToHexString(displayOneParam));
                    break;

                case 3:
                    Console.WriteLine(
                        " " + pc.ToString("x8") +
                        " " + opCode.code.ToString("x2") +
                        " " + param1.ToString("x2") +
                        " " + param2.ToString("x2") +
                        " " + opCode.label + Convert.ToHexString(displayTwoParams));
                    break;

                default:
                    Console.WriteLine(
                        " " + pc.ToString("x8") +
                        " " + opCode.code.ToString("x2") +
                        " " + opCode.label);
                    break;
            }
        }

        public void DisplayRegister(Cpu8080 cpu)
        {
            Console.WriteLine(
                " reg:" +
                " A:" + cpu.a.ToString("X2") +
                " B:" + cpu.b.ToString("X2") +
                " C:" + cpu.c.ToString("X2") +
                " D:" + cpu.d.ToString("X2") +
                " E:" + cpu.e.ToString("X2") +
                " H:" + cpu.h.ToString("X2") +
                " L:" + cpu.l.ToString("X2") +
                " SP:" + cpu.sp.ToString("x8")
                );
        }

        public void DisplayStatus(Status8080 status)
        {
            Console.WriteLine(
                " sta: " 
                + (status.s ? "s" : ".")
                + (status.z ? "z" : ".")
                + "."
                + (status.ac ? "a" : ".")
                + "."
                + (status.p ? "p" : ".")
                + "."
                + (status.cy ? "c" : ".")
                );
        }

        public void InterruptGenerated()
        {
            Console.WriteLine(" ===> interrupt generated");
        }
    }
}

