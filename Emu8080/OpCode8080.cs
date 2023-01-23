using System;
namespace Emu8080
{
	public class OpCode8080
	{
		public byte code { get; set; }
        public string? label { get; set; }
        public int size { get; set; }

		public OpCode8080(byte code, string label, int size)
		{
			this.code = code;
			this.label = label;
			this.size = size;
		}

	}
}

