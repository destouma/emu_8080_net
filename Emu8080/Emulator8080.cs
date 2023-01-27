using System;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

namespace Emu8080
{
	public class Emulator8080
	{
		private Cpu8080 cpu;
		private OpCode8080All opCodes;
        private Status8080 status;
        private Memory8080 memory;
        private InputOutput8080 io;
        IEmulator8080Listener listener;

        public Emulator8080(IEmulator8080Listener listener)
		{
			this.cpu = new Cpu8080();
            this.cpu.intEnabled = false;

			this.opCodes = new OpCode8080All();

            this.memory = new Memory8080();

            this.status = new Status8080();

            this.io = new InputOutput8080();

            this.listener = listener;
        }

        public void LoadFileInMemoryAt(int address, String filePath)
		{
			byte[] file = System.IO.File.ReadAllBytes(filePath);
            memory.InitRomFromBuffer(address, file);
		}

        public void Run()
        {
            DateTimeOffset lastInterrupt = DateTimeOffset.UtcNow;
            int interruptNumber = 1;
            while (true)
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                long nowMillisecond = now.ToUnixTimeMilliseconds();
                long lastInteeruptMillisecond = lastInterrupt.ToUnixTimeMilliseconds();
                long diff = nowMillisecond - lastInteeruptMillisecond;
                if (diff > 16)
                {
                    if (cpu.intEnabled)
                    {
                        if (interruptNumber == 1) { 
                            GenerateInterrupt(1);
                            interruptNumber = 2;
                        }
                        else
                        {
                            GenerateInterrupt(2);
                            interruptNumber = 1;
                        }
                        lastInterrupt = DateTimeOffset.UtcNow;
                    }
                }

                int cycles = 0;

                OpCode8080 opCode = opCodes.GetOpCode(memory.ReadByteFromMemoryAt(cpu.pc));
                switch (opCode.code)
                {
                    case 0xdb: // IN
                        {
                            cpu.a = io.In(GetParam(1));
                            cpu.pc += opCode.size;
                            cycles += 3;
                        }
                        break;
                    case 0xd3: // OUT
                        {
                            io.Out(GetParam(1), cpu.a);
                            cpu.pc += opCode.size;
                            cycles += 3;
                        }
                        break;
                    default:
                        cycles += Emulate();
                        break;
                }
               
            }
        }

