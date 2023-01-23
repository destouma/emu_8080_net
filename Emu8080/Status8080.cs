using System;
using System.Collections;
using System.Collections.Generic;

namespace Emu8080
{
	public class Status8080
	{
        public Boolean s { get; set; }
        public Boolean z { get; set; }
        public Boolean ac { get; set; }
        public Boolean p { get; set; }
        public Boolean cy { get; set; }

        public Status8080()
		{
			this.s = false;
			this.z = false;
			this.ac = false;
			this.p = false;
            this.cy = false;
		}

        public void CalcFlagZero(byte value)
        {
            //Z (zero) set to 1 when the result is equal to zero
            this.z = (value  == 0x00);
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
           // s z pad1 ac pad2 p pad3 cy
            byte answer = (byte)0x00;
            if (s) answer = (byte)(answer | 0x80);
            if (z) answer = (byte)(answer | 0x40);
            if (ac) answer = (byte)(answer | 0x10);
            if (p) answer = (byte)(answer | 0x04);
            if (cy) answer = (byte)(answer | 0x01);

            return answer;
        }

        public void SetPSW(byte psw)
        {
            this.s = (0x80 == (psw & 0x80));
            this.z = (0x40 == (psw & 0x40));
            this.ac = (0x10 == (psw & 0x10));
            this.p = (0x04 == (psw & 0x04));
            this.cy = (0x01 == (psw & 0x01));
        }
    }
}

