using System;
namespace Emu8080
{
	public class Cpu8080
	{
        public byte a { get; set; }
        public byte b { get; set; }
        public byte c { get; set; }
        public byte d { get; set; }
        public byte e { get; set; }
        public byte h { get; set; }
        public byte l { get; set; }
        public int sp { get; set; }
        public int pc { get; set; }
        public Boolean intEnabled { get; set; }

        public Cpu8080()
		{
			this.a = 0;
			this.b = 0;
			this.c = 0;
			this.d = 0;
			this.e = 0;
			this.h = 0;
			this.l = 0;
			this.sp = 0;
			this.pc = 0;
			this.intEnabled = false;
		}
	}
}