        private int Emulate()
		{
			OpCode8080 opCode = opCodes.GetOpCode(memory.ReadByteFromMemoryAt(cpu.pc));
            listener.DisplayInstruction(
                cpu.pc,
                opCode,
                GetParam(1),
                GetParam(2));

            switch (opCode.code)
            {
                case 0x00:// NOP
                    cpu.pc += opCode.size;
                    break;
                case 0x01:// LXI	B,word
                    {
                        cpu.b = GetParam(2);
                        cpu.c = GetParam(1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x05:// DCR    B                                            
                    {
                        cpu.b = Decrement(cpu.b);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x06:// MVI B,byte
                    {
                        cpu.b = GetParam(1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x09:// DAD B
                    {
                        int hl = (cpu.h << 8) | cpu.l;
                        int bc = (cpu.b << 8) | cpu.c;
                        int res = hl + bc;
                        cpu.h = (byte)((res & 0xff00) >> 8);
                        cpu.l = (byte)(res & 0xff);
                        status.cy = (res & 0xffff0000) != 0;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x0a: // LDAX B
                    {
                        int offset = cpu.b << 8 | cpu.c;
                        cpu.a = memory.ReadByteFromMemoryAt(offset);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x0d:// DCR    C
                    {
                        cpu.c = Decrement(cpu.c);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x0e:// MVI C,byte
                    {
                        cpu.c = GetParam(1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x0f:// RRC
                    {
                        //TODO: ??? check ???
                        byte x = cpu.a;
                        cpu.a = (byte)((byte)((byte)((byte)x & 1) << 7) | (byte)((byte)x >> 1));
                        status.cy = 1 == (x & 1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x11:// LXI	D,word
                    {
                        cpu.d = GetParam(2);
                        cpu.e = GetParam(1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x13:// INX    D
                    {
                        cpu.e++;
                        if (cpu.e == 0)
                        {
                            cpu.d++;
                        }
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x19:// DAD    D
                    {
                        int hl = (cpu.h << 8) | cpu.l;
                        int de = (cpu.d << 8) | cpu.e;
                        int res = hl + de;
                        cpu.h = (byte)((res & 0xff00) >> 8);
                        cpu.l = (byte)(res & 0xff);
                        status.cy = (res & 0xffff0000) != 0;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x1a:// LDAX	D
                    { 
                        int offset = GetAddress(cpu.d, cpu.e);
                        cpu.a = memory.ReadByteFromMemoryAt(offset);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x21:// LXI	H,word
                    {
                        cpu.h = GetParam(2);
                        cpu.l = GetParam(1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x23:// INX    H
                    {
                        cpu.l++;
                        if (cpu.l == 0)
                        {
                            cpu.h++;
                        }
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x26:// MVI H,byte
                    {
                        cpu.h = GetParam(1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x29:// DAD    H
                    {
                        int hl = (cpu.h << 8) | cpu.l;
                        int res = hl + hl;
                        cpu.h =(byte)( (res & 0xff00) >> 8);
                        cpu.l = (byte)(res & 0xff);
                        status.cy = (res & 0xffff0000) != 0;
                        cpu.pc += opCode.size;
                    }
                    break;

                case 0x31:// LXI	SP,word
                    {
                        cpu.sp = GetAddress(GetParam(2), GetParam(1));

                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x32:// STA    (word)
                    {
                        int offset = GetAddress(GetParam(2), GetParam(1));
                        memory.WriteByteInRamAt(offset,cpu.a);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x35: // DCR M
                    {
                        int offset = GetAddress(cpu.h, cpu.l);
                        byte value = memory.ReadByteFromMemoryAt(offset);
                        memory.WriteByteInRamAt(offset, Decrement(value));
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x36:// MVI	M,byte
                    {
                        int offset = GetAddress(cpu.h , cpu.l);
                        memory.WriteByteInRamAt(offset, GetParam(1));
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x37: // STC
                    {
                        status.cy = true;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x3a:// LDA    (word)
                    {
                        int offset = GetAddress(GetParam(2), GetParam(1));
                        cpu.a = memory.ReadByteFromMemoryAt(offset);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x3d:// DCR    a                                            
                    {
                        cpu.a = Decrement(cpu.a);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x3e:// MVI    A,byte
                    {
                        cpu.a = GetParam(1);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x56:// MOV D,M
                    {
                        int offset = GetAddress(cpu.h,cpu.l);
                        cpu.d = memory.ReadByteFromMemoryAt(offset);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x5e:// MOV E,M
                    {
                        int offset = GetAddress(cpu.h, cpu.l);
                        cpu.e = memory.ReadByteFromMemoryAt(offset);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x66:// MOV H,M
                    {
                        int offset = GetAddress(cpu.h, cpu.l);
                        cpu.h = memory.ReadByteFromMemoryAt(offset);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x6f:// MOV L,A
                    {
                        cpu.l = cpu.a;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x77:// MOV M,A
                    {
                        int offset = GetAddress(cpu.h, cpu.l);
                        memory.WriteByteInRamAt(offset,cpu.a);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x7a:// MOV D,A
                    {
                        cpu.a = cpu.d;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x7b:// MOV E,A
                    {
                        cpu.a = cpu.e;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x7c:// MOV H,A
                    {
                        cpu.a = cpu.h;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0x7e:// MOV A,M
                    {
                        int offset = GetAddress(cpu.h, cpu.l);
                        cpu.a = memory.ReadByteFromMemoryAt(offset);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xa7:// ANA A 
                    {
                        cpu.a = (byte)(cpu.a & cpu.a);
                        status.CalcLogicFlags(cpu.a);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xaf:// XRA A 
                    {
                        cpu.a = (byte)(cpu.a | cpu.a);
                        status.CalcLogicFlags(cpu.a);
                        cpu.pc += opCode.size;
                    }

                    break;
                case 0xc1:// POP B
                    {
                        cpu.c = memory.ReadByteFromMemoryAt(cpu.sp);
                        cpu.b = memory.ReadByteFromMemoryAt(cpu.sp + 1);
                        cpu.sp += 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xc2:// JNZ address
                    {
                        if (!status.z)
                        {
                            cpu.pc = GetAddress(GetParam(2), GetParam(1));
                        }
                        else
                        {
                            cpu.pc += opCode.size;
                        }
                    }
                    break;
                case 0xc3:// JMP address 
                    {
                        cpu.pc = GetAddress(GetParam(2), GetParam(1));
                    }
                    break;
                case 0xc5:// PUSH B
                    {
                        memory.WriteByteInRamAt(cpu.sp - 1, cpu.b);
                        memory.WriteByteInRamAt(cpu.sp - 2, cpu.c);
                        cpu.sp = cpu.sp - 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xc6:// ADI    byte
                    {
                        short x = (short)(cpu.a + GetParam(1));
                        status.CalcFlagZero((byte)x);
                        status.CalcFlagSign((byte)x);
                        status.CalcFlagParity((byte)x);
                        status.CalcFlagCarry(x);
                        cpu.a = (byte)x;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xc8:// RZ
                    {
                        if (status.z)
                        {
                            cpu.pc = GetAddress(memory.ReadByteFromMemoryAt(cpu.sp + 1), memory.ReadByteFromMemoryAt(cpu.sp));
                            cpu.sp += 2;
                        }
                    }
                    break;
                case 0xc9:// RET
                    {
                        cpu.pc = GetAddress(memory.ReadByteFromMemoryAt(cpu.sp + 1), memory.ReadByteFromMemoryAt(cpu.sp));
                        cpu.sp += 2;
                    }
                    break;
                case 0xca: // JZ addr
                    if (status.z)
                    {
                        cpu.pc = GetAddress(GetParam(2),GetParam(1));
                    }
                    else
                    {
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xcd:// CALL address
                    {
                        int ret = cpu.pc + opCode.size;
                        memory.WriteByteInRamAt(cpu.sp - 1,(byte)((byte)(ret >> 8) & 0xff));
                        memory.WriteByteInRamAt(cpu.sp - 2,(byte)(ret & 0xff));
                        cpu.sp -= 2;
                        cpu.pc = GetAddress(GetParam(2), GetParam(1));
                    }
                    break;
                case 0xd1:// POP D
                    {
                        cpu.e = memory.ReadByteFromMemoryAt(cpu.sp);
                        cpu.d = memory.ReadByteFromMemoryAt(cpu.sp + 1);
                        cpu.sp += 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xd2: // JNC addr
                    {
                        if (!status.cy)
                        {
                            cpu.pc = GetAddress(GetParam(2), GetParam(1));
                        }
                        else
                        {
                            cpu.pc += opCode.size; ;
                        }
                    }
                    break;
                    
                case 0xd5:// PUSH D
                    {
                        memory.WriteByteInRamAt(cpu.sp - 1,cpu.d);
                        memory.WriteByteInRamAt(cpu.sp - 2,cpu.e);
                        cpu.sp = cpu.sp - 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xda:// JC
                    {
                        if (status.cy)
                        {
                            cpu.pc = GetAddress(GetParam(2), GetParam(1));
                        }
                        else
                        {
                            cpu.pc += opCode.size;
                        }
                    }
                    break;
                case 0xe1:// POP    H
                    {
                        cpu.l = memory.ReadByteFromMemoryAt(cpu.sp);
                        cpu.h = memory.ReadByteFromMemoryAt(cpu.sp + 1);
                        cpu.sp += 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xe5:// PUSH   H                                              
                    {
                        memory.WriteByteInRamAt(cpu.sp - 1,cpu.h);
                        memory.WriteByteInRamAt(cpu.sp - 2,cpu.l);
                        cpu.sp = cpu.sp - 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xe6:// ANI    byte 
                    {
                        cpu.a = (byte)(cpu.a & GetParam(1));
                        status.CalcLogicFlags(cpu.a);
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xeb:// XCHG/
                    {
                        byte save1 = cpu.d;
                        byte save2 = cpu.e;
                        cpu.d = cpu.h;
                        cpu.e = cpu.l;
                        cpu.h = save1;
                        cpu.l = save2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xf1:// POP PSW
                    {
                        cpu.a = memory.ReadByteFromMemoryAt(cpu.sp +1);
                        status.SetPSW(memory.ReadByteFromMemoryAt(cpu.sp));
                        cpu.sp += 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xf5:// PUSH   PSW
                    {
                        memory.WriteByteInRamAt(cpu.sp - 1,cpu.a);
                        memory.WriteByteInRamAt(cpu.sp - 2, status.GetPSW());
                        cpu.sp = cpu.sp - 2;
                        cpu.pc += opCode.size;
                    }
                    break;
                case 0xfb:// EI
                    cpu.intEnabled = true;
                    cpu.pc += opCode.size;

                    break;
                case 0xfe:// CPI byte 
                    {
                        short answer = (byte)(cpu.a - GetParam(1));
                        status.CalcFlagParity((byte)answer);
                        status.CalcFlagZero((byte)answer);
                        status.CalcFlagSign((byte)answer);
                        status.CalcFlagCarry(answer);
                        cpu.pc += opCode.size;
                    }
                    break;
                default:
                    {
                        listener.UnimplmentedOperationCode(opCode);
                        return 0;
                    }
                    
			}
            listener.DisplayRegister(cpu);
            listener.DisplayStatus(status);

            return opCode.cycles;
		}

        private void GenerateInterrupt(int intNumber)
        {
            listener.InterruptGenerated();
            listener.FrameBufferRefresh(memory.GetVideoRam());

            // perform "PUSH PC"
            memory.WriteByteInRamAt(cpu.sp - 1, (byte)((byte)(cpu.pc >> 8) & 0xff));
            memory.WriteByteInRamAt(cpu.sp - 2, (byte)(cpu.pc & 0xff));
            cpu.sp = cpu.sp - 2;

            // Set the PC to the low memory vector.
            // This is identical to an "RST interrupt_num" instruction.
            cpu.pc = 8 * intNumber;
            cpu.intEnabled = false;
        }

        private int GetAddress(byte msb, byte lsb)
        {
            return (msb << 8) | (lsb);
        }

        private byte GetParam(int num)
        {
            return memory.ReadByteFromMemoryAt(cpu.pc + num);
        }

        private byte Decrement(byte value)
        {
            byte answer = (byte)(value - 1);
            status.CalcFlagParity(answer);
            status.CalcFlagZero(answer);
            status.CalcFlagSign(answer);
            return answer;
        }

    }
}
