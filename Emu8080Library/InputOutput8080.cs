using Microsoft.VisualBasic;
using System;
namespace Emu8080
{
	public class InputOutput8080
    {
		private byte inPort1;

		private byte shiftOffset;
        private byte shift0;
		private byte shift1;

		private byte outPort3;
        private byte outPort5;

        public InputOutput8080()
		{
		}


		public byte In(byte port)
		{
			byte ret = 0x00;

			switch (port)
			{
				case 0:
					ret = 0x0f;
					break;
				case 1:
					ret = inPort1;
                    break;
				case 2:
					ret = 0x00;
					break;
				case 3:
					int value = shift1 << 8 | shift0;
					ret = (byte)((value >> (8 - shiftOffset)) & 0xff);
					break;
			}
			return ret;
		}

		public void Out(byte port, byte value)
		{
            switch (port)
            {
                case 2:
                    shiftOffset = (byte)(value & 0x7);
                    break;
                case 3:
                    outPort3 = value;
                    break;
                case 4:
                    shift0 = shift1;
                    shift1 = value;
                    break;
                case 5:
                    outPort5 = value;
                    break;
            }
        }

		public void Or(byte port, byte value)
		{
			if(port == 0x01)
			{
				inPort1 |= value;
			}
		}

		public void And(byte port, byte value)
		{
            if (port == 0x01)
            {
				inPort1 &= value;
            }
        }

	}
}

