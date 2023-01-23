using System;
using System.Collections;

namespace Emu8080
{
	public class Status8080
	{
        //public byte s { get; set; }
        //      public byte z { get; set; }
        //      public byte ac { get; set; }
        //      public byte p { get; set; }
        //      public byte cy { get; set; }
        public Boolean s { get; set; }
        public Boolean z { get; set; }
        public Boolean pad1 { get; set; }
        public Boolean ac { get; set; }
        public Boolean pad2 { get; set; }
        public Boolean p { get; set; }
        public Boolean pad3 { get; set; }
        public Boolean cy { get; set; }

        public Status8080()
		{
			this.s = false;
			this.z = false;
            this.pad1 = false;
			this.ac = false;
            this.pad2 = false;
			this.p = false;
            this.pad3 = false;
			this.cy = false;
		}

        public void CalcFlagZero(byte value)
        {
            //Z (zero) set to 1 when the result is equal to zero
            this.z = (value & 0xff) == 0;
        }

        public void CalcFlagSign(byte value)
        {
            // S(sign) set to 1 when bit 7(the most significant bit or MSB) of the math instruction is set
            this.s = (value & 0x80) == 0x80;
        }

        public void CalcFlagParity(byte value)
        {
            //P(parity) is set when the answer has even parity, clear when odd parity
            byte num = (byte)(value & 0xff);
            byte total = 0;
            for (total = 0; num > 0; total++)
            {
                num &= (byte)(num - 1);
            }
            this.p = (total % 2) == 0;
        }

        public void CalcFlagCarry(short value)
        {
            //CY (carry) set to 1 when the instruction resulted in a carry out or borrow into the high order bit
            this.cy = (value > 0xff);
        }

        public void CalcFlagAuxCarry(byte value)
        {
            //TODO: implement auxiliary carry flag
            this.ac =  false;
        }

        public void CalcLogicFlags(byte value)
        {
            this.cy = this.ac = false;
            CalcFlagZero(value);
            CalcFlagSign(value);
            CalcFlagParity(value);
        }


        public byte GetPSW()
        {
            bool[] statusArray = new bool[8];
            statusArray[0] = z;
            statusArray[1] = s;
            statusArray[2] = pad1;
            statusArray[3] = p;
            statusArray[4] = pad2;
            statusArray[5] = cy;
            statusArray[6] = pad3;
            statusArray[7] = ac;

            BitArray bits = new BitArray(statusArray);
            byte[] answer = new byte[1];
            bits.CopyTo(answer, 0);
            byte ret = answer[0];
            return ret;
        }

        public void SetPSW(byte psw)
        {
            this.z = (0x01 == (psw & 0x01));
            this.s = (0x02 == (psw & 0x02));
            this.p = (0x04 == (psw & 0x04));
            this.cy = (0x05 == (psw & 0x08));
            this.ac = (0x01 == (psw & 0x10));
        }
    }
}

